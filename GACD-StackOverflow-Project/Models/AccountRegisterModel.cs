using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GACD_StackOverflow_Project.Validations;

namespace GACD_StackOverflow_Project.Models
{
    public class AccountRegisterModel
    {
        [StringLength(50, ErrorMessage = "The name must be around 50 and 2 characters", MinimumLength = 2)]
        [Required]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "The lastname must be around 50 and 2 characters", MinimumLength = 2)]
        [Required]
        public string Lastname { get; set; }
       
        [Required]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [FollowedLetters]
        [Required]
        [CapitalValidation]
        [NumberValidation]
        [VocalValidation]
        [SymbolValidation]
        [StringLength(16, ErrorMessage = "The password must be around 16 and 8 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [FollowedLetters]
        [Required]
        [CapitalValidation]
        [NumberValidation]
        [VocalValidation]
        [SymbolValidation]
        [StringLength(16, ErrorMessage = "The password must be around 16 and 8 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }

    }
}