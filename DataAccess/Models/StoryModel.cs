using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class StoryModel
    {
        public int StoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string RelatedTo { get; set; }
        public int RelatedToUser { get; set; }
        public DateTime? StoryDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ImageUrl { get; set; }
        public int TotalRows { get; set; }
    }
}
