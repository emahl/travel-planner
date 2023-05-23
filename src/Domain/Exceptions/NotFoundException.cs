namespace Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string type, Guid id)
        : base($"{type} with id {id} was not found.") { }
}
