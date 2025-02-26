namespace Domain.Exceptions;

public class NotFoundException(string entityName, string propertyName, string propertyValue)
    : Exception($"Entity {entityName} with {propertyName} = {propertyValue} not found.");