using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
   public class NewsInfoFromRssSourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }

        public Guid? RssSourceId { get; set; }
        public RssSource RssSource { get; set; }

        public Guid? CategoryId { get; set; }

        public Category Category { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }

        
    }
}
