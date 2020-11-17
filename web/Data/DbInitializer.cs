using web.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EhomeContext context)
        {
            context.Database.EnsureCreated();

            if(context.ListingType.Any())
                return; // Db already seeded

            var listingTypes = new ListingType[]
            {
                new ListingType{Type="Nakup"},
                new ListingType{Type="Najem"},
                new ListingType{Type="Prodaja"},
                new ListingType{Type="Oddaja"},
            };

            foreach (ListingType e in listingTypes)
                context.ListingType.Add(e);
            context.SaveChanges();

            var reTypes = new RealEstateType[]
            {
                new RealEstateType{Type="Hisa"},
                new RealEstateType{Type="Stanovanje"},
                new RealEstateType{Type="Garaza"},
                new RealEstateType{Type="Posest"}
            };

            foreach (RealEstateType e in reTypes)
                context.RealEstateType.Add(e);
            context.SaveChanges();

            var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator"},
                new IdentityRole{Id="2", Name="User"}
            };

            foreach (IdentityRole r in roles)
            {
                context.Roles.Add(r);
            }

            var user = new ApplicationUser
            {
                Email = "test@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "test@example.com",
                NormalizedUserName = "test@example.com",
                PhoneNumber = "+111111111111",
                ImageLink = "https://www.kindpng.com/picc/m/78-785827_user-profile-avatar-login-account-male-user-icon.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var adminUser = new ApplicationUser
            {
                Email = "admin@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "admin@example.com",
                NormalizedUserName = "admin@example.com",
                PhoneNumber = "+111111111111",
                ImageLink = "https://www.kindpng.com/picc/m/78-785827_user-profile-avatar-login-account-male-user-icon.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "Testni123!");
                user.PasswordHash = hashed;
                context.Users.Add(user);
            }

            if (!context.Users.Any(u => u.UserName == adminUser.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "Testni123!");
                adminUser.PasswordHash = hashed;
                context.Users.Add(adminUser);
            }
            context.SaveChanges();

            var userRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=adminUser.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id}
            };

            foreach (IdentityUserRole<string> r in userRoles)
                context.UserRoles.Add(r);
            context.SaveChanges();

            var listings = new Listing[]
            {
                new Listing{DateOfEntry=DateTime.Now, Region="Ljubljana", Address="Primorska ulica 10, 1000 Ljubljana", Size=102, Year=2000, ImageLink="https://i.imgur.com/hWfismm.gif", Description="This is description.", Price=100000, ListingType="Prodaja", RealEstateType="Hisa", Owner=user},
                new Listing{DateOfEntry=DateTime.Now, Region="Ljubljana", Address="Slovenska cesta 5, 1000 Ljubljana", Size=45, Year=2020, ImageLink="https://i.imgur.com/hWfismm.gif", Description="This is description.", Price=750, ListingType="Oddaja", RealEstateType="Stanovanje", Owner=user},
                new Listing{DateOfEntry=DateTime.Now, Region="Piran", Address="Dantejeva ulica 31, 6300 Piran", Size=50, Year=2021, ImageLink="https://i.imgur.com/hWfismm.gif", Description="This is description.", Price=1000, ListingType="Oddaja", RealEstateType="Stanovanje", Owner=user},
            };

            foreach (Listing e in listings)
                context.Listings.Add(e);
            context.SaveChanges();
        }
    }
}