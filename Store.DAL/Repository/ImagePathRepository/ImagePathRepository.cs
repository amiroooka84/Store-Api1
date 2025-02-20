using Dapper;
using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Image;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.ImagePathRepository
{
    public class ImagePathRepository : RepositoryBase<ImagePath>, IImagePathRepository
    {
        public ImagePathRepository(db db) : base(db)
        {
        }


        public void DeleteByProductId(int productId)
        {
            _connection.Open();
            _connection.Execute("delete from ImagesPath where ProductId = @ProductId ;", new { ProductId = productId });
            _connection.Close();
        }

        public IEnumerable<ImagePath> GetByProductId(int productId)
        {
            _connection.Open();
            var res = _connection.Query<ImagePath>("select * from ImagesPath where ProductId = @ProductId ;", new { ProductId = productId });
            _connection.Close();
            return res;
        }
    }
}
