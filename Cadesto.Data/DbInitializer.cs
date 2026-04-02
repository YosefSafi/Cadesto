using Cadesto.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadesto.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;   // DB has been seeded
        }

        var adminUser = new User
        {
            Email = "admin@cadesto.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"), // Placeholder hash
            FirstName = "Admin",
            LastName = "User",
            Role = "Admin"
        };

        context.Users.Add(adminUser);

        var house = new House
        {
            Name = "Luxury Villa",
            Address = "123 Ocean Drive, Miami, FL",
            Description = "A beautiful luxury villa by the ocean."
        };

        context.Houses.Add(house);
        context.SaveChanges();

        var unit1 = new Unit { Name = "Penthouse A", HouseId = house.Id };
        var unit2 = new Unit { Name = "Suite 101", HouseId = house.Id };

        context.Units.AddRange(unit1, unit2);
        context.SaveChanges();

        var listing = new Listing
        {
            Title = "Ocean View Penthouse",
            Description = "Spacious penthouse with stunning ocean views.",
            Price = 5000.00m,
            UnitId = unit1.Id
        };

        context.Listings.Add(listing);

        var image = new Image
        {
            Url = "https://images.unsplash.com/photo-1512917774080-9991f1c4c750",
            HouseId = house.Id
        };

        context.Images.Add(image);
        context.SaveChanges();
    }
}
