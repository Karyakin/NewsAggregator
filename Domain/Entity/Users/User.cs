using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity.News;

namespace Domain.Entity.Users
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string AdditionalInformation { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime RemovedDate { get; set; }
        public DateTime LastActiv { get; set; }

        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        public ContactDetails ContactDetails { get; set; }
        public long ContactDetailsId { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
