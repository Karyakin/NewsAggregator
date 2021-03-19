using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgregator.DAL.Entities.Entity.Users
{
    public class Photo
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }

        public User User { get; set; }
        public long UserId { get; set; }
    }
}
