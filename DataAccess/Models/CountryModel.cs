using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class CountryModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryShort { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public string Language { get; set; }
        public string IsdCode { get; set; }

    }
}
