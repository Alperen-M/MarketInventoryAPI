using MarketInventory.Application.Services;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using MarketInventory.Infrastructure.Security;


var builder = WebApplication.CreateBuilder(args);

// ✅ DbContext
builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddSingleton<JwtTokenGenerator>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

builder.Services.AddAuthorization();

// ✅ Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// ✅ Repository Bağımlılıkları
builder.Services.AddScoped<IBarkodRepository, BarkodRepository>();
builder.Services.AddScoped<IBirimRepository, BirimRepository>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IKullaniciTuruRepository, KullaniciTuruRepository>();
builder.Services.AddScoped<IStokHareketiRepository, StokHareketiRepository>();
builder.Services.AddScoped<IUrunRepository, UrunRepository>();
builder.Services.AddScoped<IUrunFiyatRepository, UrunFiyatRepository>();

// ✅ Servis Bağımlılıkları
builder.Services.AddScoped<IBarkodService, BarkodService>();
builder.Services.AddScoped<IBirimService, BirimService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<IKullaniciTuruService, KullaniciTuruService>();
builder.Services.AddScoped<IStokHareketiService, StokHareketiService>();
builder.Services.AddScoped<IUrunService, UrunService>();
builder.Services.AddScoped<IUrunFiyatService, UrunFiyatService>();

// ✅ Controllers
builder.Services.AddControllers();

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Market Inventory API",
        Version = "v1"
    });
});

var app = builder.Build();

// ✅ Swagger middleware (dev ortamı için)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
