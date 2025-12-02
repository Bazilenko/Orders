using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;
using Catalog.Dal.Specifications.Interfaces;

namespace Catalog.Dal.Specifications
{
    public class RestaurantByRatingSpecification : BaseSpecification<Restaurant>
    {
        public RestaurantByRatingSpecification(int rating) : base(r => r.Rating == rating)
        {
            
        }

    }
}
