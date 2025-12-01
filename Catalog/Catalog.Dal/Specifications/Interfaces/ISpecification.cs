using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace Catalog.Dal.Specifications.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T,bool>> Criteria { get; }
        List<Expression<Func<T, BaseEntity>>> Includes { get; }
        Expression<Func<T, BaseEntity>> OrderBy { get; }

    }
}
