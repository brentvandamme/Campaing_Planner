using AutoMapper;
using BL.Dtos;
using BL.Managers;
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
        [TestMethod]
        public async Task AddAsync_ShouldReturnProductId()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepositrory>();
            var campaignRepositoryMock = new Mock<ICampaignRepository>();
            var mapperMock = new Mock<IMapper>();

            var productManager = new ProductManager(productRepositoryMock.Object, campaignRepositoryMock.Object, mapperMock.Object);

            var productDto = new ProductAddingDto
            {
                Price = "10.99",
                MaxAvailableCapacity = 100,
                Name = "TestProduct"
            };

            productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(1);

            // Act
            var productId = await productManager.AddAsync(productDto);

            // Assert
            Assert.AreEqual(1, productId);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepositrory>();
            var campaignRepositoryMock = new Mock<ICampaignRepository>();
            var mapperMock = new Mock<IMapper>();

            var productManager = new ProductManager(productRepositoryMock.Object, campaignRepositoryMock.Object, mapperMock.Object);

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1" },
                new Product { Id = 2, Name = "Product2" }
            };

            productRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var result = await productManager.GetAllProductsAsync();

            // Assert
            CollectionAssert.AreEqual(products, result);
        }
    }
}
