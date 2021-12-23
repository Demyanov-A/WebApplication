using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain.Entities.Base.Interfaces;

namespace WebApplication.Domain.Entities.Base
{
    public class Brand : NamedEntity, IOrderEntity
    {
        public int Order { get; set ; }
    }

    public class Section : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }
    }
}
