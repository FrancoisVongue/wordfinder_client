using Microsoft.EntityFrameworkCore;
using WordFinder_Domain.Models;

namespace WordFinder_Repository
{
    public class TagConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}