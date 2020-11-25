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

            var sizes = new SizeOption[]
            {
                new SizeOption{Id=1, DisplayName="Do 50", CodeName="0-50"},
                new SizeOption{Id=2, DisplayName="50 - 100", CodeName="50-100"},
                new SizeOption{Id=3, DisplayName="100 - 150", CodeName="100-150"},
                new SizeOption{Id=4, DisplayName="150 - 200", CodeName="150-200"},
                new SizeOption{Id=5, DisplayName="200 - 250", CodeName="200-250"},
                new SizeOption{Id=6, DisplayName="250 in več", CodeName="250-999999"}
            };

            foreach (SizeOption e in sizes)
                context.Add(e);
            context.SaveChanges();

            var years = new YearOption[]
            {
                new YearOption{Id=1, DisplayName="Do 1949", CodeName="1900-1949"},
                new YearOption{Id=2, DisplayName="1950 - 1959", CodeName="1950-1959"},
                new YearOption{Id=3, DisplayName="1960 - 1969", CodeName="1960-1969"},
                new YearOption{Id=4, DisplayName="1970 - 1979", CodeName="1970-1979"},
                new YearOption{Id=5, DisplayName="1980 - 1989", CodeName="1980-1989"},
                new YearOption{Id=6, DisplayName="1990 - 1999", CodeName="1990-1999"},
                new YearOption{Id=7, DisplayName="2000 - 2009", CodeName="2000-2009"},
                new YearOption{Id=8, DisplayName="2010 - 2019", CodeName="2010-2019"},
                new YearOption{Id=9, DisplayName="2020 in več", CodeName="2020-2100"}
            };

            foreach (YearOption e in years)
                context.Add(e);
            context.SaveChanges();

            var prices = new PriceOption[]
            {
                new PriceOption{Id=1, DisplayName="Do 50.000", CodeName="0-50000", ListingType=1},
                new PriceOption{Id=2, DisplayName="50.000 - 100.000", CodeName="50000-100000", ListingType=1},
                new PriceOption{Id=3, DisplayName="100.000 - 150.000", CodeName="100000-150000", ListingType=1},
                new PriceOption{Id=4, DisplayName="150.000 - 200.000", CodeName="150000-200000", ListingType=1},
                new PriceOption{Id=5, DisplayName="200.000 - 400.000", CodeName="200000-400000", ListingType=1},
                new PriceOption{Id=6, DisplayName="400.000 - 600.000", CodeName="400000-600000", ListingType=1},
                new PriceOption{Id=7, DisplayName="600.000 in več", CodeName="600000-999999999", ListingType=1},
                new PriceOption{Id=8, DisplayName="Do 500", CodeName="0-500", ListingType=2},
                new PriceOption{Id=9, DisplayName="500 - 1.000", CodeName="500-1000", ListingType=2},
                new PriceOption{Id=10, DisplayName="1.000 - 1.500", CodeName="1000-1500", ListingType=2},
                new PriceOption{Id=11, DisplayName="1.500 - 2.000", CodeName="1500-2000", ListingType=2},
                new PriceOption{Id=12, DisplayName="2.000 - 3.000", CodeName="2000-3000", ListingType=2},
                new PriceOption{Id=13, DisplayName="3.000 - 4.000", CodeName="3000-4000", ListingType=2},
                new PriceOption{Id=14, DisplayName="4.000 in več", CodeName="4000-999999999", ListingType=2},
            };

            foreach (PriceOption e in prices)
                context.Add(e);
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
                new RealEstateGroup{Id=12, FullName="Garaža - Drugo", Group="Garaža", TypeId=0},
            };

            foreach (RealEstateGroup e in reGroups)
                context.RealEstateGroup.Add(e);
            context.SaveChanges();

            var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator", NormalizedName="ADMINISTRATOR"},
                new IdentityRole{Id="2", Name="User", NormalizedName="USER"}
            };

            foreach (IdentityRole r in roles)
            {
                context.Roles.Add(r);
            }

            var user1 = new ApplicationUser
            {
                Email = "janez@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "janeznovak",
                NormalizedUserName = "janeznovak".Normalize(),
                PhoneNumber = "+111111111111",
                ImageLink = "https://www.kindpng.com/picc/m/78-785827_user-profile-avatar-login-account-male-user-icon.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var user2 = new ApplicationUser
            {
                Email = "marija@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "marijanovak",
                NormalizedUserName = "marijanovak".Normalize(),
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
                UserName = "admin",
                NormalizedUserName = "admin".Normalize(),
                PhoneNumber = "+111111111111",
                ImageLink = "https://www.kindpng.com/picc/m/78-785827_user-profile-avatar-login-account-male-user-icon.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user1, "Testni123!");
                user1.PasswordHash = hashed;
                context.Users.Add(user1);
            }

            if (!context.Users.Any(u => u.UserName == user2.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user2, "Testni123!");
                user2.PasswordHash = hashed;
                context.Users.Add(user2);
            }

            if (!context.Users.Any(u => u.UserName == adminUser.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(adminUser, "Testni123!");
                adminUser.PasswordHash = hashed;
                context.Users.Add(adminUser);
            }
            context.SaveChanges();

            var userRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=adminUser.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user1.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user2.Id}
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
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Primorska ulica 10, 1000 Ljubljana", Size=102, Year=2000, ImageLink="https://miro.medium.com/max/10816/1*eO3CIaFBe7LMRePApDwSYA.jpeg", Description="Zelo blizu faksov in ZOO.", Price=100000, ListingType=1, GroupId=2, Owner=user1},
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Slovenska cesta 5, 1000 Ljubljana", Size=45, Year=2020, ImageLink="https://cache.100kvadratov.si/image/project/58/large_ljn/1/accamera1__5d2f222a8d257.jpg", Description="Center Ljubljane", Price=750, ListingType=2, GroupId=7, Owner=user1},
                new Listing{DateOfEntry=DateTime.Now, RegionId=12, Address="Dantejeva ulica 31, 6300 Piran", Size=50, Year=2021, ImageLink="https://i.imgur.com/hWfismm.gif", Description="V bližini nočnih klubov", Price=1000, ListingType=2, GroupId=7, Owner=user1},
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Hruševska cesta 140, 1261 Ljubljana", Size=140, Year=2016, ImageLink="https://img.nepremicnine.link/slonep_oglasi2/7520688.jpg", Description="Za obiskovalce je na voljo 6 parkirnih mest.", Price=208000, ListingType=1, GroupId=6, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Brdo, 1230 Domžale", Size=103, Year=2019, ImageLink="https://www.kras-nepremicnine.si/site/assets/files/1393/image1.jpeg", Description="Sodobno zasnovano.", Price=385900, ListingType=1, GroupId=6, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Brdnikova ulica 2, 1230 Domžale", Size=131, Year=2019, ImageLink="https://cache.100kvadratov.si/image/project/58/large_ljn/1/accamera1__5d2f222a8d257.jpg", Description="Pogled na Rožnik.", Price=390560, ListingType=1, GroupId=6, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Bežigrajska cesta 7, 1000 Ljubljana", Size=188, Year=2012, ImageLink="https://cache.100kvadratov.si/image/item/83/site/list_portal/4/img-4bd56aaebaf4bcfca80a2b79b368f5dc-v__5ebbde75109ae.jpg", Description="Pogled na Rožnik.", Price=690000, ListingType=1, GroupId=6, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Cesta na vič 1, 1000 Ljubljana", Size=130, Year=1968, ImageLink="https://miro.medium.com/max/10816/1*eO3CIaFBe7LMRePApDwSYA.jpeg", Description="Bližina gostilne.", Price=410000, ListingType=1, GroupId=1, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Srbska cesta 7, 1000 Ljubljana", Size=347, Year=1964, ImageLink="https://cache.100kvadratov.si/image/item/83/site/list_portal/4/img-4bd56aaebaf4bcfca80a2b79b368f5dc-v__5ebbde75109ae.jpg", Description="Adaptirana l. 2018", Price=680000, ListingType=1, GroupId=1, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=8, Address="Rožanska ulica 25, 1000 Ljubljana", Size=568, Year=1960, ImageLink="https://miro.medium.com/max/10816/1*eO3CIaFBe7LMRePApDwSYA.jpeg", Description="Odlično ohranjena", Price=1290000, ListingType=1, GroupId=1, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=3, Address="Pristava 13, 2393 Črna na Koroškem", Size=24, Year=1976, ImageLink="https://img.nepremicnine.link/slonep_oglasi2/7520688.jpg", Description="Odlično ohranjena", Price=200, ListingType=2, GroupId=6, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=3, Address="Pristava 13, 2393 Črna na Koroškem", Size=15, Year=1976, ImageLink="https://www.kras-nepremicnine.si/site/assets/files/1393/image1.jpeg", Description="Odlično ohranjena", Price=130, ListingType=2, GroupId=6, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=3, Address="Podgorska cesta, 2380 Slovenj Gradec", Size=15, Year=2005, ImageLink="https://cache.100kvadratov.si/image/item/83/site/list_portal/4/img-4bd56aaebaf4bcfca80a2b79b368f5dc-v__5ebbde75109ae.jpg", Description="Odlično ohranjena", Price=250, ListingType=2, GroupId=7, Owner=user2},
                new Listing{DateOfEntry=DateTime.Now, RegionId=12, Address="Frenkova ulica 10, 6280 Ankaran", Size=35, Year=2010, ImageLink="https://cache.100kvadratov.si/image/project/58/large_ljn/1/accamera1__5d2f222a8d257.jpg", Description="Prenovljena kuhinja.", Price=390, ListingType=2, GroupId=6, Owner=user1},
                new Listing{DateOfEntry=DateTime.Now, RegionId=12, Address="Frenkova ulica 13, 6280 Ankaran", Size=45, Year=2013, ImageLink="https://img.nepremicnine.link/slonep_oglasi2/7520688.jpg", Description="Pogled na morje.", Price=490, ListingType=2, GroupId=7, Owner=user1},
                new Listing{DateOfEntry=DateTime.Now, RegionId=12, Address="Dantejeva ulica 33, 6300 Piran", Size=47, Year=2010, ImageLink="https://miro.medium.com/max/10816/1*eO3CIaFBe7LMRePApDwSYA.jpeg", Description="Bližina restavracij.", Price=400, ListingType=2, GroupId=5, Owner=user1},
                new Listing{DateOfEntry=DateTime.Now, RegionId=12, Address="Podgorska cesta, 2380 Slovenj Gradec", Size=30, Year=2013, ImageLink="https://www.kras-nepremicnine.si/site/assets/files/1393/image1.jpeg", Description="Ima klimo.", Price=300, ListingType=2, GroupId=5, Owner=user1}
            };

            foreach (Listing e in listings)
                context.Listings.Add(e);
            context.SaveChanges();

            var favorites = new Favorite[]
            {
                new Favorite{ListingId = listings[4].Id, UserId=user1.Id},
                new Favorite{ListingId = listings[7].Id, UserId=user1.Id},
                new Favorite{ListingId = listings[11].Id, UserId=user1.Id},
                new Favorite{ListingId = listings[16].Id, UserId=user1.Id},
                new Favorite{ListingId = listings[15].Id, UserId=user1.Id},
                new Favorite{ListingId = listings[13].Id, UserId=user1.Id},
                new Favorite{ListingId = listings[0].Id, UserId=user2.Id},
                new Favorite{ListingId = listings[2].Id, UserId=user2.Id},
                new Favorite{ListingId = listings[6].Id, UserId=user2.Id},
                new Favorite{ListingId = listings[10].Id, UserId=user2.Id}
            };

            foreach (Favorite e in favorites)
                context.Add(e);
            context.SaveChanges();
        }
    }
}