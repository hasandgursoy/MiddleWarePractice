namespace MiddleWarePractice.Middlewares
{   // Custom Middleware
    public class HelloMiddleWare
    {

        private readonly RequestDelegate _next;
        public HelloMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        // Async methodların geri dönüşleride Task olur .
        public async Task Invoke(HttpContext context){
            
            System.Console.WriteLine("Hello World");
            await _next.Invoke(context);
            System.Console.WriteLine("Bye World");

        }
        
    }

    static public class HelloMiddleWareExtension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder){

            return builder.UseMiddleware<HelloMiddleWare>();

        }
    }
}