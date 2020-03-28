using System;
using Microsoft.EntityFrameworkCore;
using WordFinder_Domain.Models;

namespace WordFinder_Repository
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<WordTag> WordTags { get; set; }
        public DbSet<Translation> Translations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            WordTagConfiguration.Configure(modelBuilder);
            TagConfiguration.Configure(modelBuilder);
            UserConfiguration.Configure(modelBuilder);
            WordConfiguration.Configure(modelBuilder);
        }
        
        public ApiContext(DbContextOptions<ApiContext> options) 
            : base(options) {}
    }
}