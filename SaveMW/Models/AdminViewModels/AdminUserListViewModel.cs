using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveMW.Models.AdminViewModels
{
    public class AdminUserListViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public Paging Paging { get; set; }

        public FetchOptions FetchOptions { get; set; } = new FetchOptions();
    }
}