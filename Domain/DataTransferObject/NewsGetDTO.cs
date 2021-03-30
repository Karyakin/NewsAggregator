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
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        /*public Guid CategoryId { get; set; }
        public Guid SourceId { get; set; }*/

        public CategoryModel Category { get; set; }
        public RssSourceModel Source { get; set; }
    }
}
