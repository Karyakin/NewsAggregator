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

        public Guid? RssSourseId { get; set; }
        public Guid? CategoryId { get; set; }

        // public SelectList Sources { get; set; }
    }
}
