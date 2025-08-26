using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MarketInventory.Application.Services;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.Tests
{
    public class KullaniciServiceTests
    {
        // Mock nesnelerini ve test edilecek servisi tanımlıyoruz.
        private readonly Mock<IGenericRepository<Kullanici>> _mockRepository;
        private readonly Mock<MarketDbContext> _mockContext;
        private readonly KullaniciService _kullaniciService;

        // Her testten önce çalışacak constructor.
        public KullaniciServiceTests()
        {
            _mockRepository = new Mock<IGenericRepository<Kullanici>>();
            _mockContext = new Mock<MarketDbContext>(new DbContextOptions<MarketDbContext>());

            // Gerçek KullaniciService'i, sahte repository ve context ile başlatıyoruz.
            _kullaniciService = new KullaniciService(_mockRepository.Object, _mockContext.Object);
        }

        [Fact]
        public async Task GetKullaniciWithDetayAsync_ShouldReturnKullanici_WhenIdExists()
        {
            // Arrange (Hazırlık)
            var userId = 1;
            var allKullanicilar = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "test@example.com" },
                new Kullanici { Id = 2, KullaniciAdi = "test2@example.com" }
            };

            // Repository'nin GetAllAsync metodunun tüm kullanıcıları dönmesini ayarlıyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allKullanicilar);

            // Act (Eylem)
            var result = await _kullaniciService.GetKullaniciWithDetayAsync(userId);

            // Assert (Doğrulama)
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("test@example.com", result.KullaniciAdi);
        }

        [Fact]
        public async Task GetKullaniciWithDetayAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange (Hazırlık)
            var allKullanicilar = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "test@example.com" }
            };

            // Repository'nin GetAllAsync metodunun tüm kullanıcıları dönmesini ayarlıyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allKullanicilar);

            // Act (Eylem)
            var result = await _kullaniciService.GetKullaniciWithDetayAsync(99);

            // Assert (Doğrulama)
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnKullanici_WhenEmailExists()
        {
            // Arrange (Hazırlık)
            var email = "test@example.com";
            var allKullanicilar = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "test@example.com" },
                new Kullanici { Id = 2, KullaniciAdi = "other@example.com" }
            };

            // GetAllAsync çağrısını mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allKullanicilar);

            // Act (Eylem)
            var result = await _kullaniciService.GetByEmailAsync(email);

            // Assert (Doğrulama)
            Assert.NotNull(result);
            Assert.Equal(email, result.KullaniciAdi);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnKullanici_WithCaseInsensitiveMatch()
        {
            // Arrange (Hazırlık)
            var email = "TEST@example.com";
            var allKullanicilar = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "test@example.com" }
            };

            // GetAllAsync çağrısını mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allKullanicilar);

            // Act (Eylem)
            var result = await _kullaniciService.GetByEmailAsync(email);

            // Assert (Doğrulama)
            Assert.NotNull(result);
            Assert.Equal("test@example.com", result.KullaniciAdi);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnNull_WhenEmailDoesNotExist()
        {
            // Arrange (Hazırlık)
            var allKullanicilar = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "test@example.com" }
            };

            // GetAllAsync çağrısını mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allKullanicilar);

            // Act (Eylem)
            var result = await _kullaniciService.GetByEmailAsync("nonexistent@example.com");

            // Assert (Doğrulama)
            Assert.Null(result);
        }

        [Fact]
        public async Task IsEmailUniqueAsync_ShouldReturnTrue_WhenEmailIsUnique()
        {
            // Arrange (Hazırlık)
            var allKullanicilar = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "test@example.com" }
            };

            // GetAllAsync çağrısını mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allKullanicilar);

            // Act (Eylem)
            var result = await _kullaniciService.IsEmailUniqueAsync("new@example.com");

            // Assert (Doğrulama)
            Assert.True(result);
        }

        [Fact]
        public async Task IsEmailUniqueAsync_ShouldReturnFalse_WhenEmailIsNotUnique()
        {
            // Arrange (Hazırlık)
            var allKullanicilar = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "test@example.com" }
            };

            // GetAllAsync çağrısını mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allKullanicilar);

            // Act (Eylem)
            var result = await _kullaniciService.IsEmailUniqueAsync("TEST@example.com");

            // Assert (Doğrulama)
            Assert.False(result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnKullanici_WhenCredentialsAreCorrect()
        {
            // Arrange (Hazırlık)
            var email = "test@example.com";
            var password = "password123";

            // DbContext'i mocklamak için IQueryable nesnesi oluşturuyoruz.
            var data = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "test@example.com", Sifre = "password123", KullaniciTuru = (KullaniciTuru)Enum.Parse(typeof(KullaniciTuru), "Admin") } // Burası düzeltildi
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Kullanici>>();
            // IQueryable metotlarını mockluyoruz.
            mockDbSet.As<IQueryable<Kullanici>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Kullanici>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Kullanici>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Kullanici>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Context'teki DbSet özelliğini mockluyoruz.
            _mockContext.Setup(c => c.Kullanicilar).Returns(mockDbSet.Object);

            // Act (Eylem)
            var result = await _kullaniciService.LoginAsync(email, password);

            // Assert (Doğrulama)
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("test@example.com", result.KullaniciAdi);
            Assert.Equal((KullaniciTuru)Enum.Parse(typeof(KullaniciTuru), "Admin"), result.KullaniciTuru); // Burası düzeltildi
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenCredentialsAreIncorrect()
        {
            // Arrange (Hazırlık)
            var email = "test@example.com";
            var password = "wrongpassword";

            var data = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "test@example.com", Sifre = "password123", KullaniciTuru = (KullaniciTuru)Enum.Parse(typeof(KullaniciTuru), "Admin") } // Burası düzeltildi
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Kullanici>>();
            mockDbSet.As<IQueryable<Kullanici>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Kullanici>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Kullanici>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Kullanici>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Kullanicilar).Returns(mockDbSet.Object);

            // Act (Eylem)
            var result = await _kullaniciService.LoginAsync(email, password);

            // Assert (Doğrulama)
            Assert.Null(result);
        }

        [Fact]
        public async Task GetActiveKullanicilarAsync_ShouldReturnKullanicilar_WhenSilinmeTarihiIsNull()
        {
            // Arrange (Hazırlık)
            var allKullanicilar = new List<Kullanici>
            {
                new Kullanici { Id = 1, KullaniciAdi = "aktif1@example.com", SilinmeTarihi = null },
                new Kullanici { Id = 2, KullaniciAdi = "aktif2@example.com", SilinmeTarihi = null },
                new Kullanici { Id = 3, KullaniciAdi = "pasif@example.com", SilinmeTarihi = DateTime.Now } // Pasif kullanıcı
            };

            // GetAllAsync çağrısını mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allKullanicilar);

            // Act (Eylem)
            var result = await _kullaniciService.GetActiveKullanicilarAsync();

            // Assert (Doğrulama)
            Assert.NotNull(result);
            // Sonucun sadece iki aktif kullanıcıyı içermesi gerektiğini doğrularız.
            Assert.Equal(2, result.Count());
            // Tüm kullanıcıların SilinmeTarihi'nin null olduğunu doğrularız.
            Assert.True(result.All(k => k.SilinmeTarihi == null));
        }
    }
}
