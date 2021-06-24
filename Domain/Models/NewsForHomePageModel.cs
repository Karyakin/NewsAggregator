using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
   public class NewsForHomePageModel
    {
        public Guid Id { get; set; }
        public NewsGetDTO GoogNews { get; set; }
        public NewsGetDTO BadNews { get; set; }
    }
}
