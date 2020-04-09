using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaveMW.Models.AccountViewModels
{
    public class ChangePassViewModel
    {
        public int Id { get; set; }

        [StringLength(30)]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите Пароль")]
        public string Password { get; set; }

        [StringLength(30)]
        [Display(Name = "Новый Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите новый Пароль")]
        public string NewPassword { get; set; }

        [StringLength(30)]
        [Display(Name = "Подтвердите новый Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите новый Пароль")]
        public string ConfirmNewPassword { get; set; }
    }
}