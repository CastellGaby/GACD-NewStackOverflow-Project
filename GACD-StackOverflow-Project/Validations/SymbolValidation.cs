using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace GACD_StackOverflow_Project.Validations
{
    public class SymbolValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object password, ValidationContext validationContext)
        {
            if (password != null)
            {
                string Password = ((string)password).Trim();
                int[] PasswordASCII = new int[Password.Length];

                for (int x = 0; x < Password.Length; x++)
                {

                    PasswordASCII[x] = Encoding.ASCII.GetBytes(Password.Substring(x))[0];
                    if (!(PasswordASCII[x] > 96 && PasswordASCII[x] < 123) && !(PasswordASCII[x] > 64 && PasswordASCII[x] < 91) && !(PasswordASCII[x] > 47 && PasswordASCII[x] < 58))
                    {
                        return new ValidationResult("Are not allowed Symbols in the password");
                    }
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("This field is required");
       }
    }
}