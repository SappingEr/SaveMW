using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaveMW.Models.NoteViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }

        [StringLength(150)]
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название Новой Заметки")]
        public string Name { get; set; } = "Без названия";

        [AllowHtml]
        [Display(Name = "Текст")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        [Display(Name = "Теги")]
        public string Tags { get; set; }
    }
}