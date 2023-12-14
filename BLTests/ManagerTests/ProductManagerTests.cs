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

            // act is repo when AddAsync is called and return a id
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

            // act as the repo to return list of products when GetAllAsync is called
            productRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var result = await productManager.GetAllProductsAsync();

            // Assert
            CollectionAssert.AreEqual(products, result);
        }

        [TestMethod]
        public async Task GetProductCapacity_ShouldReturnCorrectCapacity()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepositrory>();
            var campaignRepositoryMock = new Mock<ICampaignRepository>();
            var mapperMock = new Mock<IMapper>();
            var productManager = new ProductManager(productRepositoryMock.Object, campaignRepositoryMock.Object, mapperMock.Object);

            var productId = 1;
            var product = new Product
            {
                Id = productId,
                MaxAvailableCapacity = 10
            };

            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);
            campaignRepositoryMock.Setup(repo => repo.GetNumberOfLinkedCampaignsToProduct(productId)).ReturnsAsync(2);

            // Act
            var capacity = await productManager.GetProductCapacity(productId);

            // Assert
            Assert.AreEqual(8, capacity); // MaxCapacity is 10 and 2 campaigns are linked --> 10-2 =8
        }

        [TestMethod]
        public async Task AddProductWithCampaignsAsync_ShouldAddProductAndCampaigns()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepositrory>();
            var campaignRepositoryMock = new Mock<ICampaignRepository>();
            var mapperMock = new Mock<IMapper>();
            var productManager = new ProductManager(productRepositoryMock.Object, campaignRepositoryMock.Object, mapperMock.Object);

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
    }
}
