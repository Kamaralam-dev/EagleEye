using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DataAccess.Models
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int CountryId { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public int ShippingCountryId { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public int? AccountTypeId { get; set; }
        public int? IndustryId { get; set; }
        public string Priority { get; set; }
        public int? SourceId { get; set; }
        public string SubSource { get; set; }
        public int EmployeeCount { get; set; }
        public string TaxNumber { get; set; }
        public int? RatingId { get; set; }
        public string ImageUrl { get; set; }
        public int OwnerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public int NewAccountId  { get; set; }
        public string Phone{ get; set; }
        public string PhoneCode { get; set; }
        public string Owner { get; set; }
        public string Taxname { get; set; }
        public string Source { get; set; }
        public string CountryName { get; set; }
    }
}
