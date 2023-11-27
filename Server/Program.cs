using EndavaTechCourse.BankApp.Infrastructure;
using EndavaTechCourse.BankApp.Application.Queries.GetWallets;
using EndavaTechCourse.BankApp.Shared;
using EndavaTechCourse.BankApp.Server.Composition;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Server.Common.JWTToken;

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
        builder.Services.AddInfrastructure(configuration);
        builder.Services.AddJwtIdentity(configuration);
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        builder.Services.AddSession();
        builder.Services.AddScoped<CurrencyConverter>();
        builder.Services.AddScoped<WalletCodeGenerator>();
        builder.Services.AddScoped<IJwtService, JwtService>();


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

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}

