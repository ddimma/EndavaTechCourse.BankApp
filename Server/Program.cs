using EndavaTechCourse.BankApp.Infrastructure;
using EndavaTechCourse.BankApp.Application.Queries.GetWallets;
using EndavaTechCourse.BankApp.Shared;
using EndavaTechCourse.BankApp.Server.Composition;

namespace EndavaTechCourse.BankApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        // Add services to the container.

        // Add mediatoR
        builder.Services.AddMediatR(config => {
            config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            config.RegisterServicesFromAssemblies(typeof(GetWalletsQuery).Assembly);
        });
        builder.Services.AddJwtIdentity(configuration);
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.AddInfrastructure(configuration);
        builder.Services.AddScoped<CurrencyConverter>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();


        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}

