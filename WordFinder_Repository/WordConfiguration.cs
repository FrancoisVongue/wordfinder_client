using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WordFinder_Domain.Models;

namespace WordFinder_Repository
{
    public class WordConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasOne(w => w.Topic)
                .WithMany(t => t.Words)
                .IsRequired(false);
            
        }
    }
}