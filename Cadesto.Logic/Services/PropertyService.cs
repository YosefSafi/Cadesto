using Cadesto.Data;
using Cadesto.Data.Entities;
using Cadesto.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Cadesto.Logic.Services;

public interface IPropertyService
{
    Task<List<PropertyDto>> GetAllPropertiesAsync();
    Task<List<ListingDto>> GetAllListingsAsync();
    Task<bool> CreateHouseAsync(House house);
    // ... other CRUD methods
}

public class PropertyService : IPropertyService
{
    private readonly ApplicationDbContext _context;

    public PropertyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PropertyDto>> GetAllPropertiesAsync()
    {
        return await _context.Houses
            .Include(h => h.Images)
            .Select(h => new PropertyDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                Description = h.Description,
                ImageUrls = h.Images.Select(i => i.Url).ToList()
            })
            .ToListAsync();
    }

    public async Task<List<ListingDto>> GetAllListingsAsync()
    {
        return await _context.Listings
            .Include(l => l.Unit)
            .Select(l => new ListingDto
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                Price = l.Price,
                UnitId = l.UnitId,
                UnitName = l.Unit.Name
            })
            .ToListAsync();
    }

    public async Task<bool> CreateHouseAsync(House house)
    {
        _context.Houses.Add(house);
        await _context.SaveChangesAsync();
        return true;
    }
}
