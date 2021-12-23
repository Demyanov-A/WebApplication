using WebApplication.Domain.Entities.Base.Interfaces;

namespace WebApplication.Domain.Entities.Base;

public abstract class NamedEntity : INamedEntity, IEntity
{
    public override string Name { get; set; }
}