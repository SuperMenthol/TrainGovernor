using Application.Controllers;
using Infrastructure.Interfaces.Controllers;
using Domain.Context.TrainGovernorContext;
using Domain.Interfaces.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using System.Reflection;
using Domain.Context.UserAuthContext;

namespace TrainGovernor
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", false)
            .Build();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ITrainGovernorContext, TrainGovernorContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("TrainGovernorConnection")));
            builder.Services.AddDbContext<IUserAuthContext, UserAuthContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("UserAuthConnection")));

            builder.Services
                .AddMvc()
                .AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/Login", ""))
                .AddApplicationPart(Assembly.Load("Application"))
                .AddControllersAsServices();

            builder.Services.AddScoped<ITrainGovernorContext, TrainGovernorContext>();
            builder.Services.AddScoped<IUserAuthContext, UserAuthContext>();
            builder.Services.AddScoped<ILoginController, LoginController>();
            builder.Services.AddScoped<ITrainStationController, TrainStationController>();
            builder.Services.AddScoped<ICityController, CityController>();
            builder.Services.AddScoped<ILineController, LineController>();
            builder.Services.AddScoped<ILineStartTimeController, LineStartTimeController>();

            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Host.UseNLog();

            builder.Services.AddAutoMapper(Assembly.Load("Application"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapControllers();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}