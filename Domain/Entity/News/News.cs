using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entity.News
{
    [Table("News")]
    public class News
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid SourceId { get; set; }
        public RssSource Source { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
