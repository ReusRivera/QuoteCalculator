using QuoteCalculator.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Others:
builder.Services.AddPersistenceServices(configuration);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
