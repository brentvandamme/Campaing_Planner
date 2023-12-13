using AutoMapper;
using BL.Dtos;
using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class LocationManager : GenericManager<Location>, ILocationManager
    {
        ILocationRepository _locRepo;
        private readonly IMapper _mapper;

        public LocationManager(ILocationRepository repository, IMapper mapper) : base(repository)
        {
            _locRepo = repository;
            _mapper = mapper;
        }

        public async Task<int> AddLocationAsync(LocationCreationDto locDto)
        { 
            Location loc = _mapper.Map<Location>(locDto);

            return await _locRepo.AddAsync(loc);
        }
    }
}
