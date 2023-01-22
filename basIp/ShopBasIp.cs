using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basIp
{
    public class ShopBasIp
    {
        public string? Name { get; set; }
        public string? Company { get; set; }
        public List<Currency>? Currencies { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Offer>? Offers { get; set; }

        public ShopBasIp()
        {
            Currencies = new List<Currency>();
            Categories = new List<Category>();
            Offers = new List<Offer>();
        }

    }
}
