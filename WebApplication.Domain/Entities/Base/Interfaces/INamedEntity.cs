namespace WebApplication.Domain.Entities.Base.Interfaces;

public abstract class INamedEntity : IEntity
{
    public abstract string Name { get; set; }
    public abstract int Id { get; set; }
}