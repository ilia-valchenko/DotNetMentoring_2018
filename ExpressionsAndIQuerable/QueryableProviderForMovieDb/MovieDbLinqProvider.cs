using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryableProviderForMovieDb
{
    public class MovieDbLinqProvider : IQueryProvider
    {
        private MovieDbQueryClient _movieDbQueryClient;

        public MovieDbLinqProvider(MovieDbQueryClient client)
        {
            _movieDbQueryClient = client;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new MovieDbQuery<TElement>(expression, this);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var itemType = TypeHelper.GetElementType(expression.Type);

            var translator = new ExpressionQueryTranslator();
            var queryString = translator.Translate(expression);

            return (TResult) _movieDbQueryClient.Search(itemType, queryString);
        }

        #region NotImplemented

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
