using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GACD_StackOverflow_Project.Validations;

namespace GACD_StackOverflow_Project.Models
{
    public class AccountRecoveryAccountModel
    {
        [Required]
        [FollowedLetters]
        [CapitalValidation]
        [NumberValidation]
        [VocalValidation]
        [SymbolValidation]
        [StringLength(16, ErrorMessage = "The password must be around 16 and 8 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [FollowedLetters]
        [CapitalValidation]
        [NumberValidation]
        [VocalValidation]
        [SymbolValidation]
        [StringLength(16, ErrorMessage = "The password must be around 16 and 8 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}