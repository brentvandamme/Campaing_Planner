﻿using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories;
using EFDal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class GenericManager<TEntity> : IGenericManager<TEntity>
        where TEntity : BaseEntity
    {
        internal readonly IGenericRepository<TEntity> _repository;

        public GenericManager(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public virtual List<TEntity> GetAll()
        {
            return _repository.GetAll();
        }
        public virtual TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }
        public virtual int Add(TEntity entity)
        {
            return _repository.Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<int> AddAsync(TEntity entity)
        {
           return _repository.AddAsync(entity);
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
