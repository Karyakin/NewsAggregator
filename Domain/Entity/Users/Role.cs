using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgregator.DAL.Entities.Entity.Users
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsMember { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime RemovedDate { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
