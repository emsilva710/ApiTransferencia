using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la colección.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Agregar Swagger

builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
builder.Services.AddScoped<ITransferenciaRepository, TransferenciaRepository>();

// Configuración del logging
builder.Logging.ClearProviders(); // Opcional: Elimina los proveedores de logging predeterminados
builder.Logging.AddConsole(); // Agrega logging a la consola (puedes agregar otros proveedores si es necesario)



// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar los repositorios
builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
builder.Services.AddScoped<ITransferenciaRepository, TransferenciaRepository>();


// Agregar los controladores
builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configurar CORS
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TransferenciaAPI v1"));
}

// Configurar el pipeline de solicitudes HTTP.
app.UseAuthorization();  // Asegúrate de que Authorization esté habilitado si usas autenticación

// Mapear controladores
app.MapControllers();  // Mapea los controladores

app.Run();
