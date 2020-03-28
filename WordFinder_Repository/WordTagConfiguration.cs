using Microsoft.EntityFrameworkCore;
using WordFinder_Domain.Models;

namespace WordFinder_Repository
{
    public class WordTagConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordTag>()
                .HasKey(t => new {t.TagId, t.WordId});
            modelBuilder.Entity<WordTag>()
                .HasOne(wt => wt.Word)
                .WithMany(w => w.WordTags)
                .HasForeignKey(wt => wt.WordId);
            modelBuilder.Entity<WordTag>()
                .HasOne(wt => wt.Tag)
                .WithMany(w => w.WordTags)
                .HasForeignKey(wt => wt.TagId);
        }
    }
}