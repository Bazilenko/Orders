using FluentAssertions;

using Catalog.Dal.Entities;

using Catalog.Dal.Repositories;

using Catalog.Dal.Context;

using Xunit;



namespace Catalog.Dal.Tests;



public class DishOptionRepositoryTests : IDisposable

{

    private readonly MyDbContext _context;

    private readonly DishOptionRepository _sut;



    public DishOptionRepositoryTests()

    {

        _context = DbContextFactory.Create();

        _sut = new DishOptionRepository(_context);

    }



    [Fact]

    public async Task AddAsync_WithValidDishId_ShouldSaveOption()

    {

        // Arrange

        var dish = TestDataBuilder.CreateDish(name: "Піца");

        await _context.Dishes.AddAsync(dish);

        await _context.SaveChangesAsync();



        var option = TestDataBuilder.CreateDishOption(name: "Подвійний бортик", price: 50.50m, dishId: dish.Id);



        // Act

        await _sut.AddAsync(option);

        await _context.SaveChangesAsync();



        // Assert

        var result = await _sut.GetByIdAsync(option.Id);

        result.Should().NotBeNull();

        result!.ModifierPrice.Should().Be(50.50m);

        result.DishId.Should().Be(dish.Id);

    }



    [Fact]

    public async Task GetByIdAsync_WhenDishIsDeleted_CheckCascadeBehavior()

    {

        // Arrange

        var dish = TestDataBuilder.CreateDish();

        await _context.Dishes.AddAsync(dish);

        await _context.SaveChangesAsync();



        var option = TestDataBuilder.CreateDishOption(dishId: dish.Id);

        await _sut.AddAsync(option);

        await _context.SaveChangesAsync();



        // Act

        _context.Dishes.Remove(dish); 

        await _context.SaveChangesAsync();



        // Assert

        var result = await _sut.GetByIdAsync(option.Id);

        result.Should().BeNull();

    }



    public void Dispose()

    {

        _context.Database.EnsureDeleted();

        _context.Dispose();

    }

}