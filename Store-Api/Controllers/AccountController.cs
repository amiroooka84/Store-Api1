﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using StoreApi.Entity._User;
using StoreApi.BLL;
using StoreApi.BLL.Account;
using StoreApi.Models.Services.SMS;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using StoreApi.Models.FieldsRequest.AccountField;
using Store_Api.Models.Classes.Account;
using StoreApi.Models;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Store_Api.Models.FieldsRequest.AccountField;
using StoreApi.DAL.dl_Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authentication;
using StoreApi.Entity._Address;


namespace StoreApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]


    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly IDataProtector _protector;

        public AccountController(UserManager<User> userManager, SignInManager<User> SignInManager, IDataProtectionProvider provider)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
            //_protector = provider.CreateProtector("AccountController", new string[] { "Account" });

            var serviceCollection = new ServiceCollection();

            string sKeysPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Keys");
            serviceCollection.AddDataProtection();
            var services = serviceCollection.BuildServiceProvider();
            var dataProtectionProvider = services.GetService<IDataProtectionProvider>();
            _protector = dataProtectionProvider.CreateProtector("MyFirstKey");
        }


        //[HttpPost(Name = "ExsitUser")]

        //public bool ExsitUser(PhoneNumberFieldRequest phoneNumberFieldRequest)
        //{
        //    bl_Account bl_Account = new bl_Account();
        //    if (bl_Account.ExsitUser(phoneNumberFieldRequest.PhoneNumber))
        //    {
        //        return true;
        //    }
        //    return false;
        //}


        //[HttpPost(Name = "Login")]
        //public JWT_Fields Login(LoginFieldRequest loginFieldRequest)
        //{
        //    bl_Account bl_Account = new bl_Account();
        //    if (bl_Account.UserVerification(loginFieldRequest.PhoneNumber, loginFieldRequest.Password))
        //    {

        //        JWTAuthorizeManage jWTAuthorizeManage = new JWTAuthorizeManage();
        //        var Result = jWTAuthorizeManage.Authenticate(loginFieldRequest.PhoneNumber, loginFieldRequest.Password);
        //        if (Result == null)
        //            return null;
        //        else
        //            return Result;
        //    }
        //    return null;
        //}

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
            //var RetCode = dataProtector.Protect(Serialize);
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
                bl_Account bl_Account = new bl_Account();
                if (bl_Account.ExsitUser(VerifiFieldRequest.PhoneNumber))
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
        public IActionResult EditProfile(EditProfileFieldRequest EditProfileFieldRequest)
        {
            bl_Account bl_Account = new bl_Account();
            User user = new User()
            {
                FirstName = EditProfileFieldRequest.FirstName,
                LastName = EditProfileFieldRequest.LastName,
                PhoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First()
            };
            bool res = bl_Account.EditProfile(user);
            return Ok(res);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet(Name = "GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            bl_Account bl_Account = new bl_Account();
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            List<Address> Addresses = bl_Account.GetAddresses(user.Id);
            return Ok(new { user, Addresses });
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(Name = "AddAddress")]
        public async Task<IActionResult> AddAddress(AddAddressFieldRequest AddAddressFieldRequest)
        {
            bl_Account bl_Account = new bl_Account();
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            Address address = new Address()
            {
                _Address = AddAddressFieldRequest.Address,
                PostCode = AddAddressFieldRequest.PostCode,
                UserId = user.Id
            };
            Address res = bl_Account.AddAddress(address);
            return Ok(res);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete(Name = "DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            bl_Account bl_Account = new bl_Account();
            string phoneNumber = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value).Values.First();
            User user = await _userManager.FindByNameAsync(phoneNumber);
            Address address = new Address()
            {
                id = id,
                UserId = user.Id
            };
            bool res = bl_Account.DeleteAddress(address);
            return Ok(res);
        }
        #endregion








        //[HttpPost(Name = "ForgotPassword")]
        //public IActionResult ForgotPassword(PhoneNumberFieldRequest phoneNumberFieldRequest)
        //{
        //    if (_userManager.FindByNameAsync(phoneNumberFieldRequest.PhoneNumber) != null)
        //    {
        //        SMS sms = new SMS();
        //        Random random = new Random();
        //        string Code = random.Next(1000, 9999).ToString();
        //        sms.SendConfirmCode(Code, phoneNumberFieldRequest.PhoneNumber);
        //        DateTime expireTime = DateTime.Now.AddMinutes(5);
        //        string Serialize = JsonConvert.SerializeObject(new ConfirmCode() { Code = Code, ExpireTime = expireTime });
        //        var RetCode = _protector.Protect(Serialize);
        //        return Ok(RetCode);
        //    }
        //    return Ok();
        //}

        //[HttpPost(Name = "ChangePassword")]
        //public IActionResult ChangePassword(ForgotPasswordFieldRequest forgotPasswordFieldRequest)
        //{
        //    var Encrypt = _protector.Unprotect(forgotPasswordFieldRequest.ConfirmCode);
        //    var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>(Encrypt);
        //    if (ConfirmCode.Code == forgotPasswordFieldRequest.Code && ConfirmCode.ExpireTime > DateTime.Now)
        //    {
        //        var user = _userManager.FindByNameAsync(forgotPasswordFieldRequest.PhoneNumber);
        //        _userManager.ChangePasswordAsync(user.Result , user.Result.Password , forgotPasswordFieldRequest.Password);
        //        return Ok(true);
        //    }
        //    return Ok(false);
        //}
    }
}
