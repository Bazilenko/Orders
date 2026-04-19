using Catalog.Dal.Context;
using Microsoft.EntityFrameworkCore;
public static class DbContextFactory
{
 public static MyDbContext Create()
 {
    var options = new DbContextOptionsBuilder<MyDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique db
        .Options;

    var context = new MyDbContext(options);
    context.Database.EnsureCreated();
    return context;
 }
}
