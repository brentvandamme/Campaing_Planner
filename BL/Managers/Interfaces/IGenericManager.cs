using BL.Dtos;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IGenericManager<TEntity>
        where TEntity : BaseEntity
    {
        List<TEntity> GetAll();

        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id);

        int Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<int> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
    }
}
