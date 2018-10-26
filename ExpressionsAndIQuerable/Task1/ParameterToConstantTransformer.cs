using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Task1
{
    public class ParameterToConstantTransformer<TConst> : ExpressionVisitor
    {
        private readonly Dictionary<string, TConst> dictionary;

        public ParameterToConstantTransformer(Dictionary<string, TConst> dictionary)
        {
            this.dictionary = dictionary;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if(node.NodeType == ExpressionType.Parameter)
            {
                Console.WriteLine($"Parameter name = {node.Name}");
                var constant = this.dictionary[node.Name];

                return Expression.Constant(constant);
            }

            return base.VisitParameter(node);
        }
    }
}
