using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class AccountPhoneModel
    {
        public int AccountPhoneId { get; set; }
        public int AccountId { get; set; }
        public int CountryId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        
    }
}
