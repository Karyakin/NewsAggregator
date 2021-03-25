using Microsoft.EntityFrameworkCore;
using Entities.Entity.News;
using Entities.Entity.Users;
using Entity.Users;

namespace Repositories.Context
{
    public class NewsDataContext : DbContext
    {
        public NewsDataContext(DbContextOptions<NewsDataContext> options) : base(options)
        {
        }

        private DbSet<ContactDetails> ContactDetails { get; set; }
        private DbSet<Phone> Phones { get; set; }
        private DbSet<EMail> EMails { get; set; }
        private DbSet<City> Сities { get; set; }
        private DbSet<Country> Countries { get; set; }
        private DbSet<User> Users { get; set; }
        private DbSet<Role> Roles { get; set; }
        private DbSet<Photo> Photos { get; set; }
        private DbSet<Author> Authors { get; set; }
        private DbSet<RssSource> Sources { get; set; }
        private DbSet<Category> Categories{ get; set; }
        private DbSet<Comment> Comments{ get; set; }


    }
}
