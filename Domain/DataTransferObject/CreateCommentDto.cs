using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
   public class CreateCommentDto
    {
        public Guid NewsId { get; set; }
        public string CommentText { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
