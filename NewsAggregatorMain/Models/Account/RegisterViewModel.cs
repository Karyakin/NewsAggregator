using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please fill Login field")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please fill email field")]
        [DataType(DataType.EmailAddress)] // ___@{}.{}
        public string Email { get; set; }

        [Required(ErrorMessage = "Please fill Country field")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please fill City field")]
        public string City { get; set; }

        public string Phones { get; set; }
        



        [Required(ErrorMessage = "Please fill password field")]
        [DataType(DataType.Password)]// минимально 8 символов, как минимум 1 маленькая буква, 1 большая,1 спецсимвол - это по умолчанию
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is incorrect")]// сравниваем со свойством Password
        public string PasswordConfirmation { get; set; }
    }
}
