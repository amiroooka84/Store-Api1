using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Models.FieldsRequest.AdminSide.ManagePages;
using StoreApi.Models.FieldsRequest.IDField;

namespace StoreApi.Controllers.AdminSide
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

    public class ManagePagesController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddBanner(AddBannerFieldRequest bannerFieldRequest)
        {
            using (var file = new XLWorkbook("Banners.xlsx"))
            {
                var workSheet = file.Worksheet(1);

                int rowNum = workSheet.LastRowUsed().RowNumber();

                workSheet.Row(rowNum).InsertRowsBelow(1).FirstOrDefault().Cell(1).Value = bannerFieldRequest.BannerPath;

                file.Save();
                return Ok();
            }
        }

        [HttpDelete]
        public IActionResult DeleteBanner(IntIdField id)
        {
            using (var file = new XLWorkbook("Banners.xlsx"))
            {
                var workSheet = file.Worksheet(1);            
                workSheet.Row(id.id).Delete();
                file.Save();
                return Ok();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetBanners()
        {
            using (var file = new XLWorkbook("Banners.xlsx"))
            {
                var workSheet = file.Worksheet(1);

                //workSheet.Row(workSheet.RowCount() > 0 ? workSheet.RowCount() : 1).InsertRowsBelow(1);
                Dictionary<int , string> Banners = new Dictionary<int , string>();

                foreach (var item in workSheet.Rows())
                {
                    Banners.Add(item.RowNumber() , item.Cell(1).Value.GetText());
                }
                Banners.Remove(1);
                return Ok(Banners);
            }
        }
    }
}
