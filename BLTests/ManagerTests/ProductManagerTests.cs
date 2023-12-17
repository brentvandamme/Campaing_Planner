using AutoMapper;
using BL.Dtos;
using BL.Managers;
using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests.ManagerTests
{
    [TestClass]
    public class ProductManagerTests
    {
        private Mock<IProductRepositrory> productRepositoryMock;
        private Mock<ICampaignRepository> campaignRepositoryMock;
        private Mock<IMapper> mapperMock;
        private ProductManager productManager;

        [TestInitialize]
        public void TestInitialize()
        {
            productRepositoryMock = new Mock<IProductRepositrory>();
            campaignRepositoryMock = new Mock<ICampaignRepository>();
            mapperMock = new Mock<IMapper>();
            productManager = new ProductManager(productRepositoryMock.Object, campaignRepositoryMock.Object, mapperMock.Object);
        }

        [TestMethod]
        public async Task AddAsync_ShouldReturnProductId()
        {
            var productDto = new ProductAddingDto
            {
                Price = "10.99",
                MaxAvailableCapacity = 100,
                Name = "TestProduct"
            };

            // act is repo when AddAsync is called and return a id
            productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(1);

            // Act
            var productId = await productManager.AddAsync(productDto);

            // Assert
            Assert.AreEqual(1, productId);
        }

        [TestMethod]
        public async Task AddProductWithCampaignsAsync_ShouldAddProductAndCampaigns()
        {
            // Arrange
            var productDto = new ProductAddingDto
            {
                Name = "TestProduct",
                Price = "10.99",
                MaxAvailableCapacity = 100,
            };

            var campaigns = new List<Campaign>
            {
                new Campaign { Id = 1 },
                new Campaign { Id = 2 }
            };


            productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ReturnsAsync(1);
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new Product { Id = 1 });

            // Act
            var productId = await productManager.AddProductWithCampaignsAsync(productDto, campaigns);

            // Assert
            Assert.AreEqual(1, productId);

        }
        [TestMethod]
        public async Task GetAllProductsWithFreeSpots_ShouldReturnProducts()
        {
            // Arrange
            var productsWithFreeSpots = new List<Product>
                {
                new Product { Id = 1, Name = "Product1", MaxAvailableCapacity = 50 },
                new Product { Id = 2, Name = "Product2", MaxAvailableCapacity = 75 }
                };

            productRepositoryMock.Setup(repo => repo.GetAllProductsWithFreeCapacity()).ReturnsAsync(productsWithFreeSpots);

            // Act
            var result = await productManager.GetAllProductsWithFreeSpots();

            // Assert
            CollectionAssert.AreEquivalent(productsWithFreeSpots, result);
        }
    }
}
