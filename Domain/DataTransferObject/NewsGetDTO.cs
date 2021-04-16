using Entities.Entity.NewsEnt;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class NewsGetDTO
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

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
