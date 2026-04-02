using System.ComponentModel.DataAnnotations;

namespace Cadesto.Data.Entities;

public class House
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Unit> Units { get; set; } = new();
    public List<Image> Images { get; set; } = new();
}

public class Unit
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public int HouseId { get; set; }
    public House House { get; set; } = null!;
    public Listing? Listing { get; set; }
    public List<Tenant> Tenants { get; set; } = new();
}

public class Listing
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = null!;
}

public class Tenant
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    public int? UnitId { get; set; }
    public Unit? Unit { get; set; }
    public string? IdentityUserId { get; set; }
}

public class Image
{
    public int Id { get; set; }
    [Required]
    public string Url { get; set; } = string.Empty;
    public int HouseId { get; set; }
    public House House { get; set; } = null!;
}

public class Invitation
{
    public int Id { get; set; }
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Token { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
    public bool IsUsed { get; set; }
}

public class User
{
    public int Id { get; set; }
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = "Tenant"; // Admin or Tenant
}
