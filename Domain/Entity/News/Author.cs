using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity.News
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Source> Sources { get; set; }
        public string TEST { get; set; } 

    }
}
