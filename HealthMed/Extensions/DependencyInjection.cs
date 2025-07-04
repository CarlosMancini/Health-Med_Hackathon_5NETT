﻿using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Database.Repositories;
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
            serviceCollection.AddScoped<IMedicoRepository, MedicoRepository>();
            serviceCollection.AddScoped<IPacienteRepository, PacienteRepository>();
            serviceCollection.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

            //Services
            serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
            serviceCollection.AddScoped<IMedicoService, MedicoService>();
            serviceCollection.AddScoped<IPacienteService, PacienteService>();
            serviceCollection.AddScoped<ICriptografiaService, CriptografiaService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IAgendamentoService, AgendamentoService>();
        }
    }
}
