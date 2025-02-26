namespace Domain.Exceptions;

public class PropertyNotUniqueException(string propertyName)
    : Exception($"Property {propertyName} is not unique");