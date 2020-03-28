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
            
            modelBuilder.Entity<Word>()
                .HasData(
                    new Word() {Content = "hello", UserId = 1L, Id = 1},
                    new Word() {Content = "world", UserId = 1L, Id = 2},
                    new Word() {Content = "the", UserId = 1L, Id = 3},
                    new Word() {Content = "star", UserId = 1L, Id = 4});
        }
    }
}