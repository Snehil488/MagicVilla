var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(
        option =>
        {
            option.ReturnHttpNotAcceptable = true;
        }
    )
    .AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

//Content negotiation - to not accept any other format - option.ReturnHttpNotAcceptable = true;
//To allow XML format

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
