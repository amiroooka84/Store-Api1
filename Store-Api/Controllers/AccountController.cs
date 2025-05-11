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
using StoreApi.BLL.Features.UserAddressFeature.Command.UpdateUserAddress;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using StoreApi.BLL.Features.LikeFeature.Command.NewFolder.AddLike;
using StoreApi.Entity._Like;
using StoreApi.BLL.Features.LikeFeature.Command.NewFolder.DeleteLike;


namespace StoreApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly IDataProtector _protector ;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        public AccountController(UserManager<User> userManager, SignInManager<User> SignInManager, IDataProtectionProvider provider , IMediator mediator , IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
            _mediator = mediator;
            _memoryCache = memoryCache;
            var serviceCollection = new ServiceCollection();

            string sKeysPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Keys");
            serviceCollection.AddDataProtection();
            var services = serviceCollection.BuildServiceProvider();
            var dataProtectionProvider = services.GetService<IDataProtectionProvider>();
            _protector = dataProtectionProvider.CreateProtector("MyFirstKey");
        }


        #region login
        [AllowAnonymous]
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
            _memoryCache.Remove("ConfirmCode");
            _memoryCache.Set("ConfirmCode", new ConfirmCode() { PhoneNumber = phoneNumberFieldRequest.PhoneNumber, Code = Code, ExpireTime = expireTime } , TimeSpan.FromMinutes(5));
            return Ok(new { Code });
        }


        [AllowAnonymous]
        [HttpPost(Name = "VerifiCode")]
        public async Task<IActionResult> VerifiCode(VerifiFieldRequest VerifiFieldRequest)
        {
            var ConfirmCode = _memoryCache.Get<ConfirmCode>("ConfirmCode");
            if (ConfirmCode == null)
            {
                return Ok(false);
            }
            _memoryCache.Remove("ConfirmCode");
            //var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>(_memoryCache.Get("ConfirmCode"));
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
        [HttpPost(Name = "EditProfile")]
        public async Task<IActionResult> EditProfile(EditProfileFieldRequest EditProfileFieldRequest)
        {
            User user = new User();
            user = await _userManager.FindByNameAsync(this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First());

            if (EditProfileFieldRequest.FirstName != null) { user.FirstName = EditProfileFieldRequest.FirstName; }
            if (EditProfileFieldRequest.LastName != null) { user.LastName = EditProfileFieldRequest.LastName; }
            if (EditProfileFieldRequest.Email != null) { user.Email = EditProfileFieldRequest.Email; user.EmailConfirmed = false; }
            if (EditProfileFieldRequest.ImagePath != null) { user.ImagePath = EditProfileFieldRequest.ImagePath; }
            var result = _userManager.UpdateAsync(user).Result.Succeeded;
            return Ok(result);
        }


        [HttpGet(Name = "GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            List<Address> Address = _mediator.Send(new GetUserAddressesQuery() { UserId = user.Id }).Result.ToList();
            return Ok(new { user, Address });
        }


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


        [HttpDelete(Name = "DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(IntIdField id)
        {
            bool res = await _mediator.Send(new DeleteUserAddressCommand() { id = id.id }) == null ? false : true;
            return Ok(res);
        }

        [HttpPut(Name = "EditAddress")]
        public async Task<IActionResult> EditAddress(EditAddressFieldRequest editAddressField)
        {
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            Address address = new Address()
            {
                id = editAddressField.id,
                PostCode = editAddressField.PostCode,
                UserId = user.Id,
                _Address = editAddressField.Address
            }; 
            Address res = await _mediator.Send(new UpdateUserAddressCommand() { Address = address });
            return Ok(res);
        }

        [HttpPut(Name = "VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailFieldRequest email)
        {
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            if (email.Email == user.Email)
            {
                user.EmailConfirmed = true;
                var res = _userManager.UpdateAsync(user).Result.Succeeded;
                return Ok(res);
            }
            return BadRequest();
        }



        [HttpPost(Name = "AddLike")]
        public async Task<IActionResult> AddLike(IntIdField productId)
        {
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            Like like = new Like()
            {
                UserId = user.Id,
                ProductId = productId.id,
            };
            bool res =  _mediator.Send(new AddLikeCommand() { Like = like}).IsCompletedSuccessfully;

            return Ok(res);
        }

        [HttpPost(Name = "DeleteLike")]
        public async Task<IActionResult> DeleteLike(IntIdField productId)
        {
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            Like like = new Like()
            {
                UserId = user.Id,
                ProductId = productId.id,
            };
            bool res = _mediator.Send(new DeleteLikeCommand() { Like = like }).IsCompletedSuccessfully;

            return Ok(res);
        }
        #endregion
    }
}
