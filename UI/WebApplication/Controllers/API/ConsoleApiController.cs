using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers.API
{
    [ApiController, Route("api/console")]
    public class ConsoleApiController : ControllerBase
    {
        [HttpGet("clear")]
        public void Clear() => Console.Clear();

        [HttpGet("write")]
        public void WriteLine(string Str) => Console.WriteLine(Str);
    }
}
