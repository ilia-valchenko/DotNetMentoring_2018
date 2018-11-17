using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QueryableProviderForMovieDb
{
    public class MovieDbQuery<T>: IQueryable<T>
    {
        private Expression _expression;
        private MovieDbLinqProvider _provider;

        public MovieDbQuery(Expression expression, MovieDbLinqProvider provider)
        {
            _expression = expression;
            _provider = provider;
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
                return  _expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return _provider;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _provider.Execute<IEnumerable<T>>(_expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _provider.Execute<IEnumerable>(_expression).GetEnumerator();
        }
    }
}
