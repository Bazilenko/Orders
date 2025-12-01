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
    public class BaseSpecification<T> : ISpecification<T> where T :BaseEntity
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, BaseEntity>>> Includes { get; } = new();

        public Expression<Func<T, BaseEntity>> OrderBy { get; private set; }

        protected void AddInclude(Expression<Func<T, BaseEntity>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, BaseEntity>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

    }
}
