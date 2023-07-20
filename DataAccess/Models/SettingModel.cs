using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class SettingModel
    {
        public int SettingId { get; set; }
        public string Name { get; set; }
        public string DisplayAs { get; set; }
        public int TotalRows { get; set; }
    }
}
