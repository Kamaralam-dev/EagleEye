using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class CompanyModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public int LanguageId { get; set; }
        public int TimezoneId { get; set; }
        public string Tax1Name { get; set; }
        public string Tax1Number { get; set; }
        public Decimal Tax1Percent { get; set; }
        public string Tax2Name { get; set; }
        public string Tax2Number { get; set; }
        public Decimal Tax2Percent { get; set; }
        public string Tax3Name { get; set; }
        public string Tax3Number { get; set; }
        public Decimal Tax3Percent { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Website { get; set; }
        public string Fax { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}
