using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Domain.Entities.Base;
using WebApplication.Domain.Entities.Base.Interfaces;

namespace WebApplication.Domain.Entities
{
    //[Table("Brandsss")]
    [Index(nameof(Name), IsUnique = true)]
    public class Brand : NamedEntity, IOrderEntity
    {
        //[Column("BrandOrder")]
        public int Order { get; set ; }

        public IReadOnlyCollection<Product> Products { get; set; }
    }
}
