using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using StoreApi.Entity._User;
using StoreApi.Models.Services.SMS;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection;
using StoreApi.Models.FieldsRequest.AccountField;
using Store_Api.Models.Classes.Account;
using System.Reflection;
using Store_Api.Models.FieldsRequest.AccountField;
using System.Security.Claims;
using StoreApi.Entity._Address;
using MediatR;
using StoreApi.BLL.Features.UserAddressFeature.Query.GetUserAddresses;
using StoreApi.BLL.Features.UserAddressFeature.Command.AddUserAddress;
using StoreApi.BLL.Features.UserAddressFeature.Command.DeleteUserAddress;
using StoreApi.Models.FieldsRequest.IDField;
using StoreApi.Entity._Image;


namespace StoreApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]


    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly IDataProtector _protector ;
        private readonly IMediator _mediator;

        public AccountController(UserManager<User> userManager, SignInManager<User> SignInManager, IDataProtectionProvider provider , IMediator mediator)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
            _mediator = mediator;
            var serviceCollection = new ServiceCollection();

            string sKeysPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Keys");
            serviceCollection.AddDataProtection();
            var services = serviceCollection.BuildServiceProvider();
            var dataProtectionProvider = services.GetService<IDataProtectionProvider>();
            _protector = dataProtectionProvider.CreateProtector("MyFirstKey");
        }


        #region login
        [HttpPost(Name = "PhoneNumber")]
        public IActionResult PhoneNumber(PhoneNumberFieldRequest phoneNumberFieldRequest)
        {
            if (phoneNumberFieldRequest.PhoneNumber?.Length != 11 || !phoneNumberFieldRequest.PhoneNumber.StartsWith("09"))
            {
                return Ok(false);
            }
            SMS sms = new SMS();
            Random random = new Random();
            string Code = random.Next(1000, 9999).ToString();
            sms.SendConfirmCode(Code, phoneNumberFieldRequest.PhoneNumber);
            DateTime expireTime = DateTime.Now.AddMinutes(5);
            string Serialize = JsonConvert.SerializeObject(new ConfirmCode() { PhoneNumber = phoneNumberFieldRequest.PhoneNumber, Code = Code, ExpireTime = expireTime });
            var hashCode = _protector.Protect(Serialize);
            return Ok(new { hashCode, Code });
        }


        [HttpPost(Name = "VerifiCode")]
        public async Task<IActionResult> VerifiCode(VerifiFieldRequest VerifiFieldRequest)
        {

            var Encrypt = "";

            Encrypt = _protector.Unprotect(VerifiFieldRequest.ConfirmCode);

            var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>(Encrypt);
            if (ConfirmCode.PhoneNumber == VerifiFieldRequest.PhoneNumber && ConfirmCode.Code == VerifiFieldRequest.Code && ConfirmCode.ExpireTime > DateTime.Now)
            {

                if (VerifiFieldRequest.PhoneNumber?.Length != 11 || !VerifiFieldRequest.PhoneNumber.StartsWith("09"))
                {
                    return Ok(false);
                }
                if (_userManager.FindByNameAsync(VerifiFieldRequest.PhoneNumber).Result != null)
                {
                    JWTAuthorizeManage jWTAuthorizeManage = new JWTAuthorizeManage(_userManager);
                    var Result = await jWTAuthorizeManage.AuthenticateAsync(VerifiFieldRequest.PhoneNumber);
                    if (Result == null)
                        return Ok(false);
                    else
                        return Ok(Result);
                }
                else
                {
                    var res = await _userManager.CreateAsync(new User
                    {
                        UserName = VerifiFieldRequest.PhoneNumber,
                        PhoneNumber = VerifiFieldRequest.PhoneNumber,
                        PhoneNumberConfirmed = true,
                    });

                    if (VerifiFieldRequest.PhoneNumber == "09224982760" || VerifiFieldRequest.PhoneNumber == "09187012481" || VerifiFieldRequest.PhoneNumber == "09187012481")
                    {
                        User user = new User();
                        user = await _userManager.FindByNameAsync(VerifiFieldRequest.PhoneNumber);
                        await _userManager.AddClaimAsync(user, new Claim("AdminNumber", "1"));
                    }
                    else
                    {
                        User user = new User();
                        user = await _userManager.FindByNameAsync(VerifiFieldRequest.PhoneNumber);
                        await _userManager.AddClaimAsync(user, new Claim("UserNumber", "1"));
                    }
                    if (res.Succeeded)
                    {
                        JWTAuthorizeManage jWTAuthorizeManage = new JWTAuthorizeManage(_userManager);
                        var Result = jWTAuthorizeManage.AuthenticateAsync(VerifiFieldRequest.PhoneNumber);
                        if (Result == null)
                            return Ok(false);
                        else
                            return Ok(Result);
                    }

                    return Ok(false);
                }
            }
            else
            {
                return Ok(false);
            }
        }
        #endregion

        #region Profile
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(Name = "EditProfile")]
        public async Task<IActionResult> EditProfile(EditProfileFieldRequest EditProfileFieldRequest)
        {
            User user = new User();
            user = await _userManager.FindByNameAsync(this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First());
            user.FirstName = EditProfileFieldRequest.FirstName;
            user.LastName = EditProfileFieldRequest.LastName;
            user.Email = EditProfileFieldRequest.Email;

            var result = _userManager.UpdateAsync(user).Result.Succeeded;
            return Ok(result);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet(Name = "GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            List<Address> Address = _mediator.Send(new GetUserAddressesQuery() { UserId = user.Id }).Result.ToList();
            return Ok(new { user, Address });
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(Name = "AddAddress")]
        public async Task<IActionResult> AddAddress(AddAddressFieldRequest AddAddressFieldRequest)
        {
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            Address address = new Address()
            {
                _Address = AddAddressFieldRequest.Address,
                PostCode = AddAddressFieldRequest.PostCode,
                UserId = user.Id
            };
            Address res = await _mediator.Send(new AddUserAddressCommand() { Address = address});
            return Ok(res);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete(Name = "DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(IntIdField id)
        {
            bool res = await _mediator.Send(new DeleteUserAddressCommand() { id = id.id }) == null ? false : true;
            return Ok(res);
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete(Name = "AddUserImage")]
        public async Task<IActionResult> AddUserImage(string imagePath)
        {
            User user = await _userManager.FindByNameAsync(this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First());
            user.ImagePath = imagePath;
            var res = _userManager.UpdateAsync(user).Result.Succeeded;
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete(Name = "RemoveUserImage")]
        public async Task<IActionResult> RemoveUserImage()
        {
            User user = await _userManager.FindByNameAsync(this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First());
            user.ImagePath = null;
            var res = _userManager.UpdateAsync(user).Result.Succeeded;
            return Ok(res);
        }
        #endregion
    }
}
