using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class AccountCustomFieldValueModel
    {
        public int AccountCustomFieldValueId { get; set; }
        public int AccountId { get; set; }
        public int AccountCustomFieldId { get; set; }
        public string FieldValue { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
