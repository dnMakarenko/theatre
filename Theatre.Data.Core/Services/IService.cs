using Theatre.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Theatre.Data.Core.Services
{
    public interface IService<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid Id);
        T Create(T entity);
        void Delete(T entity);
        T Update(T entity);
        void Save();

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid Id);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task SaveAsync();
    }
}
