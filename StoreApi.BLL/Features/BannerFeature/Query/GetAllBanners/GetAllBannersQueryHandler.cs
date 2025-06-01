using MediatR;
using StoreApi.DAL.Repository.BannerRepository;
using StoreApi.Entity._Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.BannerFeature.Query.GetAllBanners
{
    public class GetAllBannersQueryHandler : IRequestHandler<GetAllBannersQuery , IEnumerable<Banner>>
    {
        private readonly IBannerRepository _bannerRepository;

        public GetAllBannersQueryHandler(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public Task<IEnumerable<Banner>> Handle(GetAllBannersQuery request, CancellationToken cancellationToken)
        {
            var res = _bannerRepository.GetAll();
            return Task.FromResult(res);
        }
    }
}
