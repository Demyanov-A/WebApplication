namespace WebApplication.Domain.Entities.Base.Interfaces;

public interface INamedEntity : IEntity
{
    public string Name { get; set; }
    public int Id { get; set; }
}