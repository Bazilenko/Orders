using FluentAssertions;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories;
using Catalog.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.Dal.Tests;

public class RestaurantRepositoryTests : IDisposable
{
    private readonly MyDbContext _context;
    private readonly RestaurantRepository _sut;

    public RestaurantRepositoryTests()
    {
        _context = DbContextFactory.Create();
        _sut = new RestaurantRepository(_context);
    }

    [Fact]
    public async Task GetByIdWithFullInfo_ExistingId_ReturnsRestaurantWithAddressesAndContacts()
    {
        // Arrange
        var restaurant = TestDataBuilder.CreateRestaurant(name: "Full Info Resto");
        
        restaurant.Addresses.Clear();
        restaurant.Contacts.Clear();

        restaurant.Addresses.Add(TestDataBuilder.CreateAddress(city: "Kyiv", street: "Main St", building: "123"));
        restaurant.Contacts.Add(TestDataBuilder.CreateContact(value: "0991234567"));

        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetByIdWithFullInfo(restaurant.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Addresses.Should().HaveCount(1);
        result.Addresses.First().BuildingNumber.Should().Be("123");
        result.Contacts.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetByIdWithFullInfo_ExistingIdWithoutDetails_ReturnsRestaurantWithEmptyCollections()
    {
        // Arrange
        var restaurant = TestDataBuilder.CreateRestaurant(name: "Empty Resto");
        
        restaurant.Addresses.Clear();
        restaurant.Contacts.Clear();

        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetByIdWithFullInfo(restaurant.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Addresses.Should().BeEmpty();
        result.Contacts.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByRatingAsync_ShouldReturnOnlyMatchingRestaurants()
    {
        // Arrange
        var restaurants = new List<Restaurant>
        {
            TestDataBuilder.CreateRestaurant(id: 0, name: "Good One", rating: 4.5m),
            TestDataBuilder.CreateRestaurant(id: 0, name: "Another Good", rating: 4.0m),
            TestDataBuilder.CreateRestaurant(id: 0, name: "Bad One", rating: 2.0m)
        };
        await _context.Restaurants.AddRangeAsync(restaurants);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetByRatingAsync(4.0m);

        // Assert
        result.Should().HaveCount(2);
        result.Should().OnlyContain(r => r.Rating >= 4.0m);
    }

    [Fact]
    public async Task GetByCityAsync_ShouldReturnRestaurantsInSpecificCity()
    {
        // Arrange
        var r1 = TestDataBuilder.CreateRestaurant(name: "Kyiv Resto");
        r1.Addresses.Clear();
        r1.Addresses.Add(TestDataBuilder.CreateAddress(city: "Kyiv"));

        var r2 = TestDataBuilder.CreateRestaurant(name: "Lviv Resto");
        r2.Addresses.Clear();
        r2.Addresses.Add(TestDataBuilder.CreateAddress(city: "Lviv"));

        await _context.Restaurants.AddRangeAsync(r1, r2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetByCityAsync("Kyiv");

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Kyiv Resto");
    }

    [Fact]
public async Task GetActiveAsync_WhenActiveEntitiesExist_ReturnsOnlyActive()
{
    var restaurants = new List<Restaurant>
    {
        TestDataBuilder.CreateRestaurant(name: "Active 1", isActive: true),
        TestDataBuilder.CreateRestaurant(name: "Active 2", isActive: true),
        TestDataBuilder.CreateRestaurant(name: "Inactive", isActive: false)
    };
    await _context.Restaurants.AddRangeAsync(restaurants);
    await _context.SaveChangesAsync();

    // Act
    var result = await _sut.GetActiveAsync();

    // Assert
    result.Should().HaveCount(2);
    result.Should().OnlyContain(e => e.IsActive);
}

[Fact]
public async Task GetActiveAsync_WhenOnlyInactiveEntitiesExist_ReturnsEmpty()
{
    // Arrange
    var restaurants = new List<Restaurant>
    {
        TestDataBuilder.CreateRestaurant(name: "Inactive 1", isActive: false),
        TestDataBuilder.CreateRestaurant(name: "Inactive 2", isActive: false)
    };
    await _context.Restaurants.AddRangeAsync(restaurants);
    await _context.SaveChangesAsync();

    // Act
    var result = await _sut.GetActiveAsync();

    // Assert
    result.Should().BeEmpty(); 
}


[Fact]
public async Task GetActiveAsync_WhenDatabaseIsEmpty_ReturnsEmpty()
{
    // Arrange

    // Act
    var result = await _sut.GetActiveAsync();

    // Assert
    result.Should().BeEmpty();
}

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}