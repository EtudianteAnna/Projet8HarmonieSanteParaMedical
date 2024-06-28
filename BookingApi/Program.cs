using BookingApi.InterfaceServices;
using BookingAPI.Services;
using Microsoft.EntityFrameworkCore;
using BookingApi.DbContext;
using BookingApi.Repository;
using BookingApi.InterfaceRepository;

var builder = WebApplication.CreateBuilder(args);

// Ajout de la chaîne de connexion ici
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BookingDb>(options =>
    options.UseSqlServer(connectionString));   // Assurez-vous que UseSqlServer est reconnu

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();