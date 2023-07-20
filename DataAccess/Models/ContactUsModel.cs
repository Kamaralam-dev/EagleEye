using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class ContactUsModel
    {
        public int ContactUsId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Query { get; set; }
        public DateTime QueryDate { get; set; }
        public bool IsReplied { get; set; }
        public bool IsViewed { get; set; }
        public bool IsDeleted { get; set; }
        public string Subject { get; set; }
        public int TotalRows { get; set; }

    }
}
