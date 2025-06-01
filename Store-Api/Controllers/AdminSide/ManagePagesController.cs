using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Features.BannerFeature.Command.AddBanner;
using StoreApi.BLL.Features.BannerFeature.Command.DeleteBanner;
using StoreApi.BLL.Features.BannerFeature.Query.GetAllBanners;
using StoreApi.Entity._Banner;
using StoreApi.Models.FieldsRequest.AdminSide.ManagePages;
using StoreApi.Models.FieldsRequest.IDField;

namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

    public class ManagePagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagePagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddBanner(AddBannerFieldRequest bannerFieldRequest)
        {
            Banner banner = new Banner() 
            { 
                BannerImage = bannerFieldRequest.BannerPath 
            };
            Banner res = await _mediator.Send(new AddBannerCommand() { Banner = banner });
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBanner(IntIdField id)
        {
            Banner res = await _mediator.Send(new DeleteBannerCommand() { id = id.id });
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBanners()
        {
            var res = await _mediator.Send(new GetAllBannersQuery());
            return Ok(res);
        }
    }
}
