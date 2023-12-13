using AutoMapper;
using BL.Dtos;
using BL.Managers;
using EFDal.Entities;
using EFDal.Repositories;
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
    public class CampaignManagerTests
    {
        
        [TestMethod]
        public async Task GetCampaignsByProductId_ShouldReturnCampaigns()
        {
            // Arrange
            var campaignRepositoryMock = new Mock<ICampaignRepository>();
            var mapperMock = new Mock<IMapper>();

            var campaignManager = new CampaignManager(campaignRepositoryMock.Object, mapperMock.Object);

            var campaigns = new List<Campaign>
                 {
                     new Campaign { Id = 1, Name = "Campaign1", ProductId = 1 },
                     new Campaign { Id = 2, Name = "Campaign2", ProductId = 1 },
                     new Campaign { Id = 3, Name = "Campaign3", ProductId = 2 }
                 };

            campaignRepositoryMock.Setup(repo => repo.GetAll()).Returns(campaigns);

            // Act
            var result = await campaignManager.GetCampaignsByProductId(1);

            // Assert
            CollectionAssert.AreEqual(campaigns.Where(c => c.ProductId == 1).ToList(), result);
        }

    }
}
