var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions()
    {
        SourceCodeLineCount = 10
    };

    app.UseDeveloperExceptionPage(developerExceptionPageOptions);
}

FileServerOptions fileServerOptions = new FileServerOptions();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("home.html");

app.UseFileServer(fileServerOptions);

//app.MapGet("/", () => "Hi World!\n");
//app.MapGet("/test", () => "Hello World!\n");

// Middleware to log requests
app.Use(async (context, next) =>
{
    if (app.Environment.IsDevelopment())
    {
        throw new Exception("Testing developer exceptions.");
    }

    Console.WriteLine($"Request: {context.Request.Path}");
    await next.Invoke(); // Pass to the next middleware
    Console.WriteLine($"Response: {context.Response.StatusCode}");
    await context.Response.WriteAsync("Hi, Middleware!");
});

//Terminal middleware
//app.Run(async context =>
//{   
//    await context.Response.WriteAsync("Hello, Middleware!");
//});

app.Run();
