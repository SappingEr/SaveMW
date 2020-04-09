using System.ComponentModel.DataAnnotations;

namespace SaveMW.Models.AccountViewModels
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [StringLength(30)]
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Введите Логин")]
        public string UserName { get; set; }

        [StringLength(150)]
        [Display(Name = "ФИО")]
        [Required(ErrorMessage = "Введите ФИО")]
        public string FIO { get; set; }
    }
}