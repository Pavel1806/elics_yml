using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace basIp
{
    static internal class BasIpYml
    {
        
       public static async Task<List<Offer>> DownLoadxml(int[] categId)
        {
            HttpClient httpClient = new HttpClient();

            string? URLString = "https://bas-ip.su/files/yml.xml"; //     https://stelberry-shop.ru/files/yml.xml

            var response = (await httpClient.GetAsync(URLString)).EnsureSuccessStatusCode();

            var notification = await response.Content.ReadAsStringAsync();

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(notification);

            XmlElement? xRoot = xDoc.DocumentElement;
            List<Offer> offers = new List<Offer>();
            List<string> str = new List<string>();

            if (xRoot != null)
            {
                foreach(XmlElement node in xRoot)
                {
                    foreach (XmlElement child in node)
                    {
                        if(child.Name == "offers")
                        {
                           foreach(XmlElement item in child)
                            {
                                XmlNode? available = item.Attributes.GetNamedItem("available");
                                XmlNode? id = item.Attributes.GetNamedItem("id");

                                Offer offer = new Offer()
                                {
                                    Available = bool.Parse(available.Value),
                                    Id = int.Parse(id.Value),
                                };
                                
                                foreach (XmlNode node2 in item)
                                {
                                    switch (node2.Name)
                                    {
                                        case "url":
                                            offer.Url = node2.InnerText;
                                            break;
                                        case "price":
                                            offer.Price = int.Parse(node2.InnerText);
                                            break;
                                        case "currencyId":
                                            offer.CurrencyId = node2.InnerText;
                                            break;
                                        case "name":
                                            offer.Name = node2.InnerText;
                                            break;
                                        case "description":
                                            offer.Description = node2.InnerText;
                                            break;
                                        case "picture":
                                            offer.Picture = node2.InnerText;
                                            break;
                                        case "categoryId":
                                            offer.CategoryId = node2.InnerText;
                                            break;
                                        case "param":
                                            XmlNode? name = node2.Attributes.GetNamedItem("name");
                                            if(!name.Value.Contains("Габаритные размеры"))  //
                                            {
                                                offer.Parameters.Add(new Parameter() { NameParameter = name.Value, ValueParameter = node2.InnerText });
                                            }
                                            else
                                            {
                                                var size = node2.InnerText.Split("&times;");

                                                offer.Parameters.Add(new Parameter() { NameParameter = "Высота", ValueParameter = size[0] });
                                                offer.Parameters.Add(new Parameter() { NameParameter = "Ширина", ValueParameter = size[1] });
                                                offer.Parameters.Add(new Parameter() { NameParameter = "Толщина", ValueParameter = size[2] });
                                            }
                                       break;
                                    }

                                }
                                if(!offer.Url.Contains("arhiv"))
                                {
                                    if (categId.Length > 0) 
                                    { 
                                        foreach (var categ in categId)
                                        {
                                            if(int.Parse(offer.CategoryId) == categ)
                                            {
                                                offers.Add(offer);
                                                str.Add($"{offer.Url};{offer.Name};{offer.CategoryId};{offer.Price};{offer.CurrencyId}");
                                                
                                            }
                                        }
                                        continue;
                                    }
                                    offers.Add(offer);
                                    str.Add($"{offer.Url};{offer.Name};{offer.CategoryId};{offer.Price};{offer.CurrencyId}");
                                }
                                
                            }
                            await File.AppendAllLinesAsync("list.txt", str);
                        }
                        
                    }
                    
                }
            }
            return offers;
        }
    }
}
