using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Identity.Data
{
    public class NewsAgregatorContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Comment> Comments { get; set; }
        public NewsAgregatorContext(DbContextOptions<NewsAgregatorContext> options)
            :base(options)
        {
            
        }

    }
}
