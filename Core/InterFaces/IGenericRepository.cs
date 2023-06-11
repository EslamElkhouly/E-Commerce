using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InterFaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpecifications(ISpecificatios<T> specifications);  
        Task <IReadOnlyList<T>> ListAsync(ISpecificatios<T> specifications);  
        Task <int> CountAsync(ISpecificatios<T> specifications);  

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    
        
    }
}
