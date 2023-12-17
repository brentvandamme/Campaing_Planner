using AutoMapper;
using BL.Dtos;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            CreateMap<ProductAddingDto, Product>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => ConvertToFloat(src.Price)));

            CreateMap<Product, Product>();
        }
        private float? ConvertToFloat(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (float.TryParse(value, out float result))
            {
                return result;
            }

            return null;
        }
    }
}
