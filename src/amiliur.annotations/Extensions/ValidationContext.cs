using amiliur.annotations.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace amiliur.annotations.Extensions
{
    internal static class ValidationContextExtensions
    {
        internal static PropertyInfo GetObjectProperty(this ValidationContext ctx, string propertyName)
        {
            var property = ctx.ObjectType.GetProperty(propertyName);
            return property == null ? throw new PropertyNotFoundException(propertyName) : property;
        }
    }
}