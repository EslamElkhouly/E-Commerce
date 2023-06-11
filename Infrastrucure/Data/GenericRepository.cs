using Core.InterFaces;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;

namespace Infrastrucure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _Context;
        public GenericRepository(StoreDbContext context)
        {
            _Context = context;
        }

        public void Add(T entity)
          => _Context.Set<T>().Add(entity);

        public void Delete(T entity)
                  => _Context.Set<T>().Remove(entity);

        public void Update(T entity)
                 => _Context.Set<T>().Update(entity);


        public async Task<T> GetByIdAsync(int id)
       => await _Context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> ListAllAsync()
               => await _Context.Set<T>().ToListAsync();

        public async Task<T> GetEntityWithSpecifications(ISpecificatios<T> specifications)
         => await ApplySpecifications(specifications).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAsync(ISpecificatios<T> specifications)
                => await ApplySpecifications(specifications).ToListAsync();

        private IQueryable<T> ApplySpecifications(ISpecificatios<T> specificatios) 
            => SpecificationEvaluator<T>.GetQuery(_Context.Set<T>(), specificatios);

        public async Task<int> CountAsync(ISpecificatios<T> specifications)
       => await ApplySpecifications(specifications).CountAsync();   
    }
}
