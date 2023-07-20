using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class ItemModel
    {
        public int ItemId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public int DonerId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int PrimaryReceiverId { get; set; }
        public int SecondaryReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string RecieverAddress { get; set; }
        public string RecieverCity { get; set; }
        public string ReceiverState { get; set; }
        public string ReceiverZip { get; set; }
        public string ReceiverCountry { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public string WarrentyPeriod { get; set; }
        public decimal? MarketValue { get; set; }
        public int? Quantity { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Donor { get; set; }
        public string Description { get; set; }
        public DateTime? ClaimByDate { get; set; }
        public int TotalRows { get; set; }
        public string ItemType { get; set; }
    }
}
