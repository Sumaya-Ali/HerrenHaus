using System.Reflection;
using HerrenHaus_API.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; // return 406 (not acceptable) when accept ='application/xml' & response is json 
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); //return json or xml response according to 'Accept' parameter in header request in postman
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title="Swagger Sumaya Demo API",
            Description="Demo API for showing HerrenHaus in Germany",
            Version="v1",
            TermsOfService=new Uri("https://github.com/Sumaya-Ali/"),
            Contact= new OpenApiContact
            {
                Name="Sumaya Ali",
                Email="sumaya.jalal.ali@gmail.com",
                Url=new Uri("https://www.linkedin.com/in/sumaya-ali-ite/")
            },
            License=new OpenApiLicense
            {
                Name="Sumaya Ali Open License",
                Url=new Uri("https://github.com/Sumaya-Ali/HerrenHaus")
            }
        });
    var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath =Path.Combine(AppContext.BaseDirectory, fileName);
    options.IncludeXmlComments(filePath);
});
// End Configure Swagger



// Configer Serilog Logger
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.
//    File("log/HerrenHausLog.txt", rollingInterval: RollingInterval.Day).CreateLogger();

//builder.Host.UseSerilog();

// End Configer Serilog Logger

builder.Services.AddSingleton<ILogging, LoggingV2>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Configure Swagger
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger HerrenHaus API");
       // options.RoutePrefix = "";
        });
    // End Configure Swagger
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
