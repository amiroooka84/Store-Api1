using Dapper;
using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.LikeRepository
{
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        public LikeRepository(db db) : base(db)
        {
        }

        public Like DeleteByProductIdAndUserId(Like value)
        {
            _connection.Open();
            var res = _connection.QueryFirstOrDefault<Like>("select * from Likes where UserId = @USERID and ProductId = @PRODUCTID ", new { USERID = value.UserId, PRODUCTID = value.ProductId });
            _connection.ExecuteScalar<Like>("DELETE from Likes where UserId = @USERID and ProductId = @PRODUCTID  ;", new { USERID = value.UserId , PRODUCTID = value.ProductId });
            _connection.Close();
            return res;
        }
    }
}
