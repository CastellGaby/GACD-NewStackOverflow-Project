using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace GACD_StackOverflow_Project.Validations
{
    public class VocalValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object password, ValidationContext validationContext)
        {
            if (password != null)
            {
                string Password = ((string) password).Trim();
                int[] PasswordASCII = new int[Password.Length];

                for (int x = 0; x < Password.Length; x++)
                {
                    PasswordASCII[x] = Encoding.ASCII.GetBytes(Password.Substring(x))[0];
                    switch (PasswordASCII[x])
                    {
                        case 'A':
                        case 'a':
                            return ValidationResult.Success;

                        case 'E':
                        case 'e':
                            return ValidationResult.Success;

                        case 'I':
                        case 'i':
                            return ValidationResult.Success;

                        case 'O':
                        case 'o':
                            return ValidationResult.Success;

                        case 'U':
                        case 'u':
                            return ValidationResult.Success;
                    }
                }
                return new ValidationResult("Is necessary a Vocal in your Password");
            }
            return new ValidationResult("This field is required");
        }
        
    }
}