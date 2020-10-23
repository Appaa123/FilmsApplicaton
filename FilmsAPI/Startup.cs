using FilmsApi.Model;
using FilmsApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FilmsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            // requires using Microsoft.Extensions.Options
            services.Configure<FilmsDatabaseSettings>(
                Configuration.GetSection(nameof(FilmsDatabaseSettings)));

            services.AddSingleton<IFilmsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<FilmsDatabaseSettings>>().Value);

            services.AddSingleton<FilmService>();

            services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());
            
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Films}/{action=Index}/{id?}");
            });


        }
    }
}
