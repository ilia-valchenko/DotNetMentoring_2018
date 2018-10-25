﻿using System.Linq.Expressions;

namespace Task1
{
    public class AddToIncrementTransformer : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if(node.NodeType == ExpressionType.Add)
            {
                ParameterExpression param = null;
                ConstantExpression constant = null;

                if(node.Left.NodeType == ExpressionType.Parameter)
                {
                    param = (ParameterExpression)node.Left;
                }
                else if(node.Left.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Left;
                }

                if(node.Right.NodeType == ExpressionType.Parameter)
                {
                    param = (ParameterExpression)node.Right;
                } else if(node.Right.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Right;
                }

                if(param != null && constant != null &&
                    constant.Type == typeof(int) && (int)constant.Value == 1)
                {
                    return Expression.Increment(param);
                }
            }

            return base.VisitBinary(node);
        }
    }
}
