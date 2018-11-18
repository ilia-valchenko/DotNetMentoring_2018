using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using QueryableProviderForMovieDb.Entities;
using System.Collections;

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
            get
            {
                return typeof(T);
            }
        }

        public Expression Expression
        {
            get
            {
                return expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return provider;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return provider.Execute<IEnumerable<T>>(expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return provider.Execute<IEnumerable>(expression).GetEnumerator();
        }
    }
}
