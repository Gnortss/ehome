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

            var listings = new Listing[]
            {
                new Listing{DateOfEntry=DateTime.Now, Region="Ljubljana", Address="Primorska ulica 10, 1000 Ljubljana", Size=102, Year=2000, ImageLink="https://i.imgur.com/hWfismm.gif", Description="This is description.", Price=100000, ListingType="Prodaja", RealEstateType="Hisa"}
            };

            foreach (Listing e in listings)
                context.Listings.Add(e);
            context.SaveChanges();
        }
    }
}