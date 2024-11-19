using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApi.BLL.Admin;
using StoreApi.Entity._User;
using StoreApi.Models.FieldsRequest.IDField;
using System.Reflection;
using System.Security.Claims;

namespace StoreApi.Controllers.AdminSide
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ManageUsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public ManageUsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet(Name = "GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            List<User> users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpDelete(Name = "DeleteUser")]
        public IActionResult DeleteUser(StringIdField id)
        {
            bl_ManageUsers dl_ManageUsers = new bl_ManageUsers();
            bool res = dl_ManageUsers.DeleteUser(id.id);
            return Ok(res);
        }

        [HttpPost(Name = "LevelUpUser")]
        public async Task<IActionResult> LevelUpUser(StringIdField id)
        {
            User user = new User();
            user = await _userManager.FindByIdAsync(id.id);
            await _userManager.RemoveClaimsAsync(user, new List<Claim>() { new Claim("AdminNumber", "1"), new Claim("AdminNumber", "2"), new Claim("UserNumber", "1") });
            await _userManager.AddClaimAsync(user, new Claim("AdminNumber", "2"));
            return Ok(user);
        }

        [HttpPost(Name = "LevelDownUser")]
        public async Task<IActionResult> LevelDownUser(StringIdField id)
        {
            
            User user = new User();
            user = await _userManager.FindByIdAsync(id.id);
            await _userManager.RemoveClaimsAsync(user, new List<Claim>() { new Claim("AdminNumber" , "1"), new Claim("AdminNumber", "2") , new Claim("UserNumber", "1") });
            await _userManager.AddClaimAsync(user, new Claim("UserNumber", "1"));
            return Ok(user);
        }
    }
}
