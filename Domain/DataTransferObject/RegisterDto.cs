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
        [Required(ErrorMessage = "Введите имя пользователя")] 
        [RegularExpression("^[A-Za-z]([.A-Za-z0-9-]{1,18})([A-Za-z0-9])$", ErrorMessage = "Login должен начинаться с латинской буквы, может состоять из латинских букв, цифр, точек, минуса, а заканчиваться буквой или цифрой, пробелы запрещены")]
        [Remote("CheckLogin", "Account", ErrorMessage = "Пользователь с таким именем уже существует")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Некорректно введен адрес электронной почты. Пример: News@mail.com")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Пользователь с таким Email уже зарегистрирован")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Заполните поле")]
        [RegularExpression(@"^[^\s][^!@#$%^&*()_]+[0-9a-zA-Zа-яА-Я\s]*$", ErrorMessage = "Пробелы и одиночные спецсимволы в начале строеи недопустимы")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Введите телефон")]
        [RegularExpression(@"^\+375\((17|29|33|44|25)\)[0-9]{3}[0-9]{2}[0-9]{2}$", ErrorMessage = "Формат для ввода телефона: +375(**)*********")]
        [Remote("CheckPhone", "Account", ErrorMessage = "Пользователь с таким номером телефона уже зарегистрирован")]
        public string Phones { get; set; }


        [Required(ErrorMessage = "Введите дату")] 
        [Remote("CheckDate", "Account", ErrorMessage = "Вы не можете родится в будущем")]
        public DateTime DayOfBirth { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Remote("CheckCountry", "Account", ErrorMessage = "Такой траны не существует")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Remote("CheckCity", "Account", ErrorMessage = "Такого города не существует")]
        public string City { get; set; }


        [Required(ErrorMessage = "Придумайте пароль")]
        [DataType(DataType.Password)]// минимально 8 символов, как минимум 1 маленькая буква, 1 большая,1 спецсимвол - это по умолчанию
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{3,15}$", 
            ErrorMessage = "Пароль должен содержать: от 3 до 15 символов на латинице, хотя бы одно число, хотя бы одну заглавную букву и одну строчную букву.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirmation { get; set; }


        public string ReturnUrl { get; set; }

        public IEnumerable<string> CitiesName { get; set; }

        public IEnumerable<string> CountryName { get; set; }
    }
}
