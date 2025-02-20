using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreApi.DAL.DB;
using StoreApi.DAL.Repository.CategoryRepository;
using StoreApi.DAL.Repository.ImagePathRepository;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.DAL.Repository.ProductColorsRepository;
using StoreApi.DAL.Repository.ProductTagRepository;
using StoreApi.DAL.Repository.RepositoryBase;
using StoreApi.DAL.Repository.UserAddressRepository;


namespace StoreApi.DAL
{
    public static class DALServiecRegistration
    {
        public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<db>(options =>
            {
                options.UseSqlServer(ConStr.con);
            });

            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductColorsRepository, ProductColorsRepository>();
            services.AddScoped<IProductTagRepository, ProductTagRepository>();
            services.AddScoped<IImagePathRepository,ImagePathRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IUserAddressRepository,UserAddressRepository>();

            return services;
        }
    }
}
