using Microsoft.EntityFrameworkCore;
using Orders.Application.Interfaces;
using Orders.Application.UseCases;
using Orders.Infrastructure.EF;
using Orders.Infrastructure.Queries;
using Orders.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// Database (EF Core)
// ---------------------------------------------------------
builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    // Aqui você pode trocar para o banco real
    // SqlServer, PostgreSQL, MySQL etc.

    options.UseInMemoryDatabase("OrdersDb");
});

// ---------------------------------------------------------
// Application Layer
// ---------------------------------------------------------
// UseCases (Commands)
builder.Services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();

// ---------------------------------------------------------
// Queries (Read Side - CQRS)
// ---------------------------------------------------------
builder.Services.AddScoped<IOrderQuery, OrdersQueries>();

// ---------------------------------------------------------
// Repositories (Write Side - Domain persistence)
// ---------------------------------------------------------
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// ---------------------------------------------------------
// Controllers / API
// ---------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// (Opcional) CORS para permitir testar no Postman/Front
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();

// ---------------------------------------------------------
// Pipeline
// ---------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.MapControllers();

app.Run();
