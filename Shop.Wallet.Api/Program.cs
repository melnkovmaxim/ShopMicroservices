using System.Reflection;
using Shop.Infrastructure.Extensions;
using Shop.Wallet.DataProvider.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddRabbitMq(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
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

app.Run();