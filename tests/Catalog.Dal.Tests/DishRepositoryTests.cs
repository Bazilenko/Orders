using FluentAssertions;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories;
using Catalog.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.Dal.Tests;

public class DishRepositoryTests : IDisposable
{
    private readonly MyDbContext _context;
    private readonly DishRepository _sut;

    public DishRepositoryTests()
    {
        _context = DbContextFactory.Create();
        _sut = new DishRepository(_context);
    }

    [Fact]
    public async Task GetDishesByCategory_ExistingCategory_ReturnsDishesWithExplicitLoading()
    {
        // Arrange
        var category = TestDataBuilder.CreateCategory(name: "Italian");
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync(); 

        var dish1 = TestDataBuilder.CreateDish(name: "Pasta", categoryId: category.Id);
        var dish2 = TestDataBuilder.CreateDish(name: "Lasagna", categoryId: category.Id);
        await _context.Dishes.AddRangeAsync(dish1, dish2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetDishesByCategory(category.Id);

        // Assert
        result.Should().HaveCount(2);
        result.Should().AllSatisfy(d => d!.CategoryId.Should().Be(category.Id));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllDishesWithCategoriesLoaded()
    {
        // Arrange
        var category = TestDataBuilder.CreateCategory(name: "Drinks");
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        var dish = TestDataBuilder.CreateDish(name: "Cola", categoryId: category.Id);
        await _context.Dishes.AddAsync(dish);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().NotBeEmpty();
        var firstDish = result.First();
        firstDish.Category.Should().NotBeNull(); // Explicit/Eager loading
        firstDish.Category.Name.Should().Be("Drinks");
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsDishWithCategory()
    {
        // Arrange
        var category = TestDataBuilder.CreateCategory(name: "Burgers");
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        var dish = TestDataBuilder.CreateDish(name: "Cheeseburger", categoryId: category.Id);
        await _context.Dishes.AddAsync(dish);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();

        // Act
        var result = await _sut.GetByIdAsync(dish.Id);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Cheeseburger");
        result.Category.Should().NotBeNull(); // Check LoadAsync 
        result.Category.Name.Should().Be("Burgers");
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