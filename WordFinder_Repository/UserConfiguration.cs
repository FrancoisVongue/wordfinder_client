using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WordFinder_Domain.Models;

namespace WordFinder_Repository
{
    public class UserConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = 1,
                    FirstName = "Francois",
                    LastName = "Vongue"
                });
        }
    }
}