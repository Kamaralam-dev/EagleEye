using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class PaymentModel
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public int PaymnetModeId { get; set; }
        public string PaymentDetail { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string OrderTitle { get; set; }
        public string PaymentMode { get; set; }
        public string CreatedByName { get; set; }
        public int TotalRows { get; set; }
    }
}
