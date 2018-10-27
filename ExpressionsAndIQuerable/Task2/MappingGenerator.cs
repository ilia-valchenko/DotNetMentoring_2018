using System;
using System.Linq.Expressions;
using System.Linq;

namespace Task2
{
    /// <summary>
    /// The mapper generator.
    /// </summary>
    public class MappingGenerator
    {
        /// <summary>
        /// Creates a mapper for provided types.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <returns>
        /// Returns a mapper.
        /// </returns>
        public Mapper<TSource, TDestination> GenerateMapper<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var constructorInfoForDestinationType = destinationType.GetConstructors().First();

            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(
                Expression.MemberInit(Expression.New(constructorInfoForDestinationType),
                    sourceType.GetProperties()
                        .Select(parameterOfSourceType => Expression.Bind(
                            typeof(TDestination).GetProperty(parameterOfSourceType.Name),
                            Expression.Property(sourceParam, parameterOfSourceType)))),
                sourceParam);

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }
    }
}
