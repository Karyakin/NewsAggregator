using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgregator.DAL.Entities.Entity.Users
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
