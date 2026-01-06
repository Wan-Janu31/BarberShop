using AutoMapper;
using Barber_Service.Models;
using Barber_Service.Repository.Implementations;
using Barber_Service.Repository.Interfaces;
using Barber_Service.Service.Implementations;
using Barber_Service.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<BarberBookingDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase"));
});

// Repository
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBarberRepository, BarberRepository>();

// Service
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IBarberService, BarberService>();

// 🔥 AutoMapper (BẮT BUỘC)
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
