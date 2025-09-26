using global::MarketInventory.Infrastructure.Data;
using MarketInventory.Application.DTOs;
using MarketInventory.Application.Services;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace MarketInventory.Tests
{


    public class UrunServiceMockTests
    {
        // Facr -> Parametresiz bir test metodudur, koştuğunda tek seferlik sabit bir senaryoyu test eder.
        [Fact]
        public async Task GetAllAsync_ShouldReturn_MockedUrunler()
        {
            // Arrange
            //Fake (mock) data hazırlanıyor.
            //Urun nesneleri normalde DB'den gelir , ama testte veritabanı kullanmak istemiyoruz-> elimizle 2 tane Urun nesnesi oluşturduk.
            var urunler = new List<Urun>
        {
            new Urun { Id = 1, Ad = "Elma", Tur = "Meyve", Birim = new Birim { Id = 1, Ad = "Kg" } },
            new Urun { Id = 2, Ad = "Armut", Tur = "Meyve", Birim = new Birim { Id = 1, Ad = "Kg" } }
        };
            //MarketDbContext'in mock(sahte) versiyonunu oluşturuyoruz.
            var mockContext = new Mock<MarketDbContext>(new DbContextOptions<MarketDbContext>());
            //Urunler DbSeti'ne erişildiğinde bizim verdiğimiz urunler listesi dönsün istiyoruz.
            mockContext.Setup(c => c.Urunler).ReturnsDbSet(urunler);
            //Mock context'i kullanarak gerçek UrunService nesnesini oluşturuyoruz.
            //Artık service.GetAllAsync() çağırısı yapıldığında, gerçek DB yerine sahte urunler listesini dönecek.
            var service = new UrunService(mockContext.Object);

            // Act
            //Test edilen metodu çağırıyoruz. Bu çağrı arka planda mock'tan gelen urunler listesine bağlanacak.
            var result = await service.GetAllAsync();

            // Assert(Doğrulama kısmı)
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

        [Theory]
        [InlineData(1, "Elma", "Meyve", 2, "Kg")]
        [InlineData(2, "Armut", "Meyve", 2, "Kg")]
        [InlineData(3, "Kiraz", "Meyve", 2, "Kg")]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists_WithParameters(
        int id, string expectedAd, string expectedTur, int expectedBirimId, string expectedBirimAdi)
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
            var result = await service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
            Assert.Equal(expectedAd, result.Ad);
            Assert.Equal(expectedTur, result.Tur);
            Assert.Equal(expectedBirimId, result.BirimId);
            Assert.Equal(expectedBirimAdi, result.BirimAdi);
        }


        [Fact]
        public async Task AddAsync_ShouldAddEntityAndReturnDto_WhenValidInput()
        {
            //Arrange
            var urunCreateDto = new UrunCreateDto
            {
                Ad = "Muz",
                Tur = "Meyve",
                BirimId = 2
            };
            var birim = new Birim { Id = 2, Ad = "Kg" };

            //Mock Dbset<Urun>
            var urunler = new List<Urun>();

            var mockContext = new Mock<MarketDbContext>(new DbContextOptions<MarketDbContext> { });
            mockContext.Setup(c => c.Urunler).ReturnsDbSet(urunler);
            mockContext.Setup(c => c.Birimler).ReturnsDbSet(new List<Birim> { birim });
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var service = new UrunService(mockContext.Object);

            //Act
            var result = await service.AddAsync(urunCreateDto);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Muz", result.Ad);
            Assert.Equal("Meyve", result.Tur);
            Assert.Equal(2, result.BirimId);
            Assert.Equal("Kg", result.BirimAdi);

            //result'ın sonunda neden ! kullanılır.
            //kodları chate sor kendin test yaz
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity_WhenEntityExists()
        {
            // Arrange
            // Mock'lanacak veritabanı bağlamının parametresi için bir seçenek nesnesi oluşturuyoruz.
            var dbContextOptions = new DbContextOptions<MarketDbContext>();

            // Test için bir Urun nesnesi oluşturuyoruz.
            var urun = new Urun
            {
                Id = 1,
                Ad = "Elma",
                Tur = "Meyve",
                BirimId = 1,
                CreatedById = 10,
                KayitTarihi = DateTime.UtcNow
            };

            // DbSet'i taklit etmek için bir Mock nesnesi oluşturuyoruz.
            var mockUrunlerDbSet = new Mock<DbSet<Urun>>();

            mockUrunlerDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(urun);

            var mockContext = new Mock<MarketDbContext>(dbContextOptions);

            mockContext.Setup(c => c.Urunler).Returns(mockUrunlerDbSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(1);

            var service = new UrunService(mockContext.Object);

            var dto = new UrunUpdateDto
            {
                Ad = "Armut",
                Tur = "Meyve",
                BirimId = 2,
                CreatedById = 20
            };

            // Act
            var result = await service.UpdateAsync(1, dto);

            // Assert
            Assert.True(result);

            Assert.Equal("Armut", urun.Ad);
            Assert.Equal(2, urun.BirimId);
            Assert.Equal(20, urun.CreatedById);
        }

      
        private Mock<MarketDbContext> SetupMockDbContext(Urun urun)
        {
            var dbContextOptions = new DbContextOptions<MarketDbContext>();
            var mockContext = new Mock<MarketDbContext>(dbContextOptions);

            var mockUrunlerDbSet = new Mock<DbSet<Urun>>();

            if (urun != null)
            {
                mockUrunlerDbSet.Setup(m => m.FindAsync(urun.Id)).ReturnsAsync(urun);
            }
            else
            {
                mockUrunlerDbSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync((Urun)null);
            }

            mockContext.Setup(c => c.Urunler).Returns(mockUrunlerDbSet.Object);

            return mockContext;
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenEntityExists()
        {
            // Arrange
            var urun = new Urun { Id = 1, Ad = "Elma", Tur = "Meyve", BirimId = 1, CreatedById = 10, KayitTarihi = DateTime.UtcNow };

           
            var mockContext = SetupMockDbContext(urun);

           
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(1);
            
            var service = new UrunService(mockContext.Object);

            // Act
            var result = await service.DeleteAsync(1);

            // Assert
            Assert.True(result);

            mockContext.Verify(c => c.Urunler.Remove(urun), Times.Once);

           
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenEntityDoesNotExist()
        {
            // Arrange
            var mockContext = SetupMockDbContext(null);

         
            var service = new UrunService(mockContext.Object);

            // Act
            // Var olmayan bir ürün için silme işlemini gerçekleştiriyoruz.
            var result = await service.DeleteAsync(99); // Var olmayan bir Id

            // Assert
            Assert.False(result);

            mockContext.Verify(c => c.Urunler.Remove(It.IsAny<Urun>()), Times.Never);

           
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

    }
}


//diğer testleri tamamla    
//void metodlara unit test nasıl yazılır. 
//metod ne alıyor ne dönüyor
//hangi servislere bağımlı
//hangi case'leri test etmeliyim


