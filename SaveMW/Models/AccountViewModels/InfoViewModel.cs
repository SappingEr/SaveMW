using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaveMW.Models.AccountViewModels
{
    public class InfoViewModel: EditViewModel
    {
        public int NotesCount { get; set; }        
    }
}