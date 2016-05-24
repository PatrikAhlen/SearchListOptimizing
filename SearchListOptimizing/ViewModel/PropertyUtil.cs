using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SearchListOptimizing.ViewModel
{
    public static class PropertyUtil
    {
        public static string GetPropertyName<T>(this T obj, Expression<Func<T>> expression)
        {
            return InternalGetPropertyName(expression.Body);
        }

        //public static string GetPropertyName<T>(Expression<Func<T>> expression)
        //{
        //    return InternalGetPropertyName(expression.Body);
        //}

        public static string GetPropertyName<T>(Expression<T> expression)
        {
            return InternalGetPropertyName(expression.Body);
        }

        private static string InternalGetPropertyName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression", "The expression is null.");
            }

            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression as UnaryExpression;
                if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }

            if (memberExpression != null && memberExpression.Member.MemberType == MemberTypes.Property)
            {
                return memberExpression.Member.Name;
            }

            throw new ArgumentException("No property reference expression was found", "expression");
        }
    }

}
