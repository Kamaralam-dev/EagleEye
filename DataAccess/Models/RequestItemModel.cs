using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RequestItemModel
    {
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string RequestNote { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string PurchaseDate { get; set; }
        public string MarketValue { get; set; }
        public string Condition { get; set; }
        public string ClaimByDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ClaimByName { get; set; }
        public string ClaimByPhone { get; set; }
        public string ClaimByEmail { get; set; }
        public string ImageUrl { get; set; }
        public string RequestType { get; set; }

    }
}
