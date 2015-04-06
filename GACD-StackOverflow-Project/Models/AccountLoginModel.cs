using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GACD_StackOverflow_Project.Models
{
    public class AccountLoginModel
    {
        
        [Required(ErrorMessage = "*This Field is required")]
        [DataType(DataType.EmailAddress)]
        public string  Email{ get; set; }

        [Required(ErrorMessage = "*This Field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool ImnotRobot { get; set; }

    }

}