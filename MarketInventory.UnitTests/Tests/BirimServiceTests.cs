using MarketInventory.Application.Services;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace MarketInventory.Tests
{
    public class BirimServiceTests
    {
        // Mock nesnesini ve test edilecek servisi tanımlıyoruz.
        private readonly Mock<IGenericRepository<Birim>> _mockRepository;
        private readonly BirimService _birimService;

        // Her testten önce çalışacak constructor.
        public BirimServiceTests()
        {
            _mockRepository = new Mock<IGenericRepository<Birim>>();
            // Gerçek BirimService'i, sahte repository ile başlatıyoruz.
            _birimService = new BirimService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetActiveBirimlerAsync_ShouldReturnActiveBirimler()
        {
            // Arrange (Hazırlık)
            // Aktif ve pasif birimleri içeren bir liste oluşturuyoruz.
            var allBirimler = new List<Birim>
            {
                new Birim { Id = 1, Ad = "KG", Carpan = 1 },
                new Birim { Id = 2, Ad = "Adet", Carpan = 0 }, // Bu pasif olacak
                new Birim { Id = 3, Ad = "Litre", Carpan = 2 }
            };

            // Repository'nin GetAllAsync metodunun tüm birimleri dönmesini ayarlıyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allBirimler);

            // Act (Eylem)
            var result = await _birimService.GetActiveBirimlerAsync();

            // Assert (Doğrulama)
            // Sonuç null olmamalı ve 2 öğe içermeli.
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            // Sonuçta dönen tüm birimlerin çarpanı 0'dan büyük olmalı.
            Assert.True(result.All(b => b.Carpan > 0));
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnBirim_WhenNameExists()
        {
            // Arrange (Hazırlık)
            var birimAdi = "Kilogram";
            var allBirimler = new List<Birim>
            {
                new Birim { Id = 1, Ad = "Kilogram", Carpan = 1000 },
                new Birim { Id = 2, Ad = "Litre", Carpan = 1000 }
            };

            // Repository'nin GetAllAsync metodunun tüm birimleri dönmesini ayarlıyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allBirimler);

            // Act (Eylem)
            var result = await _birimService.GetByNameAsync(birimAdi);

            // Assert (Doğrulama)
            // Sonucun null olmadığını ve doğru birim olduğunu kontrol ediyoruz.
            Assert.NotNull(result);
            Assert.Equal(birimAdi, result.Ad);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnNull_WhenNameDoesNotExist()
        {
            // Arrange (Hazırlık)
            var allBirimler = new List<Birim>
            {
                new Birim { Id = 1, Ad = "Kilogram", Carpan = 1000 }
            };

            // Repository'nin GetAllAsync metodunun tüm birimleri dönmesini ayarlıyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allBirimler);

            // Act (Eylem)
            var result = await _birimService.GetByNameAsync("Litre");

            // Assert (Doğrulama)
            // Sonucun null olduğunu kontrol ediyoruz.
            Assert.Null(result);
        }

        [Fact]
        public async Task IsNameUniqueAsync_ShouldReturnTrue_WhenNameIsUnique()
        {
            // Arrange (Hazırlık)
            var allBirimler = new List<Birim>
            {
                new Birim { Id = 1, Ad = "Kilogram", Carpan = 1000 }
            };

            // Repository'nin GetAllAsync metodunun birimleri dönmesini ayarlıyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allBirimler);

            // Act (Eylem)
            var result = await _birimService.IsNameUniqueAsync("Litre");

            // Assert (Doğrulama)
            // Sonucun true olduğunu kontrol ediyoruz.
            Assert.True(result);
        }

        [Fact]
        public async Task IsNameUniqueAsync_ShouldReturnFalse_WhenNameIsNotUnique()
        {
            // Arrange (Hazırlık)
            var allBirimler = new List<Birim>
            {
                new Birim { Id = 1, Ad = "Kilogram", Carpan = 1000 }
            };

            // Repository'nin GetAllAsync metodunun birimleri dönmesini ayarlıyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allBirimler);

            // Act (Eylem)
            var result = await _birimService.IsNameUniqueAsync("Kilogram");

            // Assert (Doğrulama)
            // Sonucun false olduğunu kontrol ediyoruz.
            Assert.False(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddBirim_WhenNameIsUnique()
        {
            // Arrange (Hazırlık)
            var newBirim = new Birim { Ad = "Adet", Carpan = 1 };
            // IsNameUniqueAsync'in true dönmesi için GetAllAsync'i boş bir liste dönecek şekilde mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Birim>());
            // AddAsync çağrısını mockluyoruz.
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Birim>())).Returns(Task.CompletedTask);

            // Act (Eylem)
            // Metodun bir hata fırlatmadığından emin olmak için Assert.ThrowsAsync kullanmıyoruz.
            await _birimService.AddAsync(newBirim);

            // Assert (Doğrulama)
            // AddAsync metodunun tam olarak bir kez çağrıldığını doğruluyoruz.
            _mockRepository.Verify(r => r.AddAsync(newBirim), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenNameIsNotUnique()
        {
            // Arrange (Hazırlık)
            var existingBirimler = new List<Birim> { new Birim { Id = 1, Ad = "Adet", Carpan = 1 } };
            var newBirim = new Birim { Ad = "Adet", Carpan = 1 };
            // IsNameUniqueAsync'in false dönmesi için GetAllAsync'i dolu bir liste dönecek şekilde mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(existingBirimler);

            // Act & Assert (Eylem ve Doğrulama)
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _birimService.AddAsync(newBirim));

            Assert.Equal("Birim adı zaten mevcut.", exception.Message);
            // Repository'deki AddAsync metodunun hiçbir zaman çağrılmadığını doğruluyoruz.
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Birim>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateBirim_WhenBirimExistsAndNameIsUnique()
        {
            // Arrange (Hazırlık)
            var existingBirim = new Birim { Id = 1, Ad = "Adet", Carpan = 1 };
            var updatedBirim = new Birim { Id = 1, Ad = "Güncel Adet", Carpan = 2 };

            // GetByIdAsync metodunu mockluyoruz.
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBirim);
            // IsNameUniqueAsync'in true dönmesi için GetAllAsync'i boş bir liste dönecek şekilde mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Birim>());
            // UpdateAsync metodunu mockluyoruz.
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Birim>())).Returns(Task.CompletedTask);

            // Act (Eylem)
            await _birimService.UpdateAsync(updatedBirim);

            // Assert (Doğrulama)
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Birim>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenBirimDoesNotExist()
        {
            // Arrange (Hazırlık)
            var updatedBirim = new Birim { Id = 1, Ad = "Güncel Adet", Carpan = 2 };

            // GetByIdAsync'i null dönecek şekilde mockluyoruz.
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Birim)null);

            // Act & Assert (Eylem ve Doğrulama)
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _birimService.UpdateAsync(updatedBirim));

            Assert.Equal("Birim bulunamadı.", exception.Message);
            // Hiçbir güncelleme işlemi yapılmadığını doğruluyoruz.
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Birim>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenNewNameIsNotUnique()
        {
            // Arrange (Hazırlık)
            var existingBirim = new Birim { Id = 1, Ad = "Adet", Carpan = 1 };
            var otherBirim = new Birim { Id = 2, Ad = "Litre", Carpan = 1 };
            var updatedBirim = new Birim { Id = 1, Ad = "Litre", Carpan = 2 };

            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBirim);
            // GetAllAsync'i, içinde "Litre" birimi bulunan bir liste dönecek şekilde mockluyoruz.
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Birim> { existingBirim, otherBirim });

            // Act & Assert (Eylem ve Doğrulama)
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _birimService.UpdateAsync(updatedBirim));

            Assert.Equal("Birim adı zaten mevcut.", exception.Message);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Birim>()), Times.Never);
        }
    }
}
