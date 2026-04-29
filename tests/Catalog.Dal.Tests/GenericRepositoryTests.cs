using FluentAssertions;
using Catalog.Dal.Entities;      
using Catalog.Dal.Repositories;  
using Catalog.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.Dal.Tests;

public class GenericRepositoryTests : IDisposable
{
    private readonly MyDbContext _context;
    private readonly GenericRepository<Restaurant> _sut;

    public GenericRepositoryTests()
    {
        _context = DbContextFactory.Create();
        _sut = new GenericRepository<Restaurant>(_context);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsRestaurant()
    {
        // Arrange 
        var restaurant = TestDataBuilder.CreateRestaurant();
        await _sut.AddAsync(restaurant);

        // Act 
        var result = await _sut.GetByIdAsync(restaurant.Id);

        // Assert 
        result.Should().NotBeNull();
        result.Id.Should().Be(restaurant.Id);
        result.Name.Should().Be(restaurant.Name);
    }

    [Fact]
    public async Task GetAllAsync_WhenEntitiesExist_ReturnsAllRestaurants()
    {
        // Arrange
        var restaurants = TestDataBuilder.CreateRestaurants(3);
        await _sut.AddRangeAsync(restaurants);
        await _context.SaveChangesAsync(); 

        // Act
        var result = await _sut.GetAllAsync();

        // Assert 
        result.Should().HaveCount(3);
        result.Should().NotContainNulls();
    }

    [Fact]
    public async Task AddAsync_ValidEntity_PersistsInDatabase()
    {
        // Arrange
        var restaurant = TestDataBuilder.CreateRestaurant();

        // Act
        var result = await _sut.AddAsync(restaurant);

        // Assert
        result.Should().NotBeNull();
        var dbResult = await _context.Set<Restaurant>().FindAsync(restaurant.Id);
        dbResult.Should().NotBeNull();
        dbResult!.Name.Should().Be(restaurant.Name);
    }

    [Fact]
    public async Task UpdateAsync_ExistingEntity_UpdatesDatabase()
    {
        // Arrange
        var restaurant = TestDataBuilder.CreateRestaurant();
        await _sut.AddAsync(restaurant);
        
        // Act
        restaurant.Name = "Updated Name";
        await _sut.UpdateAsync(restaurant);

        // Assert
        var dbResult = await _context.Set<Restaurant>().FindAsync(restaurant.Id);
        dbResult!.Name.Should().Be("Updated Name");
    }

    [Fact]
    public async Task DeleteAsync_ExistingEntity_RemovesFromDatabase()
    {
        // Arrange
        var restaurant = TestDataBuilder.CreateRestaurant();
        await _sut.AddAsync(restaurant);

        // Act
        await _sut.DeleteAsync(restaurant);
        await _context.SaveChangesAsync(); 

        // Assert
        var dbResult = await _context.Set<Restaurant>().FindAsync(restaurant.Id);
        dbResult.Should().BeNull();
    }

    [Fact]
    public async Task GetPagedDataAsync_ReturnsCorrectPage()
    {
        // Arrange
        var restaurants = TestDataBuilder.CreateRestaurants(10); // Створюємо 10 записів
        await _sut.AddRangeAsync(restaurants);
        await _context.SaveChangesAsync();

        // Act
        var (items, totalCount) = await _sut.GetPagedDataAsync(1, 3, "Name", "asc");

        // Assert
        items.Should().HaveCount(3);
        totalCount.Should().Be(10);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Act
        var result = await _sut.GetByIdAsync(999);

        // Assert
        result.Should().BeNull(); 
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted(); 
        _context.Dispose();
    }
}