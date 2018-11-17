using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QueryableProviderForMovieDb.Entities;

namespace QueryableProviderForMovieDb
{
    public class MovieDbEntitySet<T> : IQueryable<T> where T : MovieDbEntity
    {
        protected Expression expression;
        protected IQueryProvider provider;

        public MovieDbEntitySet()
        {
            expression = Expression.Constant(this);
            var client = new MovieDbQueryClient();
            provider = new MovieDbLinqProvider(client);
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }
    }
}
