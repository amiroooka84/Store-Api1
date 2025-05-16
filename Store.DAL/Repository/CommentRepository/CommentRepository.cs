using Dapper;
using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Comment;
using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.CommentRepository
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(db db) : base(db)
        {
        }

        public IEnumerable<Comment> GetProductComments(int ProductId)
        {
            _connection.Open();
            var res = _connection.Query<Comment>("select * from Comments where ProductId = @ProductId", new { ProductId = ProductId });
            _connection.Close();
            return res;
        }
    }
}
