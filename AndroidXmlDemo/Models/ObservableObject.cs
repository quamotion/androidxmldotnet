using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace AndroidXmlDemo.Models
{
    public abstract class ObservableObject<T> : INotifyPropertyChanged where T : ObservableObject<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected PropertyChangedEventHandler PropertyChangedHandler
        {
            get { return PropertyChanged; }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public void RaisePropertyChanged<TProperty>(Expression<Func<T, TProperty>> expr)
        {
            if (expr == null)
            {
                throw new ArgumentNullException("expr");
            }
            var memberExpression = expr.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Not a MemberExpression", "expr");
            }
            var parameterExpression = memberExpression.Expression as ParameterExpression;
            if (parameterExpression == null || !expr.Parameters.Contains(parameterExpression))
            {
                throw new ArgumentException("Not a property of " + typeof (T).Name, "expr");
            }
            var memberInfo = memberExpression.Member;
            if (!(memberInfo is PropertyInfo))
            {
                var message = string.Format("Not a property: {0}.{1}", typeof (T).Name, memberInfo.Name);
                throw new ArgumentException(message, "expr");
            }
            RaisePropertyChanged(memberInfo.Name);
        }
    }
}