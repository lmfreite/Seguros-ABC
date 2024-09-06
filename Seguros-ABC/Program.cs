using Microsoft.EntityFrameworkCore;
using Seguros_ABC.Context;

var builder = WebApplication.CreateBuilder(args);

// Variable para la cadena de conexi�n
var connectionString = builder.Configuration.GetConnectionString("Connection");

// Registrar el servicio para la conexi�n
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Configurar CORS para permitir las URLs espec�ficas
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://segurosabc.netlify.app")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar HTTPS
app.UseHttpsRedirection();

// Aplicar la pol�tica de CORS
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
