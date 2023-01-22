using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basIp
{
    static public class OzonApi
    {
        static HttpClient httpClient = new HttpClient();
        static public async Task CategoryAttribute()
        {

            string json = JsonConvert.SerializeObject(new { attribute_type = "ALL", category_id = new int[] { 17036194 } });

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            content.Headers.Add("Client-Id", "840507");
            content.Headers.Add("Api-Key", "124ed60e-be56-4972-9691-d49a8bf050fc");

            httpClient.Timeout = TimeSpan.FromSeconds(200);

            using var response = await httpClient.PostAsync("https://api-seller.ozon.ru/v3/category/attribute", content);

            string responseText = await response.Content.ReadAsStringAsync();

        }
        static public async Task CategoryAttributeValues()
        {

            string json = JsonConvert.SerializeObject(new { attribute_id = 85, category_id = 17036194, language = "DEFAULT", last_value_id = 0, limit = 5000 });

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            content.Headers.Add("Client-Id", "840507");
            content.Headers.Add("Api-Key", "124ed60e-be56-4972-9691-d49a8bf050fc");

            httpClient.Timeout = TimeSpan.FromSeconds(200);

            using var response = await httpClient.PostAsync("https://api-seller.ozon.ru/v2/category/attribute/values", content);

            string responseText = await response.Content.ReadAsStringAsync();

        //    {
        //        "id": 970989256,
        //    "value": "BAS-IP",
        //    "info": "Домофоны",
        //    "picture": "https://cdn1.ozone.ru/s3/multimedia-5/6056252561.jpg"
        //}

        }

        static public async Task ProductImport(List<Offer> offers)
        {
            var offer = offers[0];
            string json = JsonConvert.SerializeObject(
                new {
                    items = new[] {
                        new{ attributes = new[]{
                        new{ id = 85,
                             values = new[] { new { complex_id= 0, dictionary_value_id = 970989256, value = "" } } },
                        new{ id = 8229,
                             values = new[] { new { complex_id = 0, dictionary_value_id = 95540, value = "" } } },
                        new{ id = 9048,
                             values = new[] { new { complex_id = 0, dictionary_value_id = 0, value = $"{offer.Name}" } } },
                        new{ id = 4384,
                             values = new[] { new { complex_id = 0, dictionary_value_id = 0, value = $"{offer.Description}" } } },
                        } } },
                    barcode = "485387985897676978",   //штрихкод
                    category_id = 17036194,
                    color_image = "",
                    complex_attributes= new[] { $"{null}" },
                    currency_code = "RUB",
                    depth = 10,
                    dimension_unit = "mm",
                    height = $"{offer.Parameters.Where(x => x.NameParameter == "Высота").Select(x => x.ValueParameter).FirstOrDefault()}",
                    images = new[] { $"{offer.Picture}" },
                    images360 = new[] { $"{null}" },
                    name = $"{offer.Name}",
                    offer_id = "1432132467",              //артикул
                    old_price = $"{offer.Price}",
                    pdf_list = new[] { $"{null}" },
                    premium_price = $"{offer.Price * 0.7}",
                    price = $"{offer.Price * 0.9}",
                    primary_image = "",
                    vat = "0.1",
                    weight = 10,
                    weight_unit = "g",
                    width = $"{offer.Parameters.Where(x => x.NameParameter == "Ширина").Select(x => x.ValueParameter).FirstOrDefault()}"
                }) ;
            Console.WriteLine(json);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            content.Headers.Add("Client-Id", "840507");
            content.Headers.Add("Api-Key", "124ed60e-be56-4972-9691-d49a8bf050fc");

            httpClient.Timeout = TimeSpan.FromSeconds(200);

            using var response = await httpClient.PostAsync("https://api-seller.ozon.ru/v2/product/import", content);

            string responseText = await response.Content.ReadAsStringAsync();

        }
    }
}

//task_id\":658149152    task_id\":658155004       658198783



