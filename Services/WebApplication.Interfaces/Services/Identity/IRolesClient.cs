using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication.Domain.Entities.Identity;

namespace WebApplication.Interfaces.Services.Identity
{
    public interface IRolesClient : IRoleStore<Role>
    {

    }
}
