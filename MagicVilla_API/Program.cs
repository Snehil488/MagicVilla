using MagicVilla_API.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//using custom logger serilog intead of built in one
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//    .WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

//builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(
        option =>
        {
            //option.ReturnHttpNotAcceptable = true;
        }
    )
    .AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

//Content negotiation - to not accept any other format - option.ReturnHttpNotAcceptable = true;
//To allow XML format

//register cutom logger
builder.Services.AddSingleton<ILogging, Logging>();

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

app.Run();
