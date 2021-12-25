using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Domain.Entities.Base;
using WebApplication.Domain.Entities.Base.Interfaces;

namespace WebApplication.Domain.Entities
{
    [Index(nameof(Name))]
    public class Product : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }
        public int SectionId { get; set; }

        [ForeignKey((nameof(SectionId)))]
        public Section Section { get; set; }
        public int? BrandId { get; set; }

        [ForeignKey((nameof(BrandId)))]
        public Brand Brand { get; set; }
        public string ImageURL { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
