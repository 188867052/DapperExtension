﻿using System;
using System.Linq.Expressions;

namespace DapperExtension
{
    public static class ExpressionExtension
    {
        public static string GetPropertyName<T>(this Expression<Func<T, int>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, decimal>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, bool?>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, string>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, DateTime>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, object>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T, TEnumType>(this Expression<Func<T, TEnumType>> expression) where TEnumType : Enum
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, bool>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, int?>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, decimal?>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, DateTime?>> expression)
        {
            return expression.Body.GetName();
        }

        public static string GetName(this Expression expression)
        {
            string name;
            switch (expression)
            {
                case UnaryExpression unaryExpression:
                    name = unaryExpression.GetName();
                    break;
                case MemberExpression memberExpression:
                    name = memberExpression.GetName();
                    break;
                case ParameterExpression parameterExpression:
                    name = parameterExpression.GetName();
                    break;
                default:
                    throw new ArgumentException("不支持的参数");
            }

            return name;
        }

        private static string GetName(this UnaryExpression unaryExpression)
        {
            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }

        private static string GetName(this MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        private static string GetName(this ParameterExpression parameterExpression)
        {
            return parameterExpression.Type.Name;
        }
    }
}