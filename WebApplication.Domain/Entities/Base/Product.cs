using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain.Entities.Base.Interfaces;

namespace WebApplication.Domain.Entities.Base
{
    public class Product : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }
        public int SectionId { get; set; }
        public int? BrandId { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
    }
}
