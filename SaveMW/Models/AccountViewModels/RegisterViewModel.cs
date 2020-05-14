using SaveMW.Models.UserViewModels;
using System.ComponentModel.DataAnnotations;

namespace SaveMW.Models.AccountViewModels
{
    public class RegisterViewModel : EditViewModel
    {
        [StringLength(30)]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите Пароль")]
        public string Password { get; set; }

        [StringLength(30)]
        [Display(Name = "Подтвердите Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль повторно")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}