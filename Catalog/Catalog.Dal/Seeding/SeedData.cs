using Bogus;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.UOW.Interfaces;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static async Task SeedAsync(IUnitOfWork _unitOfWork)
    {
         

        var random = new Random();

       
        var categoryFaker = new Faker<Category>()
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First());

        var categories = categoryFaker.Generate(8);

        await _unitOfWork.Categories.AddRangeAsync(categories);
        await _unitOfWork.SaveChangesAsync();

       
        var restaurantFaker = new Faker<Restaurant>()
            .RuleFor(r => r.Name, f => f.Company.CompanyName())
            .RuleFor(r => r.Description, f => f.Lorem.Sentence(20))
            .RuleFor(r => r.ImageUrl, f => f.Image.PicsumUrl())
            .RuleFor(r => r.Rating, f => f.Random.Decimal(3.5m, 5m));

        var restaurants = restaurantFaker.Generate(5);
        await _unitOfWork.Restaurants.AddRangeAsync(restaurants);
        await _unitOfWork.SaveChangesAsync();


        var addressFaker = new Faker<Address>()
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Street, f => f.Address.StreetName())
            .RuleFor(a => a.BuildingNumber, f => f.Address.BuildingNumber())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode("#####"))
            .RuleFor(a => a.RestaurantId, f => f.PickRandom(restaurants).Id);

        var addresses = addressFaker.Generate(10);
        await _unitOfWork.Addresses.AddRangeAsync(addresses);

      
        var contactTypes = new[] { "Phone", "Email", "Instagram", "Facebook" };

        var contactFaker = new Faker<Contact>()
            .RuleFor(c => c.Type, f => f.PickRandom(contactTypes))
            .RuleFor(c => c.Value, (f, c) =>
                c.Type switch
                {
                    "Phone" => f.Phone.PhoneNumber(),
                    "Email" => f.Internet.Email(),
                    "Instagram" => "@" + f.Internet.UserName(),
                    "Facebook" => "fb.com/" + f.Internet.UserName(),
                    _ => ""
                })
            .RuleFor(c => c.RestaurantId, f => f.PickRandom(restaurants).Id);

        var contacts = contactFaker.Generate(20);
        await _unitOfWork.Contacts.AddRangeAsync(contacts);

        await _unitOfWork.SaveChangesAsync();

        
        var dishFaker = new Faker<Dish>()
            .RuleFor(d => d.Name, f => f.Commerce.ProductName())
            .RuleFor(d => d.Description, f => f.Lorem.Sentence())
            .RuleFor(d => d.Price, f => f.Random.Decimal(5m, 40m))
            .RuleFor(d => d.ImageUrl, f => f.Image.PicsumUrl())
            .RuleFor(d => d.CategoryId, f => f.PickRandom(categories).Id)
            .RuleFor(d => d.RestaurantId, f => f.PickRandom(restaurants).Id);

        var dishes = dishFaker.Generate(40);
        await _unitOfWork.Dishes.AddRangeAsync(dishes);
        await _unitOfWork.SaveChangesAsync();

        
        var optionFaker = new Faker<DishOption>()
            .RuleFor(o => o.Name, f => f.Commerce.ProductMaterial())
            .RuleFor(o => o.ModifierPrice, f => f.Random.Decimal(0.5m, 10m))
            .RuleFor(o => o.DishId, f => f.PickRandom(dishes).Id);

        var options = optionFaker.Generate(100);
        await _unitOfWork.DishOptions.AddRangeAsync(options);
        await _unitOfWork.SaveChangesAsync();
    }
}
