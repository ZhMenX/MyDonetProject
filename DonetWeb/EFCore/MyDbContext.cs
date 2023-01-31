using EF_Identity;
using EFCoreModel.Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    public class MyDbContext:DbContext
    {
        public DbSet<Blog> Blogs { get; set; }


        public DbSet<Music> Music { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }


    }
}