using StoreApi.Entity._Product;
using System.Collections.Generic;

namespace StoreApi.DAL.Repository.RepositoryBase
{
    public interface IRepository<T>
    {
         T Create(T entity);
         T Update(T entity);
         T Delete(int id);
         IEnumerable<T> GetAll();
         T GetById(int id);

    }
}