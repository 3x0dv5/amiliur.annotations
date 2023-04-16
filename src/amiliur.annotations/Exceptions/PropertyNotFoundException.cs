using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amiliur.annotations.Exceptions
{
    public class PropertyNotFoundException: ArgumentException
    {
        public PropertyNotFoundException(string propertyName): base($"Property with the name `{propertyName}` was not found")
        {
            
        }
    }
}
