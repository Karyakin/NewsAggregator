using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entity.NewsEnt
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<RssSource> Sources{ get; set; }

    }
}
