using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ExceptionHandlingMiddleware> _Logger;

        public ExceptionHandlingMiddleware(RequestDelegate Next, ILogger<ExceptionHandlingMiddleware> Logger)
        {
            _Next = Next;
            _Logger = Logger;
        }

        public async Task InvokeAsync(HttpContext Context)
        {
            try
            {
                await _Next(Context);
            }
            catch (Exception error)
            {
                HandleException(Context, error);
                throw;
            }
        }

        private void HandleException(HttpContext Context, Exception Error)
        {
            _Logger.LogError(Error, "Ошибка при обработке запроса {0}", Context.Request.Path);
        }
    }
}
