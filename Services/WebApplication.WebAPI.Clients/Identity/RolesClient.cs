using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Interfaces;
using WebApplication.WebAPI.Clients.Base;

namespace WebApplication.WebAPI.Clients.Identity
{
    public class RolesClient : BaseClient
    {
        public RolesClient(HttpClient Client) : base(Client, WebAPIAddresses.Identity.Roles)
        {
        }
    }
}
