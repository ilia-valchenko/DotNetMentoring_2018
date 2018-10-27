using System;
using System.Linq.Expressions;

namespace Task1
{
    /// <summary>
    /// Represents a simple tree traversal class which
    /// print all nodes to console.
    /// </summary>
    public class TreeTraversal : ExpressionVisitor
    {
        /// <summary>
        /// Visits expression tree and prints each node to console.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        /// Returs expression.
        /// </returns>
        public override Expression Visit(Expression node)
        {
            int indent = 0;

            if (node == null)
            {
                return base.Visit(node);
            }

            if (node.NodeType == ExpressionType.Parameter)
            {
                indent++;
                ParameterExpression parameter = (ParameterExpression)node;
                Console.WriteLine($"{new string(' ', indent * 2)}{node.NodeType} - {parameter.Name} [{node.Type}]");
                Expression result = base.Visit(node);
                indent--;

                return result;
            }
            else if (node.NodeType == ExpressionType.Constant)
            {
                indent++;
                ConstantExpression constant = (ConstantExpression)node;
                Console.WriteLine($"{new string(' ', indent * 2)}{node.NodeType} - {constant.Value} [{node.Type}]");
                Expression result = base.Visit(node);
                indent--;

                return result;
            }
            else
            {
                indent++;
                Console.WriteLine($"{new string(' ', indent)}{node.NodeType} - {node.Type}");
                Expression result = base.Visit(node);
                indent--;

                return result;
            }
        }
    }
}
