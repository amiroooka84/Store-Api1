using MediatR;
using StoreApi.Entity._Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.BannerFeature.Command.AddBanner
{
    public class AddBannerCommand : IRequest<Banner>
    {
        public Banner Banner { get; set; }
    }
}
