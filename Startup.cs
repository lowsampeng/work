using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Singapore.Data;
using Singapore.WindDirection;
using Singapore.AirTemperature;
using Singapore.PSI;
using Singapore.RelativeHumidity;
using Singapore.WindSpeed;
using Singapore.TwoHourWeatherForecast;
using Singapore.TwentyFourHourWeatherForecast;
using Singapore.FourDayWeatherForecast;

namespace Singapore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<SingaporeWindDirectionService>();
            services.AddSingleton<SingaporeAirTemperatureService>();
            services.AddSingleton<SingaporePSIService>();
            services.AddSingleton<SingaporeRelativeHumidityService>();
            services.AddSingleton<SingaporeWindSpeedService>();
            services.AddSingleton<SingaporeTwoHourWeatherForecastService>();
            services.AddSingleton<SingaporeTwentyFourHourWeatherForecastService>();
            services.AddSingleton<SingaporeFourDayWeatherForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
