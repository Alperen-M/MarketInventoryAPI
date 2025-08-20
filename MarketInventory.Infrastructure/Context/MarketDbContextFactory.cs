using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MarketInventory.Infrastructure.Data
{
    public class MarketDbContextFactory : IDesignTimeDbContextFactory<MarketDbContext>
    {
        public MarketDbContext CreateDbContext(string[] args)
        {
            // Başlangıç projesinin (startup project) yolunu bulur.
            // Bu, 'dotnet ef' komutunun --startup-project parametresine bağlıdır.
            var basePath = AppContext.BaseDirectory;
            Console.WriteLine($"Base Path: {basePath}"); // Hata ayıklama için ekledim

            // appsettings.json dosyasını doğru yerden okumak için bir ConfigurationBuilder oluşturulur.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // SQL Server için bağlantı dizesini alır.
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Bağlantı dizesi 'DefaultConnection' appsettings.json dosyasında bulunamadı.");
            }

            // DbContextOptionsBuilder nesnesi oluşturulur.
            var optionsBuilder = new DbContextOptionsBuilder<MarketDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // Yeni bir MarketDbContext örneği döndürülür.
            return new MarketDbContext(optionsBuilder.Options);
        }
    }
}
