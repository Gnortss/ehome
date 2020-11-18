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
                new ListingType{Id=1, Type="Prodaja"},
                new ListingType{Id=2, Type="Oddaja"}
            };

            foreach (ListingType e in listingTypes)
                context.ListingType.Add(e);
            context.SaveChanges();

            var reTypes = new RealEstateType[]
            {
                new RealEstateType{Id=0, Type="Drugo"},
                new RealEstateType{Id=1, Type="Samostojna"},
                new RealEstateType{Id=2, Type="Vrstna"},
                new RealEstateType{Id=3, Type="Atrijska"},
                new RealEstateType{Id=4, Type="Apartma"},
                new RealEstateType{Id=5, Type="Garsonjera"},
                new RealEstateType{Id=6, Type="1-sobno"},
                new RealEstateType{Id=7, Type="Zazidljiva"},
                new RealEstateType{Id=8, Type="Nezazidljiva"}
            };

            foreach (RealEstateType e in reTypes)
                context.RealEstateType.Add(e);
            context.SaveChanges();

            var reGroups = new RealEstateGroup[]
            {
                new RealEstateGroup{Id=1, FullName="Hiša - Samostojna", Group="Hiša", TypeId=1},
                new RealEstateGroup{Id=2, FullName="Hiša - Vrstna", Group="Hiša", TypeId=2},
                new RealEstateGroup{Id=3, FullName="Hiša - Atrijska", Group="Hiša", TypeId=3},
                new RealEstateGroup{Id=4, FullName="Hiša - Drugo", Group="Hiša", TypeId=0},
                new RealEstateGroup{Id=5, FullName="Stanovanje - Apartma", Group="Stanovanje", TypeId=4},
                new RealEstateGroup{Id=6, FullName="Stanovanje - Garsonjera", Group="Stanovanje", TypeId=4},
                new RealEstateGroup{Id=7, FullName="Stanovanje - 1-sobno", Group="Stanovanje", TypeId=5},
                new RealEstateGroup{Id=8, FullName="Stanovanje - Drugo", Group="Stanovanje", TypeId=0},
                new RealEstateGroup{Id=9, FullName="Posest - Zazidljiva", Group="Posest", TypeId=6},
                new RealEstateGroup{Id=10, FullName="Posest - Nezazidljiva", Group="Posest", TypeId=7},
                new RealEstateGroup{Id=11, FullName="Posest - Drugo", Group="Posest", TypeId=8},
                new RealEstateGroup{Id=12, FullName="Garaza - Drugo", Group="Garaza", TypeId=0},
            };

            foreach (RealEstateGroup e in reGroups)
                context.RealEstateGroup.Add(e);
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

            var regions = new Region[]
            {
                new Region{Id=1, Name="Pomurska regija"},
                new Region{Id=2, Name="Podravska regija"},
                new Region{Id=3, Name="Koroška regija"},
                new Region{Id=4, Name="Savinjska regija"},
                new Region{Id=5, Name="Zasavska regija"},
                new Region{Id=6, Name="Posavska regija"},
                new Region{Id=7, Name="Jugovzhodna regija"},
                new Region{Id=8, Name="Osrednjeslovenska regija"},
                new Region{Id=9, Name="Gorenjska regija"},
                new Region{Id=10, Name="Primorsko-notranjska regija"},
                new Region{Id=11, Name="Goriška regija"},
                new Region{Id=12, Name="Obalno-kraška regija"}
            };

            foreach (Region e in regions)
                context.Region.Add(e);
            context.SaveChanges();

            var listings = new Listing[]
            {
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Primorska ulica 10, 1000 Ljubljana", Size=102, Year=2000, ImageLink="https://i.imgur.com/hWfismm.gif", Description="This is description.", Price=100000, ListingType=1, GroupId=2, Owner=user},
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Slovenska cesta 5, 1000 Ljubljana", Size=45, Year=2020, ImageLink="https://i.imgur.com/hWfismm.gif", Description="This is description.", Price=750, ListingType=2, GroupId=7, Owner=user},
                new Listing{DateOfEntry=DateTime.Now, RegionId=12, Address="Dantejeva ulica 31, 6300 Piran", Size=50, Year=2021, ImageLink="https://i.imgur.com/hWfismm.gif", Description="This is description.", Price=1000, ListingType=2, GroupId=7, Owner=user},
            };

            foreach (Listing e in listings)
                context.Listings.Add(e);
            context.SaveChanges();
        }
    }
}