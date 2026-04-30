using Catalog.Dal.Entities;
using System.Collections.Generic;
using System.Linq;

public static class TestDataBuilder
{
    public static Restaurant CreateRestaurant(
        int id = 0, 
        string name = "Test Restaurant", 
        decimal rating = 4.5m, 
        string imageUrl = "https://example.com/image.png", 
        bool isActive = true,
        string description = "Delicious food")
    {
        var restaurant = new Restaurant 
        { 
            Id = id, 
            IsActive = isActive,
            Name = name, 
            Rating = rating,
            ImageUrl = imageUrl,
            Description = description,
            Addresses = new List<Address> 
            { 
                CreateAddress(id: 0, restaurantId: id) 
            },
            Contacts = new List<Contact>
            {
                CreateContact(id: 0, restaurantId: id)
            }
        };
        
        return restaurant;
    }

    public static Address CreateAddress(
        int id = 0, 
        int restaurantId = 0, 
        string city = "Kyiv", 
        string street = "Main Street", 
        string building = "10") =>
        new Address
        {
            Id = id,
            RestaurantId = restaurantId,
            City = city,
            Street = street,
            BuildingNumber = building,
            PostalCode = "01001"
        };

    public static Contact CreateContact(
        int id = 0, 
        int restaurantId = 0, 
        string type = "Phone", 
        string value = "+380630342585") =>
        new Contact
        {
            Id = id,
            RestaurantId = restaurantId,
            Type = type,
            Value = value
        };
    
    public static Category CreateCategory(int id = 0, string name = "General") =>
        new Category 
        { 
            Id = id, 
            Name = name 
        };

    public static Dish CreateDish(
        int id = 0, 
        string name = "Pizza", 
        decimal price = 250.0m, 
        int categoryId = 0, 
        int restaurantId = 0) =>
        new Dish
        {
            Id = id,
            Name = name,
            Description = "Very tasty",
            Price = price,
            ImageUrl = "pizza.png",
            CategoryId = categoryId,
            RestaurantId = restaurantId,
            DishOptions = new List<DishOption?>()
        };

    public static List<Restaurant> CreateRestaurants(int count = 3) =>
        Enumerable.Range(1, count)
            .Select(i => CreateRestaurant(id: 0, name: $"Restaurant {i}"))
            .ToList();
}