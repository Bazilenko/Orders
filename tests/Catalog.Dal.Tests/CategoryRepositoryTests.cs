using FluentAssertions;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories;
using Catalog.Dal.Context;
using Xunit;

namespace Catalog.Dal.Tests;

public class CategoryRepositoryTests : IDisposable
{
    private readonly MyDbContext _context;
    private readonly CategoryRepository _sut;

    public CategoryRepositoryTests()
    {
        _context = DbContextFactory.Create();
        _sut = new CategoryRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldSaveCategoryWithCorrectData()
    {
        // Arrange
        var category = TestDataBuilder.CreateCategory(name: "Суші");

        // Act
        await _sut.AddAsync(category);
        await _context.SaveChangesAsync();

        // Assert
        var result = await _sut.GetByIdAsync(category.Id);
        result.Should().NotBeNull();
        result!.Name.Should().Be("Суші");
        result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task GetAllAsync_WhenMultipleCategoriesExist_ShouldReturnAll()
    {
        // Arrange
        var categories = new List<Category>
        {
            TestDataBuilder.CreateCategory(name: "Десерти"),
            TestDataBuilder.CreateCategory(name: "Напої")
        };
        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().HaveCountGreaterThanOrEqualTo(2);
        result.Select(c => c.Name).Should().Contain(new[] { "Десерти", "Напої" });
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}