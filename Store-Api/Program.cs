﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using StoreApi;
using StoreApi.BLL;
using StoreApi.DAL;
using StoreApi.DAL.DB;
using StoreApi.Entity._Product;
using StoreApi.Entity._User;
using StoreApi.Models.FieldsRequest.AdminSide.ManageProduct;
using StoreApi.Models.Services.Redis;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

[EnableCors]
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<db>(options =>
        {
            options.UseSqlServer(ConStr.con);
        }, ServiceLifetime.Transient);


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

        builder.Services.AddBusinessAccessLayerServices(builder.Configuration);
        builder.Services.AddDataAccessLayerServices(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddMemoryCache();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store-Api", Version = "v1" });
                        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constansts.JWT_SECURITY_KEY_FOR_TOKEN)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
            };
        });
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin1", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    var number = context.User.FindFirst("AdminNumber");
                    return number != null && int.Parse(number.Value) < 2;
                });
            });
            options.AddPolicy("Admin2", policy =>
            {
                policy.RequireClaim("AdminNumber");
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
            });
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

        var serviceCollection = new ServiceCollection();

        string sKeysPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Keys");
        serviceCollection.AddDataProtection()
            .SetDefaultKeyLifetime(new TimeSpan(30, 0, 0, 0))
;

        //ConnectionMultiplexer.ConnectAsync("basketdb:6379");
        builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false"));
        builder.Services.AddHostedService<RedisSyncService>();
        builder.Services.AddScoped<ICacheProvider , CacheProvider>();

        var app = builder.Build();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var dbcon = scope.ServiceProvider.GetRequiredService<db>();
        //    //Same as the question
        //    dbcon.Database.Migrate();
        //}


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