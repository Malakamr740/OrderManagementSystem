using Microsoft.Extensions.DependencyInjection;
using OrderBase.Interfaces;
using OrderBase.Implementations;
using OrderCore.Interfaces;
using OrderCore.Implementations;
using RunTime.Validations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using OrderBase.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace RunTime.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void Configure(this IServiceCollection services, IConfiguration configuration)
        {
            #region MyRegion
                services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            #endregion;

            #region Services

                 #region InfraStructureConfiguration
                    services.AddScoped<IProductRepository, ProductEFRepository>();    
                    services.AddScoped<ICustomerRepository, CustomerEFRepository>();
                    services.AddScoped<IOrderRepository, OrderEFRepository>();
                    services.AddScoped<ISeeder, CustomerSeeder>();
                 #endregion



                 #region BusinessLayerConfiguration
                    services.AddScoped<IProductService, ProductService>();
                    services.AddScoped<ICustomerService, CustomerService>();
                    services.AddScoped<IOrderService, OrderService>();
            #endregion

            #region PasswordHasher
            services.AddScoped<IHasher, PasswordHasher>();
            #endregion


            #region UnitOfWorkConfiguration
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                 #endregion  
            
            #endregion



            #region Validation
                services.AddValidatorsFromAssemblyContaining<ProductValidator>();
                services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
                services.AddValidatorsFromAssemblyContaining<OrderValidator>();
            #endregion

        }

        public static void AppConfiguration(IApplicationBuilder applicationBuilder) {

            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
                seeder.Seed();
            }
        }
    }
}