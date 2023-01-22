using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basIp
{
    public class Offer
    {
        public int Id { get; set; }
        public bool Available { get; set; }
        public string? Url { get; set; }
        public int Price { get; set; }
        //public Currency? CurrencyId { get; set; }
        //public Category? CategoryId { get; set; }
        public string? CurrencyId { get; set; }
        public string? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }

        public List<Parameter>? Parameters { get; set; }

        public Offer()
        {
            Parameters = new List<Parameter>();
        }

    }
}
