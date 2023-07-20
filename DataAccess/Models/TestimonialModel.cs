using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class TestimonialModel
    {
        public int TestimonialId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string GivenBy { get; set; }
        public string GivenByTitle { get; set; }
        public string GivenByCompany { get; set; }
        public int TotalRows { get; set; }
    }
}
