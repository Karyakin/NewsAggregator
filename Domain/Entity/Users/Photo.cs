using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity.Users
{
    public class Photo : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
