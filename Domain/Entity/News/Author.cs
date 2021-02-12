using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entity.News
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Source> Sources{ get; set; }

    }
}
