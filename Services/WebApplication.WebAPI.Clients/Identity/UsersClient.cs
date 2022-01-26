using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Interfaces;
using WebApplication.WebAPI.Clients.Base;

namespace WebApplication.WebAPI.Clients.Identity
{
    public class UsersClient : BaseClient
    {
        public UsersClient(HttpClient Client) : base(Client, WebAPIAddresses.Identity.Users)
        {
        }
    }
}
