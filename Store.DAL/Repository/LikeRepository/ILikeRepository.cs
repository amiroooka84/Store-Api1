using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.LikeRepository
{
    public interface ILikeRepository : IRepository<Like>
    {
        Like DeleteByProductIdAndUserId(Like value);
        Like GetByProductIdAndUserId(Like value);
    }
}