using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Admin;
using StoreApi.Entity._User;

namespace StoreApi.Controllers.AdminSide
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ManageUsersController : ControllerBase
    {
        [HttpPost(Name = "GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            bl_ManageUsers dl_ManageUsers = new bl_ManageUsers();
            List<User> res = dl_ManageUsers.GetAllUsers();
            return Ok(res);
        }
    }
}
