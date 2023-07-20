using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class AccountCustomFieldModel
    {
       // temp
        public int AccountCustomFieldId { get; set; }
        public int CompanyId { get; set; }
        public string DisplayName { get; set; }
        public string DataType { get; set; }
        public string Control { get; set; }
        public string ControlOption { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Boolean IsRequired { get; set; }
        public int OrderIndex { get; set; }
    }

    public class AccountSortOrder
    {
        public int AccountCustomFieldId { get; set; }
        public int ToPosition { get; set; }
    }
}
