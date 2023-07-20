using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class OrderModel
    {
        public int OrderId { get; set; }
        public int DonorId { get; set; }
        public int? PrimaryReceiverId { get; set; }
        public int? SecondaryReceiverId { get; set; }
        public DateTime? OrderDate { get; set; }
        public Decimal? OrderAmount { get; set; }
        public string OrderTitle { get; set; }
        public string DonorAddress1 { get; set; }
        public string DonorAddress2 { get; set; }
        public string DonorCity { get; set; }
        public string DonorState { get; set; }
        public string DonorZip { get; set; }
        public string DonorCountry { get; set; }
        public string ReceiverAddress1 { get; set; }
        public string ReceiverAddress2 { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverState { get; set; }
        public string ReceiverZip { get; set; }
        public string ReceiverCountry { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Donar { get; set; }
        public string CreatedByName { get; set; }
        public string ReceiverType { get; set; }
        public int TotalRows { get; set; }
    }
}
