using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class FaqModel
    {
        public int FaqId { get; set; }
        public string Faq { get; set; }
        public string Answer { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int TotalRows { get; set; }

    }
}
