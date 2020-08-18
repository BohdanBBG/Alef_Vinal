using Alef_Vinal.Contexts;
using Alef_Vinal.Models;
using Alef_Vinal.Models.ModelValidation;
using Alef_Vinal.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Alef_Vinal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = Configuration.Get<ApplicationSettings>();
            services.Configure<ApplicationSettings>(Configuration);

            services.AddDbContext<CodeEntityContext>(options =>
            options.UseSqlServer(config.ConnectionStrings.MainDb));

            services.AddTransient<IDataRepository, DataRepository>();

            services.AddMvc()
               .AddFluentValidation(options =>
               {
                   options.RegisterValidatorsFromAssemblyContaining<Startup>(); // register all validators in assembly
                   options.RunDefaultMvcValidationAfterFluentValidationExecutes = true; // allow default validation to run
               });

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alef_Vinal V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
