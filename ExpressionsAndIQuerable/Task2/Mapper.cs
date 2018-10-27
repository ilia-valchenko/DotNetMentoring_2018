using System;

namespace Task2
{
    /// <summary>
    /// The mapper.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    public class Mapper<TSource, TDestination>
    {
        private readonly Func<TSource, TDestination> mapFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mapper{TSource, TDestination}"/> class.
        /// </summary>
        /// <param name="mapFunction">The mapping function.</param>
        internal Mapper(Func<TSource, TDestination> mapFunction)
        {
            this.mapFunction = mapFunction;
        }

        /// <summary>
        /// Mapps source type to destination type.
        /// </summary>
        /// <param name="source">The instance of the source type.</param>
        /// <returns>
        /// Returns an instance of the destination type.
        /// </returns>
        public TDestination Map(TSource source)
        {
            return this.mapFunction(source);
        }
    }
}
