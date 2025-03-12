using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreApi.BLL.Features.ProductFeature;
using StoreApi.DAL;
using StoreApi.DAL.Repository.CategoryRepository;
using StoreApi.DAL.Repository.ImagePathRepository;
using StoreApi.DAL.Repository.ManagementRepository;
using StoreApi.DAL.Repository.ProductColorsRepository;
using StoreApi.DAL.Repository.ProductTagRepository;
using StoreApi.DAL.Repository.RepositoryBase;
using System.Reflection;

namespace StoreApi.BLL
{
    public static class BLLServiceRegistration
    {
        public static IServiceCollection AddBusinessAccessLayerServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
