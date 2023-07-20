using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class AccountSocialModel
    {
        public int AccountSocialId { get; set; }
        public int AccountId { get; set; }
        public string Handler { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
