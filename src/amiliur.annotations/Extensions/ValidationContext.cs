using amiliur.annotations.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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