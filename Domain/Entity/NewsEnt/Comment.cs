using Contracts.Interfaces;
using Entities.Entity.Users;
using System;
using System.Collections.Generic;
using System.Text;


namespace Entities.Entity.NewsEnt
{
    public class Comment : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? RemoveDate { get; set; }

        public News News { get; set; }
        public Guid NewsId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
