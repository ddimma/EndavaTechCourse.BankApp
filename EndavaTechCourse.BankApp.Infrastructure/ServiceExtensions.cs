using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;

namespace EndavaTechCourse.BankApp.Infrastructure;

public static class ServicesExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], x => x.MigrationsAssembly("EndavaTechCourse.BankApp.Infrastructure")));

        return services;
    }
}