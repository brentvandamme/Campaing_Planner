﻿using BL.Dtos;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface ILocationManager : IGenericManager<Location>
    {
        Task<int> AddLocationAsync(LocationCreationDto locDto);
    }
}
