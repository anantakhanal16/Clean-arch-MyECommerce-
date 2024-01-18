using Core.Entites.Identity;
using Core.Interface.Repositories;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Infra.Dependencies
{
    public static class InfraDependency
    {
        public static IServiceCollection InfraServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IImageUploadRepository, ImageUploadRepository>();

            services.AddScoped<ICartRepository, CartRepository>();

            services.AddDbContext<AppIdentityDbcontext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

           
            services.AddIdentity<AppUser, IdentityRole>()
           
           .AddEntityFrameworkStores<ApplicationDbContext>()
           
           .AddSignInManager<SignInManager<AppUser>>()
           
           .AddDefaultTokenProviders();
            
            services.AddAuthentication();

            return services;
        }
    }
}
