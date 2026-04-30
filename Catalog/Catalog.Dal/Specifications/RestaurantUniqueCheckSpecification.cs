using Catalog.Dal.Entities;
using Catalog.Dal.Specifications;
using Catalog.Dal.Specifications.Interfaces;

public class RestaurantUniqueCheckSpecification : BaseSpecification<Restaurant>
{
    public RestaurantUniqueCheckSpecification(string name, string? city) 
        : base(r => r.Name.ToLower() == name.ToLower() && 
                    r.Addresses.Any(a => a.City == city))
    {
    }   
}