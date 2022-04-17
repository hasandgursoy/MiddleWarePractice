using MiddleWarePractice.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Request ile response arasında işlemler yapabiliyoruz hemen aşşağı bir örnek bırakıyorum.


// app.Run( async context => {
//     await context.Response.WriteAsync("Response da ki yazı benim swagger'dan izle beni !");
// });

app.UseHello();

app.Use(async (context,next) => {
    
    System.Console.WriteLine("1. Middle Ware");
    await next.Invoke();
    System.Console.WriteLine("1. Middle Ware kapanıyor");
});


app.Use(async (context,next) => {

    System.Console.WriteLine("2. Middle Ware");
    await next.Invoke();
    System.Console.WriteLine("2. Middle Ware kapanıyor");
});

app.Use(async (context,next) => {

    System.Console.WriteLine("3. Middle Ware");
    await next.Invoke();
    System.Console.WriteLine("3. Middle Ware kapanıyor");
});

app.Use(async (context,next) => {

    System.Console.WriteLine("2. Middle Ware");
    await next.Invoke();
    System.Console.WriteLine("2. Middle Ware kapanıyor");
});
// Route'a göre çalışsın demektir.
app.Map("/example",internalApp => {
    internalApp.Run(async context => {
        System.Console.WriteLine("/example midleware tetiklendi");
        await context.Response.WriteAsync("/example middleware tetiklendi.");
    });
});
// Requestin içindeki herhangi bir bilgiye göre tetiklenicek bir midleware yazalım
app.MapWhen(x => x.Request.Method =="GET",internalApp => {
    internalApp.Run(async context => {
        System.Console.WriteLine("Middleware Tetiklendi");
        await context.Response.WriteAsync("MapWhen MiddleWare Tetiklendi.");
    });
});

app.Run();

/*
Middleware yani ara katman client tarafından bir request 
gönderildiğinde request'e karşılık response dönene kadar
geçen sürede yapılması gereken işlemler için process'in arasına
girmeyi sağlayan yapılardır. Request ve response arasına girip işlem 
yapmamıza olanak sağlamasının yanında, bu aralığa çoklu işlemler de dahil edebiliriz.
Bu işlemlerin hangi sırayla yapılacağını da belirleyebiliriz.
*/