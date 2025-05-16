using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.CommentRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetProductComments(int ProductId);
    }
}
