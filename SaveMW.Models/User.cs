using Microsoft.AspNet.Identity;
using SaveMW.Models.Repositories;
using System.Collections.Generic;

namespace SaveMW.Models
{
    public class User : IUser<int>
    {
        public virtual int Id { get; set; }

        [FastSearch]
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        [FastSearch]
        public virtual string FIO { get; set; }

        public virtual IList<Note> Notes { get; set; } = new List<Note>();

        public virtual UserStatus UserStatus { get; set; }
    }
}
