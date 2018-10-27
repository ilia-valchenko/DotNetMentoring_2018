using System.Collections.Generic;
using System.Linq.Expressions;

namespace Task1
{
    /// <summary>
    /// The transformer which replaces parameter expression by
    /// constant expressions.
    /// </summary>
    /// <typeparam name="TConst"></typeparam>
    public class ParameterToConstantTransformer<TConst> : ExpressionVisitor
    {
        /// <summary>
        /// The dictionary which represents a map for mapping
        /// parameters to constants.
        /// </summary>
        private readonly Dictionary<string, TConst> dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterToConstantTransformer{TConst}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public ParameterToConstantTransformer(Dictionary<string, TConst> dictionary)
        {
            this.dictionary = dictionary;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if(node != null && node.NodeType == ExpressionType.Parameter)
            {
                return Expression.Constant(this.dictionary[node.Name]);
            }

            return base.VisitParameter(node);
        }

        /// <summary>
        /// Visits lambda expression.
        /// </summary>
        /// <typeparam name="T">The type of generic method.</typeparam>
        /// <param name="node">The node.</param>
        /// <returns>Returns expression.</returns>
        /// <remarks>
        /// We need to override VisitLambda method because it should
        /// return a lambda of the same shape and the default implementation
        /// would also visit (and thus swap) out the parameters from the declaration.
        /// </remarks>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            // Don't visit the parameters.
            return Expression.Lambda(Visit(node.Body), node.Parameters);
        }
    }
}
