using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
   public class RssSourceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; } 
        public DateTime DateOfReceiving { get; set; } = DateTime.Now;
       
    }
}
