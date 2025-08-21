using global::MarketInventory.Infrastructure.Data;
using MarketInventory.Application.DTOs;
using MarketInventory.Application.Services;
using MarketInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace MarketInventory.Tests
{


    public class UrunServiceMockTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturn_MockedUrunler()
        {
            // Arrange
            var urunler = new List<Urun>
        {
            new Urun { Id = 1, Ad = "Elma", Tur = "Meyve", Birim = new Birim { Id = 1, Ad = "Kg" } },
            new Urun { Id = 2, Ad = "Armut", Tur = "Meyve", Birim = new Birim { Id = 1, Ad = "Kg" } }
        };

            var mockContext = new Mock<MarketDbContext>(new DbContextOptions<MarketDbContext>());
            mockContext.Setup(c => c.Urunler).ReturnsDbSet(urunler);

            var service = new UrunService(mockContext.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn_MockedUrunler3()
        {
            // Arrange
            var urunler = new List<Urun>
        {
            new Urun { Id = 1, Ad = "Elma", Tur = "Meyve", Birim = new Birim { Id = 1, Ad = "Kg" } },
            new Urun { Id = 2, Ad = "Armut", Tur = "Meyve", Birim = new Birim { Id = 1, Ad = "Kg" } },
             new Urun { Id = 3, Ad = "Armut", Tur = "Meyve", Birim = new Birim { Id = 1, Ad = "Kg" } }
        };

            var mockContext = new Mock<MarketDbContext>(new DbContextOptions<MarketDbContext>());
            mockContext.Setup(c => c.Urunler).ReturnsDbSet(urunler);

            var service = new UrunService(mockContext.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Distinct(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            // Arrange
            var urunler = new List<Urun>
    {
        new Urun { Id = 1, Ad = "Elma", Tur = "Meyve", Birim = new Birim { Id = 2, Ad = "Kg" }},
        new Urun { Id = 2, Ad = "Armut", Tur = "Meyve", Birim = new Birim { Id = 2, Ad = "Kg" }},
        new Urun { Id = 3, Ad = "Kiraz", Tur = "Meyve", Birim = new Birim { Id = 2, Ad = "Kg" }}
    };

            var mockContext = new Mock<MarketDbContext>(new DbContextOptions<MarketDbContext>());
            mockContext.Setup(c => c.Urunler).ReturnsDbSet(urunler);

            var service = new UrunService(mockContext.Object);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal("Elma", result.Ad);
            Assert.Equal("Meyve", result.Tur);
            Assert.Equal(2, result.BirimId);
            Assert.Equal("Kg", result.BirimAdi);
        }

    }

}


//diğer testleri tamamla    
//void metodlara unit test nasıl yazılır. 
//metod ne alıyor ne dönüyor
//hangi servislere bağımlı
//hangi case'leri test etmeliyim


