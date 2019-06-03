using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransacaoApi.Models
{
    public class MerchantDiscountReturn
    {
        public string Adquirente { get; set; }
        public List<Taxa> Taxas { get; set; }
    }
}
