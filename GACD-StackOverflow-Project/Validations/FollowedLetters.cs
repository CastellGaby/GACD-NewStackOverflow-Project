using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace GACD_StackOverflow_Project.Validations
{
    public class FollowedLetters : ValidationAttribute
    {
        protected override ValidationResult IsValid(object password, ValidationContext validationContext)
        {
            if (password != null) { 
                string Password = ((string)password).Trim();
                int[] PasswordASCII = new int[Password.Length];

                

                for (int t = 0; t < Password.Length; t++)
                {
                    PasswordASCII[t] = Encoding.ASCII.GetBytes(Password.Substring(t))[0];
                }

                for (int i = 0; i < Password.Length; i++)
                {
                    for (int j = i + 1; j < Password.Length; j++)
                    {
                        if ((PasswordASCII[j] == PasswordASCII[i]) && (!(PasswordASCII[i] > 47) && !(PasswordASCII[i] < 58)))
                        {
                            return new ValidationResult("Are not allowed twice the same letter");
                        }
                    }
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("This field is required");
        }
    }
}