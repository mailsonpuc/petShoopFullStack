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

namespace PetShoop.CrossCutting.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DataBase"));


        //cliente
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteService, ClienteService>();

        //pet
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IPetService, PetService>();

        //agendamento
        services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
        services.AddScoped<IAgendamentoService, AgendamentoService>();

        /*
        services.AddScoped<IConsultaRepository, ConsultaRepository>();
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<IItemVendaRepository, ItemVendaRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProntuarioRepository, ProntuarioRepository>();
        services.AddScoped<IServicoRepository, ServicoRepository>();
        services.AddScoped<IVacinaRepository, VacinaRepository>();
        services.AddScoped<IVendaRepository, VendaRepository>();
        */

        // ===============================
        // CONFIGURAÇÃO DO ASP.NET IDENTITY
        // ===============================
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // ===============================
        // SERVIÇO DE AUTENTICAÇÃO
        // ===============================
        services.AddScoped<IAuthenticate, AuthenticateService>();


        return services;
    }
}
