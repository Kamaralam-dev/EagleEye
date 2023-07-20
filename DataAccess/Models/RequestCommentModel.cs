using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RequestCommentModel
    {
        public int CommentId { get; set; }
        public string Comments { get; set; }
        public DateTime? CommentDate { get; set; }
        public int RequestId { get; set; }
        public int CreatedBy { get; set; }

    }
}
