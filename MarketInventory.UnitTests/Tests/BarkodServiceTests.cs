using MarketInventory.Application.Services;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace MarketInventory.Tests
{
    public class BarkodServiceMockTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllBarkods_FromRepository()
        {
            // Arrange
            var barkodList = new List<Barkod>
            {
                new Barkod { Id = 1, Kod = "1234567890123" },
                new Barkod { Id = 2, Kod = "3210987654321" },
                new Barkod { Id = 3, Kod = "1122334455667" }
            };
            var mockRepo = new Mock<IBarkodRepository>();
            var service = new BarkodService(mockRepo.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
            Assert.Equal(barkodList, result);
        }
       
            [Fact]
            public async Task GetByIdAsync_ShouldReturnBarkod_WhenExists()
            {
                // Arrange 
                var barkod = new Barkod { Id = 1, Kod = "12345678901231" };

                var mockRepo = new Mock<IBarkodRepository>();

               
                mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(barkod);
                
                var service = new BarkodService(mockRepo.Object);
                
                // Act 
                var result = await service.GetByIdAsync(1);

                // Assert 
                Assert.NotNull(result);

                Assert.Equal(1, result.Id);

                Assert.Equal("12345678901231", result.Kod);
                
                mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once);
            }
        [Fact]
        public async Task AddAsync_ShouldAddBarkod_WhenBarkodDoesNotExist()
        {
            // Arrange (Hazırlık)
            var barkod = new Barkod { Kod = "1234567890123" };
            var mockRepo = new Mock<IBarkodRepository>();

            // Servisin içindeki BarkodExistsAsync metodu GetAllAsync'i çağırdığı için,
            // boş bir liste dönmesini mockluyoruz. Bu sayede BarkodExistsAsync false dönecektir.
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Barkod>());

            // AddAsync metodunu çağrıldığında hiçbir şey yapmasını taklit ediyoruz.
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Barkod>()))
                    .Returns(Task.CompletedTask);

            var service = new BarkodService(mockRepo.Object);

            // Act (Eylem)
            await service.AddAsync(barkod);

            // Assert (Doğrulama)
            // Servis içi BarkodExistsAsync metodunun çağırdığı GetAllAsync'in bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);

            // AddAsync metodunun doğru nesneyle bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.AddAsync(barkod), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenBarkodExists()
        {
            // Arrange (Hazırlık)
            var existingBarkod = new Barkod { Kod = "1234567890123" };
            var newBarkod = new Barkod { Kod = "1234567890123" }; // Aynı koda sahip nesne

            var mockRepo = new Mock<IBarkodRepository>();

            // Servisin içindeki BarkodExistsAsync metodunun true dönmesi için,
            // GetAllAsync'in içinde mevcut barkodun bulunduğu bir liste dönmesini mockluyoruz.
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Barkod> { existingBarkod });

            var service = new BarkodService(mockRepo.Object);

            // Act & Assert (Eylem ve Doğrulama)
            // Assert.ThrowsAsync kullanarak, beklenen InvalidOperationException hatasının fırlatıldığını doğrularız.
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(newBarkod));

            Assert.Equal("Bu barkod zaten mevcut.", exception.Message);

            // Servis içi BarkodExistsAsync metodunun çağırdığı GetAllAsync'in bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);

            // Önemli: Barkod mevcut olduğu için AddAsync metodunun hiçbir zaman çağrılmadığını doğrularız.
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Barkod>()), Times.Never);
        }


        [Fact]
        public async Task DeleteAsync_ShouldDeleteBarkod_WhenBarkodExists()
        {
            // Arrange (Hazırlık)
            var barkod = new Barkod { Id = 1, Kod = "1234567890123" };
            var mockRepo = new Mock<IBarkodRepository>();

            // Repository'nin DeleteAsync metodu çağrıldığında hiçbir şey yapmasını taklit ediyoruz.
            mockRepo.Setup(r => r.DeleteAsync(barkod)).Returns(Task.CompletedTask);

            var service = new BarkodService(mockRepo.Object);

            // Act (Eylem)
            await service.DeleteAsync(barkod);

            // Assert (Doğrulama)
            // Repository'deki DeleteAsync metodunun doğru nesneyle bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.DeleteAsync(barkod), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate_WhenCalled()
        {
            // Arrange (Hazırlık)
            // Güncellenecek Barkod nesnesini oluşturuyoruz.
            var barkod = new Barkod { Id = 1, Kod = "YeniKod" };

            // IBarkodRepository arayüzünü taklit eden bir Moq nesnesi oluşturulur.
            var mockRepo = new Mock<IBarkodRepository>();

            // Repository'nin UpdateAsync metodu çağrıldığında hiçbir şey yapmasını taklit ediyoruz.
            // Bu, metodun başarılı bir şekilde tamamlandığını varsayar.
            mockRepo.Setup(r => r.UpdateAsync(barkod)).Returns(Task.CompletedTask);

            // Servis sınıfı, sahte repository nesnesi ile oluşturulur.
            var service = new BarkodService(mockRepo.Object);

            // Act (Eylem)
            // Servis metodunu çağırıyoruz.
            await service.UpdateAsync(barkod);

            // Assert (Doğrulama)
            // Repository'deki UpdateAsync metodunun doğru nesneyle tam olarak bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.UpdateAsync(barkod), Times.Once);
        }
        [Fact]
        public async Task GetBarkodsByUrunIdAsync_ShouldReturnCorrectBarkods_WhenUrunIdExists()
        {
            // Arrange (Hazırlık)
            var urunId = 1;

            // Test için sahte bir barkod listesi oluşturuyoruz.
            // Bazı barkodların UrunId'si beklenen değerle (1) eşleşirken, bazıları farklıdır.
            var allBarkods = new List<Barkod>
            {
                new Barkod { Id = 1, UrunId = 1, Kod = "111" },
                new Barkod { Id = 2, UrunId = 2, Kod = "222" }, // Yanlış UrunId
                new Barkod { Id = 3, UrunId = 1, Kod = "333" },
                new Barkod { Id = 4, UrunId = 3, Kod = "444" }  // Yanlış UrunId
            };

            var mockRepo = new Mock<IBarkodRepository>();

            // Mock repository'nin GetAllAsync metodu çağrıldığında, yukarıdaki tüm listeyi dönmesini sağlıyoruz.
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(allBarkods);

            var service = new BarkodService(mockRepo.Object);

            // Act (Eylem)
            var result = await service.GetBarkodsByUrunIdAsync(urunId);

            // Assert (Doğrulama)
            // Sonucun null olmadığını kontrol ederiz.
            Assert.NotNull(result);

            // Dönen listenin eleman sayısının 2 olduğunu doğrularız.
            Assert.Equal(2, result.Count());

            // Dönen listenin tüm elemanlarının UrunId'sinin 1 olduğunu doğrularız.
            Assert.True(result.All(b => b.UrunId == urunId));

            // GetAllAsync metodunun repository'den tam olarak bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetBarkodsByUrunIdAsync_ShouldReturnEmptyList_WhenUrunIdDoesNotExist()
        {
            // Arrange (Hazırlık)
            var urunId = 99; // Var olmayan bir UrunId

            // Test için sahte bir barkod listesi oluşturuyoruz.
            var allBarkods = new List<Barkod>
            {
                new Barkod { Id = 1, UrunId = 1, Kod = "111" },
                new Barkod { Id = 2, UrunId = 2, Kod = "222" }
            };

            var mockRepo = new Mock<IBarkodRepository>();

            // Mock repository'nin GetAllAsync metodu çağrıldığında, yukarıdaki tüm listeyi dönmesini sağlıyoruz.
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(allBarkods);

            var service = new BarkodService(mockRepo.Object);

            // Act (Eylem)
            var result = await service.GetBarkodsByUrunIdAsync(urunId);

            // Assert (Doğrulama)
            // Sonucun null olmadığını kontrol ederiz.
            Assert.NotNull(result);

            // Dönen listenin eleman sayısının 0 olduğunu doğrularız.
            Assert.Empty(result);

            // GetAllAsync metodunun repository'den tam olarak bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }
        [Fact]
        public async Task BarkodExistsAsync_ShouldReturnTrue_WhenBarkodExists()
        {
            // Arrange (Hazırlık)
            var existingKod = "1234567890123";
            var allBarkods = new List<Barkod>
            {
                new Barkod { Id = 1, Kod = existingKod },
                new Barkod { Id = 2, Kod = "9876543210987" }
            };

            var mockRepo = new Mock<IBarkodRepository>();

            // Mock repository'nin GetAllAsync metodu çağrıldığında, içinde mevcut barkodun bulunduğu listeyi dönmesini sağlıyoruz.
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(allBarkods);

            var service = new BarkodService(mockRepo.Object);

            // Act (Eylem)
            var result = await service.BarkodExistsAsync(existingKod);

            // Assert (Doğrulama)
            // Sonucun true olduğunu doğrularız.
            Assert.True(result);

            // GetAllAsync metodunun repository'den bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task BarkodExistsAsync_ShouldReturnFalse_WhenBarkodDoesNotExist()
        {
            // Arrange (Hazırlık)
            var nonExistingKod = "9999999999999";
            var allBarkods = new List<Barkod>
            {
                new Barkod { Id = 1, Kod = "1234567890123" },
                new Barkod { Id = 2, Kod = "9876543210987" }
            };

            var mockRepo = new Mock<IBarkodRepository>();

            // Mock repository'nin GetAllAsync metodu çağrıldığında, aranan barkodun olmadığı listeyi dönmesini sağlıyoruz.
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(allBarkods);

            var service = new BarkodService(mockRepo.Object);

            // Act (Eylem)
            var result = await service.BarkodExistsAsync(nonExistingKod);

            // Assert (Doğrulama)
            // Sonucun false olduğunu doğrularız.
            Assert.False(result);

            // GetAllAsync metodunun repository'den bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}

