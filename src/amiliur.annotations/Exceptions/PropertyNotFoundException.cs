namespace amiliur.annotations.Exceptions
{
    public class PropertyNotFoundException: ArgumentException
    {
        public PropertyNotFoundException(string propertyName): base($"Property with the name `{propertyName}` was not found")
        {
            
        }
    }
}
