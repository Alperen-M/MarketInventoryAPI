using MarketInventory.Application.Services;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace MarketInventory.UnitTests.Tests
{
    public class KullaniciTuruServiceTests
    {
        private readonly Mock<IGenericRepository<KullaniciTuru>> _mockRepository;
        private readonly Mock<MarketDbContext> _mockContext;
        private readonly KullaniciTuruService _kullaniciTuruService;

        public KullaniciTuruServiceTests()
        {
            _mockRepository = new Mock<IGenericRepository<KullaniciTuru>>();
            _mockContext = new Mock<MarketDbContext>(new DbContextOptions<MarketDbContext>());
            _kullaniciTuruService = new KullaniciTuruService(_mockRepository.Object, _mockContext.Object);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllKullaniciTuru_FromRepository()
        {
            //Arrange
            var kullaniciTurleri = new List<KullaniciTuru>();
            {
                new KullaniciTuru { Id = 1, Ad="Admin"},
                new KullaniciTuru {Id = 2, Ad="M"
            }
        }
    }
}
