using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QueryableProviderForMovieDb
{
    public class ExpressionQueryTranslator : ExpressionVisitor
    {
        private StringBuilder _resultString;

        internal ExpressionQueryTranslator()
        {
            _resultString = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            _resultString.Append("{\"fields\": [");

            Visit(exp);

            _resultString.Append("]}");

            return _resultString.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    _resultString.Append($"{{\"operation\":\"equal\",");

                    if (!(node.Left.NodeType == ExpressionType.MemberAccess))
                    {
                        throw new NotSupportedException(string.Format("Left operand should be property or field", node.NodeType));
                    }

                    if (!(node.Right.NodeType == ExpressionType.Constant))
                    {
                        throw new NotSupportedException(string.Format("Right operand should be constant", node.NodeType));
                    }

                    Visit(node.Left);
                    //_resultString.Append("(");
                    Visit(node.Right);
                    //_resultString.Append(")");
                    break;

                case ExpressionType.AndAlso:
                    Visit(node.Left);
                    _resultString.Append(",");
                    Visit(node.Right);
                    break;

                //case ExpressionType.And:
                //    _resultString.Append(",{{");
                //    break;

                default:
                    throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultString.Append($"\"name\":\"{node.Member.Name}\",");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultString.Append($"\"value\":");

            if(node.Value is int)
            {
                _resultString.Append($"{node.Value}}}");
            }
            else if(node.Value is string)
            {
                var transformedString = node.Value.ToString().Replace(' ', '+');
                _resultString.Append($"\"{transformedString}\"}}");
            }
            else
            {
                _resultString.Append($"\"{node.Value}\"}}");
            }

            return node;
        }
    }
}
