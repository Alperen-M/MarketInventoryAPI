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

            // Doğrulama için kullanılacak olan BarkodExistsAsync metodunu false dönecek şekilde ayarlıyoruz.
            mockRepo.Setup(r => r.BarkodExistsAsync(barkod.Kod)).ReturnsAsync(false);

            // AddAsync metodunu çağrıldığında hiçbir şey yapmasını taklit ediyoruz.
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Barkod>()))
                    .Returns(Task.CompletedTask);

            var service = new BarkodService(mockRepo.Object);

            // Act (Eylem)
            await service.AddAsync(barkod);

            // Assert 

            
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenBarkodExists()
        {
            // Arrange (Hazırlık)
            var barkod = new Barkod { Kod = "1234567890123" };
            var mockRepo = new Mock<IBarkodRepository>();

            // Mock'u herhangi bir string değeri için true dönecek şekilde ayarlıyoruz.
            mockRepo.Setup(r => r.BarkodExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var service = new BarkodService(mockRepo.Object);

            // Act & Assert (Eylem ve Doğrulama)
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(barkod));

            Assert.Equal("Bu barkod zaten mevcut.", exception.Message);

            // BarkodExistsAsync metodunun herhangi bir string ile bir kez çağrıldığını doğrularız.
            mockRepo.Verify(r => r.BarkodExistsAsync(It.IsAny<string>()), Times.Once);

            // AddAsync metodunun hiçbir zaman çağrılmadığını doğrularız.
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
    }
}

