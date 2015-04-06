using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GACD_StackOverflow_Project.Models
{
    public class AccountPasswordRecoveryModel
    {
        /*[Required]
        public string Username { get; set; }*/
        
        [Required(ErrorMessage = "*This Field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}