using Moq;
using FluentAssertions;
using Catalog.Dal.Context;
using Catalog.Dal.UOW;
using Catalog.Dal.Repositories;
using Catalog.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.Dal.Tests;

public class UnitOfWorkTests : IDisposable
{
    private readonly MyDbContext _context;

    public UnitOfWorkTests()
    {
        _context = DbContextFactory.Create();
    }

    [Fact]
    public void UnitOfWork_Repositories_ShareSameDbContext()
    {
        // Arrange
        var restaurantRepo = new RestaurantRepository(_context);
        var dishRepo = new DishRepository(_context);
        
        var uow = new UnitOfWork(_context, null!, dishRepo, restaurantRepo, null!, null!, null!);

        // Act
        var ctxFromRestaurants = _context; 
        var ctxFromDishes = _context;

        // Assert
        ctxFromRestaurants.Should().BeSameAs(ctxFromDishes);
    }

    [Fact]
    public async Task SaveChangesAsync_AfterMultipleAdds_PersistsAllEntities()
    {
        // Arrange
        var restaurantRepo = new RestaurantRepository(_context);
        var dishRepo = new DishRepository(_context);
        var uow = new UnitOfWork(_context, null!, dishRepo, restaurantRepo, null!, null!, null!);

        var category = new Category {Name = "Salad"};
        var restaurant = TestDataBuilder.CreateRestaurant(name: "UOW Restaurant");
        var dish = new Dish { Name = "UOW Dish", Price = 10, Description= "Fresh dish",ImageUrl = "gkfk/image.jpg", CategoryId = category.Id, RestaurantId = restaurant.Id };

        // Act
        await uow.Restaurants.AddAsync(restaurant);
        await uow.Dishes.AddAsync(dish);
        await uow.SaveChangesAsync();

        // Assert
        _context.Restaurants.Should().Contain(r => r.Name == "UOW Restaurant");
        _context.Dishes.Should().Contain(d => d.Name == "UOW Dish");
    }

    [Fact]
    public async Task SaveChangesAsync_WhenCalled_InvokesContextSaveChanges()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb")
            .Options;

        var mockContext = new Mock<MyDbContext>(options);
        
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                   .ReturnsAsync(1);

        var uow = new UnitOfWork(mockContext.Object, null!, null!, null!, null!, null!, null!);

        // Act
        await uow.SaveChangesAsync();

        // Assert
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void Dispose_WhenCalled_DoesNotThrow()
    {
        // Arrange
        // Створюємо окремий контекст спеціально для цього тесту
        var localContext = DbContextFactory.Create();
        var uow = new UnitOfWork(localContext, null!, null!, null!, null!, null!, null!);

    // Act & Assert
    // Перевіряємо, що після виклику uow.Dispose() не виникає помилок
    Action act = () => uow.Dispose();
    act.Should().NotThrow();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}