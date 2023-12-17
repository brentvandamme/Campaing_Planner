using AutoMapper;
using BL.Dtos;
using BL.Managers;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLTests.ManagerTests
{
    [TestClass]
    public class CampaignManagerTests
    {
        private Mock<ICampaignRepository> campaignRepositoryMock;
        private Mock<IMapper> mapperMock;
        private CampaignManager campaignManager;

        [TestInitialize]
        public void TestInitialize()
        {
            campaignRepositoryMock = new Mock<ICampaignRepository>();
            mapperMock = new Mock<IMapper>();
            campaignManager = new CampaignManager(campaignRepositoryMock.Object, mapperMock.Object);
        }

        [TestMethod]
        public void Add_ShouldReturnCampaignId()
        {
            // Arrange
            var campaignDto = new CampaignDto
            {
                Name = "TestCampaign",
                // Add other properties as needed...
            };

            mapperMock.Setup(mapper => mapper.Map<Campaign>(campaignDto)).Returns(new Campaign { Id = 1 });

            campaignRepositoryMock.Setup(repo => repo.Add(It.IsAny<Campaign>())).Returns(1);

            // Act
            var campaignId = campaignManager.Add(campaignDto);

            // Assert
            Assert.AreEqual(1, campaignId);
        }

        [TestMethod]
        public async Task AddAsync_ShouldReturnCampaignId()
        {
            // Arrange
            var campaignDto = new CampaignDto
            {
                Name = "TestCampaign",
            };

            mapperMock.Setup(mapper => mapper.Map<Campaign>(campaignDto)).Returns(new Campaign { Id = 1 });

            campaignRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Campaign>())).ReturnsAsync(1);

            // Act
            var campaignId = await campaignManager.AddAsync(campaignDto);

            // Assert
            Assert.AreEqual(1, campaignId);
        }

        [TestMethod]
        public async Task GetCampaignsByProductIdAsync_ShouldReturnListOfCampaigns()
        {
            // Arrange
            var productId = 1;
            var campaigns = new List<Campaign>
            {
                new Campaign { Id = 1, Name = "Campaign1" },
                new Campaign { Id = 2, Name = "Campaign2" }
            };

            campaignRepositoryMock.Setup(repo => repo.GetCampaignsByProductId(productId)).ReturnsAsync(campaigns);

            // Act
            var result = await campaignManager.GetCampaignsByProductIdAsync(productId);

            // Assert
            CollectionAssert.AreEqual(campaigns, result);
        }
    }
}
