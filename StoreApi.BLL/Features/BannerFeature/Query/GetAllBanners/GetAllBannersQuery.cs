using MediatR;
using StoreApi.Entity._Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.BannerFeature.Query.GetAllBanners
{
    public class GetAllBannersQuery : IRequest<IEnumerable<Banner>>
    {
    }
}
