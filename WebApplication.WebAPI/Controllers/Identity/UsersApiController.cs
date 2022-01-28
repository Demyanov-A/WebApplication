using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.DAL.Context;
using WebApplication.Domain.Entities.Identity;
using WebApplication.Interfaces;

namespace WebApplication.WebAPI.Controllers.Identity
{
    [ApiController]
    [Route(WebAPIAddresses.Identity.Users)]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebApplicationDB> _UserStore;

        public UsersApiController(WebApplicationDB db)
        {
            _UserStore = new UserStore<User, Role, WebApplicationDB>(db);
            //_UserStore.AutoSaveChanges = true;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAll() => await _UserStore.Users.ToArrayAsync();
    }
}
