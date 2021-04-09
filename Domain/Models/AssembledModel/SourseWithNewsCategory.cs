using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.AssembledModel
{
   public class SourseWithNewsCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; } 
        public DateTime DateOfReceiving { get; set; }
       // public IEnumerable<Guid> NewsId { get; set; }
        public IEnumerable<News> News{ get; set; }
        public Category Categories { get; set; }
    }
}
