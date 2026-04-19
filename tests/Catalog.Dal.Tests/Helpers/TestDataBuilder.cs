using Catalog.Dal.Entities;
public static class TestDataBuilder
{
    public static Restaurant CreateRestaurant(int id = 1, string name = "Test Restaurant", 
                                            decimal rating = 4.5m, 
                                            string imageUrl = "https://example.com/image.png", 
                                            string description = "Delicious food") =>
        new Restaurant 
        { 
            Id = id, 
            Name = name, 
            Rating = rating,
            ImageUrl = imageUrl,
            Description = description
        };

    public static List<Restaurant> CreateRestaurants(int count = 3) =>
        Enumerable.Range(1, count)
            .Select(i => CreateRestaurant(id: i, name: $"Restaurant {i}"))
            .ToList();
}