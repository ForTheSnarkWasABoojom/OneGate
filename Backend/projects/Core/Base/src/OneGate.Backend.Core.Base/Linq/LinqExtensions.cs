using System;
using System.Linq.Expressions;
using LinqKit;

namespace OneGate.Backend.Core.Base.Linq
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Applies an <paramref name="expr2"/> to <paramref name="expr1"/> by AND operator
        /// if and only if specified <paramref name="obj"/> is not null.
        /// </summary>
        public static Expression<Func<T, bool>> FilterBy<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2, object obj)
        {
            return obj != null ? expr1.And(expr2) : expr1;
        }
        
        /// <summary>
        /// Applies an <paramref name="expr2"/> to <paramref name="expr1"/> by AND operator.
        /// </summary>
        public static Expression<Func<T, bool>> FilterBy<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return expr1.And(expr2);
        }
    }
}