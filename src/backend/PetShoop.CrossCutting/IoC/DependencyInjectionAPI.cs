using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Services;
using PetShoop.Domain.Interfaces;
using PetShoop.Infrastructure.Context;
using PetShoop.Infrastructure.Repositories;
using PetShoop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using PetShoop.Infrastructure.Identity.Interfaces;
using PetShoop.Infrastructure.Identity.Services;
using PetShoop.Infrastructure.HealthChecks;

namespace PetShoop.CrossCutting.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
    {
        //Usando em Memomy
        //services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DataBase"));


        //Usando SQL Server
        services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(
                         // String de conexão vinda do appsettings.json
                         configuration.GetConnectionString("DefaultConnection"),

                         // Define onde ficarão as migrations
                         b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                     )
                 );


        //cliente
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteService, ClienteService>();

        //pet
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IPetService, PetService>();

        //agendamento
        services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
        services.AddScoped<IAgendamentoService, AgendamentoService>();

        //vacina
        services.AddScoped<IVacinaRepository, VacinaRepository>();
        services.AddScoped<IVacinaService, VacinaService>();

        //Consulta
        services.AddScoped<IConsultaRepository, ConsultaRepository>();
        services.AddScoped<IConsultaService, ConsultaService>();


        //Funcionario
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<IFuncionarioService, FuncionarioService>();

        //ItemVenda
        services.AddScoped<IItemVendaRepository, ItemVendaRepository>();
        services.AddScoped<IItemVendaService, ItemVendaService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Prontuario
        services.AddScoped<IProntuarioRepository, ProntuarioRepository>();
        services.AddScoped<IProntuarioService, ProntuarioService>();

        //Venda
        services.AddScoped<IVendaRepository, VendaRepository>();
        services.AddScoped<IVendaService, VendaService>();

        //Servico
        services.AddScoped<IServicoRepository, ServicoRepository>();
        services.AddScoped<IServicoService, ServicoService>();

        //Produto
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();

        // ===============================
        // CONFIGURAÇÃO DO ASP.NET IDENTITY
        // ===============================
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        // ===============================
        // SERVIÇO DE AUTENTICAÇÃO
        // ===============================
        services.AddScoped<IAuthenticate, AuthenticateService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("database");

        return services;
    }
}
