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
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---- JWT Options & Service (tek servis) ----
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// JWT Authentication
var jwt = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,

            ValidateAudience = true,
            ValidAudience = jwt.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,              // saat farkı toleransı kapat
            RoleClaimType = ClaimTypes.Role         // role claim tipi net
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                Console.WriteLine("JWT auth failed: " + ctx.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

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
builder.Services.AddScoped<IUrunService, UrunService>();
builder.Services.AddScoped<IUrunFiyatService, UrunFiyatService>();

builder.Services.AddControllers();

// Swagger + JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Market Inventory API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,   // <— Http + bearer
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Sadece token gir: örn: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
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
