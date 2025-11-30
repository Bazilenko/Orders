

using Catalog.Bll.Mapper.Profiles;
using Catalog.Bll.Services;
using Catalog.Bll.Services.Interfaces;
using Catalog.Dal.Context;
using Catalog.Dal.Repositories;
using Catalog.Dal.Repositories.Interfaces;
using Catalog.Dal.UOW;
using Catalog.Dal.UOW.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogDb"))
    );



// Add services to the container.
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDishService,  DishService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(CategoryProfile));
builder.Services.AddAutoMapper(typeof(DishProfile));
builder.Services.AddAutoMapper(typeof(RestaurantProfile));
builder.Services.AddAutoMapper(typeof(ContactProfile));
builder.Services.AddAutoMapper(typeof(AddressProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//migration for data base in container
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    db.Database.Migrate();
}

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
