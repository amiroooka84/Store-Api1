using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.Entity._Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL.Repository.BannerRepository
{
    public class BannerRepository : RepositoryBase<Banner>, IBannerRepository
    {
        public BannerRepository(db db) : base(db)
        {
        }
    }
}
