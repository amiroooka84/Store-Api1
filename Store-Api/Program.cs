using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StoreApi;
using StoreApi.DAL.DB;
using StoreApi.Entity._User;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

[EnableCors]
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //شناختن دیتا بیس به پروژه
        builder.Services.AddDbContext<db>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("con")));



        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("https://localhost:3000",
                                                      "http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                              });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store-Api", Version = "v1" });
                        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constansts.JWT_SECURITY_KEY_FOR_TOKEN)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddIdentity<User, IdentityRole>(option =>
        {
            option.Password.RequireDigit = false;
            option.Password.RequireLowercase = false;
            option.Password.RequireUppercase = false;
            option.Password.RequireNonAlphanumeric = false;
            option.Password.RequiredLength = 6;
            option.SignIn.RequireConfirmedPhoneNumber = false;
        }).AddUserManager<UserManager<User>>()
        //.AddEntityFrameworkStores<IdentityDbContext>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<db>();


        string sKeysPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Keys");
        builder.Services.AddDataProtection().
            DisableAutomaticKeyGeneration().
            SetDefaultKeyLifetime(new TimeSpan(30, 0, 0, 0));
    //        .SetApplicationName("StoreApi") // Application A sets this same name
    //.PersistKeysToFileSystem(new DirectoryInfo("/Keys"))
    //.DisableAutomaticKeyGeneration();



        var app = builder.Build();
        //if (app.Environment.IsDevelopment())
        //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store-Api"); });
        //}

        app.UseCors(MyAllowSpecificOrigins);
        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseRouting();
      
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}