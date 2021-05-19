using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Identity.Data
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? RemoveDate { get; set; }

      //  public News News { get; set; }
        public Guid NewsId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
