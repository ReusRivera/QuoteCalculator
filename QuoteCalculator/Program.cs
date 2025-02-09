using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Context;
using QuoteCalculator.Services.BorrowersService;

var builder = WebApplication.CreateBuilder(args);

// Connection Strings:
string? defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DBContext:
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(defaultConnectionString));

// Interface & Repository:
builder.Services.AddScoped<IBorrowers, Borrowers>();

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
