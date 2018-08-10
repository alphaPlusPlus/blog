using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<Models.Blog> Blogs { get; set; }
        public DbSet<Models.Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"")
        }
    }
}
