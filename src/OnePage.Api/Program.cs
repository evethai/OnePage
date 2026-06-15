using Microsoft.EntityFrameworkCore;
using OnePage.Domain.Interfaces;
using OnePage.Infrastructure.Data;
using OnePage.Infrastructure.Repositories;
using OnePage.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure Database Service (Dynamic SQLite / Postgres swapping)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var usePostgres = builder.Configuration.GetValue<bool>("UsePostgres");

if (usePostgres)
{
    var pgConn = builder.Configuration.GetConnectionString("PostgresConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(pgConn, b => b.MigrationsAssembly("OnePage.Infrastructure")));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(connectionString, b => b.MigrationsAssembly("OnePage.Infrastructure")));
}

// 2. Register Dependency Injection (DI) Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IQrCodeService, QrCodeService>();
builder.Services.AddSingleton<IExcelService, ExcelService>();

// Register Storage Service using local wwwroot path for local development
builder.Services.AddScoped<IStorageService>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var rootPath = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    return new LocalStorageService(rootPath);
});

// 3. Configure Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS for local development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 4. Configure HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// 5. Configure Static File Serving
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

// SPA Fallback: Redirect non-API requests to index.html
app.MapFallbackToFile("index.html");

// 6. Automatic Database Migration on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Automatically initialize database schema
    dbContext.Database.Migrate();
}

app.Run();
