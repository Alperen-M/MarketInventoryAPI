using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using MarketInventory.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Ayarlarý
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwt = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;

// JWT Auth
Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

// Authentication ekle
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
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
    };

    options.RequireHttpsMetadata = false;

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication Failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine("OnChallenge triggered: " + context.Error + " - " + context.ErrorDescription);
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            Console.WriteLine("Token received: " + context.Token);
            return Task.CompletedTask;
        }
    };
});

// Token servisi
builder.Services.AddScoped<IJwtTokenService, JwtTokenGenerator>();


// Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBarkodRepository, BarkodRepository>();
builder.Services.AddScoped<IBirimRepository, BirimRepository>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IKullaniciTuruRepository, KullaniciTuruRepository>();
builder.Services.AddScoped<IStokHareketiRepository, StokHareketiRepository>();
builder.Services.AddScoped<IUrunRepository, UrunRepository>();
builder.Services.AddScoped<IUrunFiyatRepository, UrunFiyatRepository>();

// Services
builder.Services.AddScoped<IBarkodService, BarkodService>();
builder.Services.AddScoped<IBirimService, BirimService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<IKullaniciTuruService, KullaniciTuruService>();
builder.Services.AddScoped<IStokHareketiService, StokHareketiService>();
builder.Services.AddScoped<IUrunService, UrunService>(); // Düzeltme yapýldý
builder.Services.AddScoped<IUrunFiyatService, UrunFiyatService>(); // Düzeltme yapýldý

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Opsiyonel: Eðer C# property isimlerini aynen kullanmak isterseniz.
    });


// Swagger + JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Market Inventory API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Sadece token yapýþtýr (Bearer yazmana gerek yok)."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

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
