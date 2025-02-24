using QuoteCalculator.Infrastructure;
using QuoteCalculator.WebApi.ApplicationRegistration;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Others:
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Custom:
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(configuration);

//builder.Logging.AddConsole();
//builder.Logging.AddDebug();

//builder.Host.UseSerilog((context, config) =>
//{
//    config.WriteTo.File("logs/quoteCalculator.txt");
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(options =>
//{
//    var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

//    options.SetIsOriginAllowed(origin => allowedOrigins.Contains(origin))
//       .AllowAnyHeader()
//       .AllowAnyMethod()
//       .AllowCredentials();

//    //opt.WithOrigins("https://localhost:5001").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
//});

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
