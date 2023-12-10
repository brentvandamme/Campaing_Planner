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

        public LocationManager(ILocationRepository repository) : base(repository)
        {
            _locRepo = repository;
        }

        public int AddLocation(LocationCreationDto locDto)
        {
            //todo - automapper
            Location loc = new();
            loc.Street = locDto.Street;
            loc.City = locDto.City;
            loc.Number = locDto.Number;
            loc.ExtraInfo = locDto.ExtraInfo;
            loc.Name= locDto.Name;
            loc.Zip= locDto.Zip;
            return _locRepo.Add(loc);
        }
    }
}
