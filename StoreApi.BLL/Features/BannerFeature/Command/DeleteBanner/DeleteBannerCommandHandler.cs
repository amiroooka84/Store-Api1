using MediatR;
using StoreApi.DAL.Repository.BannerRepository;
using StoreApi.Entity._Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.BannerFeature.Command.DeleteBanner
{
    public class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommand, Banner>
    {
        private readonly IBannerRepository _bannerRepository;

        public DeleteBannerCommandHandler(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }
        public Task<Banner> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
        {
            var res = _bannerRepository.Delete(request.id);
            return Task.FromResult(res);
        }
    }
}
