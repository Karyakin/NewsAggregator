using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class NewsGetDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }


    }
}
