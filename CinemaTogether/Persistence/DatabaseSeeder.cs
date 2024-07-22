using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Utils.Cryptography;

namespace Persistence
{
    public class DatabaseSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Id = Guid.Parse("A0440A78-41CC-419C-B05F-B511EE65D28A"),
                        Email = "d.krumkachev@gmail.com",
                        Username = "Test1",
                        PasswordHash = Hasher.Hash("A-123123"),
                        TwoFactorEnabled = false,
                        Name = "Robby Krieger",
                        DateOfBirth = DateTime.Now.AddYears(-25),
                        Gender = Domain.Enums.Gender.Male,
                        CityId = Guid.Parse("9240D9F4-EDD0-458A-3A59-08DCAA8F590A")
                    },
                    new User
                    {
                        Id = Guid.Parse("DDC7D332-E194-4E6E-A77D-C1EBCE29E746"),
                        Email = "artemij1258@gmail.com",
                        Username = "Test2",
                        PasswordHash = Hasher.Hash("A-123123"),
                        TwoFactorEnabled = false,
                        Name = "John Densmore",
                        DateOfBirth = DateTime.Now.AddYears(-23),
                        Gender = Domain.Enums.Gender.Male,
                        CityId = Guid.Parse("9240D9F4-EDD0-458A-3A59-08DCAA8F590A")
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
