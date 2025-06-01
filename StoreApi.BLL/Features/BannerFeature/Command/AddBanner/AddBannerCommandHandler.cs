using MediatR;
using StoreApi.DAL.Repository.BannerRepository;
using StoreApi.Entity._Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.BannerFeature.Command.AddBanner
{
    public class AddBannerCommandHandler : IRequestHandler<AddBannerCommand, Banner>
    {
        private readonly IBannerRepository _bannerRepository;

        public AddBannerCommandHandler(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public Task<Banner> Handle(AddBannerCommand request, CancellationToken cancellationToken)
        {
           var res = _bannerRepository.Create(request.Banner);
            return Task.FromResult(res);
        }
    }
}
