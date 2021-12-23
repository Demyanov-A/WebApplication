using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Domain.Entities.Base;
using WebApplication.Domain.Entities.Base.Interfaces;

namespace WebApplication.Domain.Entities
{
    public class Brand : NamedEntity, IOrderEntity
    {
        public int Order { get; set ; }
    }
}
