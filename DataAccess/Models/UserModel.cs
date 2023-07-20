using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }  
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; } 
        public DateTime CreatedOn
        {
            get { return DateTime.Now; }
        }
    }
}
