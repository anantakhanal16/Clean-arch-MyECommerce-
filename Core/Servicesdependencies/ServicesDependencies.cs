using Core.Implementation;
using Core.Interface.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Servicesdependencies
{
    public static class ServicesDependencies
    {
        public static IServiceCollection CoreServices(this IServiceCollection services)
        {
           services.AddScoped<IProductService, ProductService>();
          
           services.AddScoped<IProductBrandService, ProductBrandService>();
          
           services.AddScoped<IProductTypeService, ProductTypeService>();
            
           return services;
        }
    }
}
