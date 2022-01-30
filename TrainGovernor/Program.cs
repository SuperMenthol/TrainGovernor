using Application.Controllers;
using Domain.Interfaces.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Interfaces.Context;
using Infrastructure.Context.TrainGovernorContext;
using System.Reflection;

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

            builder.Services
                .AddMvc()
                .AddApplicationPart(Assembly.Load("Application"))
                .AddControllersAsServices();

            builder.Services.AddScoped<ITrainGovernorContext, TrainGovernorContext>();
            builder.Services.AddScoped<ITrainStationController, TrainStationController>();
            builder.Services.AddScoped<ICityController, CityController>();

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

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}