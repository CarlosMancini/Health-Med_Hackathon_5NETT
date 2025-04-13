using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Database.Repositories;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Extensions
{
    public static class DependencyInjection
    {
        public static void Inject(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
            }, ServiceLifetime.Scoped);

            // Repositories
            serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //Services
            serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
            serviceCollection.AddScoped<ICriptografiaService, CriptografiaService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
        }
    }
}
