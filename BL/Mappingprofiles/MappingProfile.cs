using AutoMapper;
using BL.Dtos;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mappingprofiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LocationCreationDto, Location>();

            CreateMap<CampaignDto, Campaign>();

            CreateMap<CustomerCreationDto, Customer>();

            CreateMap<ProductAddingDto, Product>();

            CreateMap<Product, Product>();
        }
    }
}
