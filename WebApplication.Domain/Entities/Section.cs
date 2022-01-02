using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApplication.Domain.Entities.Base;
using WebApplication.Domain.Entities.Base.Interfaces;

namespace WebApplication.Domain.Entities;
[Index(nameof(Name), IsUnique = true)]
public class Section : NamedEntity, IOrderEntity
{
    
    public int Order { get; set; }

    public int? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public Section Parent { get; set; }

    public ICollection<Product> Products { get; set; }
}