namespace WebApplication.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;

        public TestMiddleware(RequestDelegate Next)
        {
            _Next = Next;
        }

        public async Task Invoke(HttpContext context)
        {
            var controller_name = context.Request.RouteValues["Controller"];
            var action_name = context.Request.RouteValues["action"];
            //Обработка информации из Context.Request;

            var processing_task = _Next(context);//далее здесь работает оставшаяся часть конвейера

            //Можно выполнить какие-то действия параллельно асинхронно с остальной частью конвейера

            await processing_task; 
            
            // дообработка данных в Context.Response
        }
    }
}
