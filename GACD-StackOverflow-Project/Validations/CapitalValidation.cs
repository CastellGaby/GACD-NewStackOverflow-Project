using System;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace GACD_StackOverflow_Project.Validations
{
    public class CapitalValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object password, ValidationContext validationContext)
        {
            if (password != null) { 
                string Password = ((string)password).Trim();
                int[] PasswordASCII = new int[Password.Length];
                

                for (int x = 0; x < Password.Length; x++)
                {
                    PasswordASCII[x] = Encoding.ASCII.GetBytes(Password.Substring(x))[0];
                    if (PasswordASCII[x] > 64 && PasswordASCII[x] < 91)
                    {
                        return ValidationResult.Success;
                    }
                }
                return new ValidationResult("Is necessary a Capital Letter in your Password");
            }
            return new ValidationResult("This field is required");
        }

        
    }
}