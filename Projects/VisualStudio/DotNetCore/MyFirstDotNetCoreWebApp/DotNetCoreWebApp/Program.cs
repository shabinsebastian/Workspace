var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hi World!\n");
app.MapGet("/abc", () => "Hello World!\n");

// Middleware to log requests
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Path}");
    await next.Invoke(); // Pass to the next middleware
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

Terminal middleware
app.Run(async context =>
{   
    await context.Response.WriteAsync("Hello, Middleware!");
});

app.Run();
