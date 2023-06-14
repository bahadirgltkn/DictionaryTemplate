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
            return services;
        }
    }
}
