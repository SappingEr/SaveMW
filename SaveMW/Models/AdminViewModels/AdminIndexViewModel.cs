using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveMW.Models.AdminViewModels
{
    public class AdminIndexViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public Paging Paging { get; set; }
    }
}