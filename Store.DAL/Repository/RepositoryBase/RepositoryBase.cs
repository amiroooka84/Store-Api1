using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreApi.DAL.DB;
using StoreApi.Entity;
using StoreApi.Entity._Product;
using System.Collections.Generic;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StoreApi.Entity._Order;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StoreApi.DAL.Repository.RepositoryBase
{
    public  class  RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        protected readonly db _db;
        protected readonly DbSet<T> _Query;
        protected readonly SqlConnection _connection;

        public RepositoryBase(db db)
        {
            _db = db;
            _Query = _db.Set<T>();
            _connection = new SqlConnection(ConStr.con);
        }

        public T Create(T entity)
        {
            var res = _Query.Add(entity).Entity;
            _db.SaveChanges();
            return res;
        }

        public T Delete(int id)
        {
            var table = _db.Model.FindEntityType(typeof(T)).GetTableName();
            _connection.Open();
            var res = _connection.QuerySingleOrDefault<T>("select * from " + table + " where id = @ID", new { ID = id });
            _connection.ExecuteScalar<T>("DELETE from "+table+" where id = @ID ;", new { ID = id});
            _connection.Close();
            return res;
        }

        public IEnumerable<T> GetAll()
        {
            var table = _db.Model.FindEntityType(typeof(T)).GetTableName();
            _connection.Open();
            var res = _connection.Query<T>("select * from " + table, new { TABLE = table });
            _connection.Close();
            return res;
        }

        public T GetById(int id)
        {
            string table = _db.Model.FindEntityType(typeof(T)).GetTableName();
            _connection.Open();
            var res = _connection.QuerySingleOrDefault<T>("select * from "+table+" where id = @ID", new { ID = id });
            _connection.Close();
            return res;
        }

        public T Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return entity;
        }
    }
}
