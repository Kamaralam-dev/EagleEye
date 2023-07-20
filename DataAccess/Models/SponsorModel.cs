using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class SponsorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int SortOrder { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int TotalRows { get; set; }
    }
}
