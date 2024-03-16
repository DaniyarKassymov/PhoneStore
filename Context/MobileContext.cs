using Bogus;
using Lesson.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson.Context;

public class MobileContext : DbContext
{
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAuthRole> UserAuthRoles { get; set; }

    public MobileContext(DbContextOptions<MobileContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAuthRole>()
            .HasData(
                new List<UserAuthRole>
                {
                    new() { Name = "admin", Id = 1 },
                    new() { Name = "user", Id = 2 }
                });

        modelBuilder.Entity<User>()
            .HasData(
                new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@admin.admin",
                    Password = "admin",
                    UserAuthRoleId = 1
                });
                
        
        modelBuilder.Entity<Brand>()
            .HasData(new List<Brand>
                {
                    new() {Id = 1, Name = "Apple", Link = "https://apple.com"},
                    new() {Id = 2, Name = "Samsung", Link = "https://apple.com"},
                    new() {Id = 3, Name = "Xiaomi", Link = "https://apple.com"},
                    new() {Id = 4, Name = "Huawei", Link = "https://huawei.com"},
                });

        modelBuilder.Entity<User>()
            .HasData(new List<User>
            {
                new() {Id = Guid.NewGuid(), Age = 42, Name = "John", BrandId = 1},
                new() {Id = Guid.NewGuid(), Age = 32, Name = "Doe", BrandId = 1},
                new() {Id = Guid.NewGuid(), Age = 19, Name = "Harry", BrandId = 1},
                new() {Id = Guid.NewGuid(), Age = 45, Name = "Sam", BrandId = 1},
                new() {Id = Guid.NewGuid(), Age = 54, Name = "John", BrandId = 2}
            });

        var phoneIds = 1;
        var testPhones = new Faker<Phone>()
            .RuleFor(p => p.Id, _ => phoneIds++)
            .RuleFor(p => p.BrandId, f => f.Random.Short(1, 4))
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Finance.Random.Int(1000, 5000))
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.BatteryCapacity, f => f.Finance.Random.Short(3000, 6000))
            .RuleFor(p => p.DisplayType, f => f.Commerce.ProductMaterial())
            .RuleFor(p => p.PhotoPath, _ => "uploads/IPhone_16_pro_max.jpg");

        var generate = testPhones.Generate(100);
        modelBuilder.Entity<Phone>().HasData(generate);

        base.OnModelCreating(modelBuilder);
    }
}