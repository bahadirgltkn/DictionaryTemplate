using DictionaryTemplate.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DictionaryTemplate.Infrastructure.Persistance.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<DictionaryTemplateContext>(conf =>
            {
                var conn = configuration["DictionaryTemplateDbConnectionString"].ToString();

                conf.UseSqlServer(conn, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });

            // if you want your tables to be full at startup, you can run it once.

            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            return services;
        }
    }
}
