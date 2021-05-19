using Entities.Models;
using Entity.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class RegisterDto
    {
        //[StringLength(20, MinimumLength = 6)] //_@_.ii
        //[Required(ErrorMessage = "Please fill Login field")] 
        // [MaxLength(5,ErrorMessage ="максимальная длинна")]

        [Required(ErrorMessage = "asxasxsax")]
        [Remote("CheckLogin", "Account", ErrorMessage = "Current email already exist")]
        public string Login { get; set; }

        //[Required(ErrorMessage = "Please fill email field")]
        //[DataType(DataType.EmailAddress)] // ___@{}.{}

       
        [EmailAddress(ErrorMessage = "Поле Email должно быть заполнено")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Current email already exist")]
        public string Email { get; set; }

        [Required(ErrorMessage = "asxasxsax")]
        public DateTime DayOfBirth { get; set; }

        //  [Required(ErrorMessage = "Please fill Country field")]
        [Required(ErrorMessage = "asxasxsax")]
        public string Country { get; set; }

        //[Required(ErrorMessage = "Please fill City field")]
        [Required(ErrorMessage = "asxasxsax")]
        public string City { get; set; }
        [Required(ErrorMessage = "asxasxsax")]
        public string LastName { get; set; }

        [Required]
        // [RegularExpression("^[0-9*#+-()]+$")]
        [Phone]
        public string Phones { get; set; }
        //[Required(ErrorMessage = "Please fill password field")]
        [DataType(DataType.Password)]// минимально 8 символов, как минимум 1 маленькая буква, 1 большая,1 спецсимвол - это по умолчанию
        /* [RegularExpression("^[0-9*#+-()]+$",//"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[.#$^+=!*()@%&]).{8,}$",
             ErrorMessage = "Password must contain blablabla")]*/
        public string Password { get; set; }


        // [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Password is incorrect")]// сравниваем со свойством Password

        [Required]
        // [Compare("Password", ErrorMessage = "123123")]
        public string PasswordConfirmation { get; set; }
        // public PasswordSoultModel PasswordSoultModel { get; set; }

        // public SelectList SelectListSourseCountry{ get; set; }
        // public SelectList SelectListSourseCity{ get; set; }
        /*   public Guid? CountrySourseId { get; set; }
           public Guid? CitySourseId { get; set; }*/

        public string ReturnUrl { get; set; }


        public IEnumerable<string> CitiesName { get; set; }
        public IEnumerable<string> CountryName { get; set; }



    }
}
