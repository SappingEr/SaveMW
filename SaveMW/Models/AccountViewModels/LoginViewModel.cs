using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaveMW.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [StringLength(30)]
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Введите Логин")]
        public string Login { get; set; }

        [StringLength(30)]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}