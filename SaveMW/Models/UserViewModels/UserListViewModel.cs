using System.Collections.Generic;

namespace SaveMW.Models.UserViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public Paging Paging { get; set; }

        public FetchOptions FetchOptions { get; set; }
    }
}