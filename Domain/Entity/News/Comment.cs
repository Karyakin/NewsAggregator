using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity.Users;


namespace Domain.Entity.News
{
    public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime RemoveDate { get; set; }

        public News News { get; set; }
        public long NewsId { get; set; }

        public User User { get; set; }
        public long UserId { get; set; }
    }
}
