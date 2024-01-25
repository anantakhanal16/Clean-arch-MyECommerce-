using Core.Entites.Identity;
using Core.Interface.Repositories;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
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

            services.AddScoped<ICheckoutRepository, CheckOutRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IAuthenticationService, UserAuthentication>();

            services.AddDbContext<AppIdentityDbcontext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });


            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters= "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

        
            })

           .AddEntityFrameworkStores<AppIdentityDbcontext>()
           
           .AddSignInManager<SignInManager<AppUser>>()
           
           .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(op =>op.LoginPath = "/Account/Login");

            return services;
        }
    }
}
