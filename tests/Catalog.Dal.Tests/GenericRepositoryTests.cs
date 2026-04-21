using FluentAssertions;
using Catalog.Dal.Entities;      
using Catalog.Dal.Repositories;  
using Catalog.Dal.Context;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
public class GenericRepositoryTests : IDisposable
{
    private readonly MyDbContext _context;
    private readonly GenericRepository<Restaurant> _sut; // System Under Test [cite: 194]

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
        await _context.Set<Restaurant>().AddAsync(restaurant);
        await _context.SaveChangesAsync();

        // Act 
        var result = await _sut.GetByIdAsync(1);

        // Assert 
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be(restaurant.Name);
    }

    [Fact]
    public async Task GetAllAsync_WhenEntitiesExist_ReturnsAllRestaurants()
    {
        // Arrange
        var restaurants = TestDataBuilder.CreateRestaurants(3);
        await _context.Set<Restaurant>().AddRangeAsync(restaurants);
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
        await _sut.AddAsync(restaurant);

        // Assert
        var dbResult = await _context.Set<Restaurant>().FindAsync(restaurant.Id);
        dbResult.Should().NotBeNull();
        dbResult!.Name.Should().Be(restaurant.Name);
    }

    [Fact]
    public async Task DeleteAsync_ExistingEntity_RemovesFromDatabase()
    {
        // Arrange
        var restaurant = TestDataBuilder.CreateRestaurant(id: 10);
        await _context.Set<Restaurant>().AddAsync(restaurant);
        await _context.SaveChangesAsync();

        // Act
        await _sut.DeleteAsync(restaurant);
        await _context.SaveChangesAsync(); 

        // Assert
        var dbResult = await _context.Set<Restaurant>().FindAsync(10);
        dbResult.Should().BeNull();
    }

    [Fact]
public async Task GetByIdAsync_NonExistingId_ReturnsNull()
{
    // Arrange

    // Act
    var result = await _sut.GetByIdAsync(999);

    // Assert
    result.Should().BeNull(); 
}
[Fact]
public async Task GetAllAsync_EmptyDatabase_ReturnsEmptyCollection()
{
    // Arrange

    // Act
    var result = await _sut.GetAllAsync();

    // Assert
    result.Should().NotBeNull(); 
    result.Should().BeEmpty();   
}

[Fact]
public async Task DeleteAsync_NonExistingEntity_ThrowsException()
{
    // Arrange
    var nonExistingRestaurant = TestDataBuilder.CreateRestaurant(id: 999);

    // Act
    var act = async () => 
    {
        await _sut.DeleteAsync(nonExistingRestaurant);
        await _context.SaveChangesAsync();
    };

    // Assert
    await act.Should().ThrowAsync<DbUpdateConcurrencyException>(); 
}

    public void Dispose() => _context.Dispose(); 
}