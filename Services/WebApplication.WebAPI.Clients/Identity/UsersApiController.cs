using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Interfaces;

namespace WebApplication.WebAPI.Clients.Identity
{
    [ApiController]
    [Route(WebAPIAddresses.Identity.Users)]
    public class UsersApiController : ControllerBase
    {
    }
}
