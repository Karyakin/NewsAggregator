using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAgregator.DAL.Entities.Entity.News
{
    public class News
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public long SourceId { get; set; }
        public Source Source { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
