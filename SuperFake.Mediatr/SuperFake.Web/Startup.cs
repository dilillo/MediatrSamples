using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperFake.Data;

namespace SuperFake.Web
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
            services.AddControllersWithViews();

            services.AddDbContext<SuperFakeDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SuperFake")));

            var domainsAssembly = typeof(SuperFake.Domains.DomainException).Assembly;

            services.AddMediatR(domainsAssembly, this.GetType().Assembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Components.LoggingBehavior<,>));
            services.AddTransient<SuperFake.Domains.ICreateCustomerV1CommandHandlerData, SuperFake.Domains.CreateCustomerV1CommandHandlerData>();
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
