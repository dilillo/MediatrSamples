using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperFake.Customers.Data;
using SuperFake.Customers.Domain;
using SuperFake.Orders.Data;
using SuperFake.Orders.Domain;
using SuperFake.Products.Data;
using SuperFake.Products.Domain;
using SuperFake.Shared.Domain;
using SuperFake.Web.Data;
using SuperFake.Web.Domain;

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

            services.AddDbContext<SuperFakeCustomersDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SuperFakeCustomers")));

            services.AddDbContext<SuperFakeOrdersDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SuperFakeOrders")));

            services.AddDbContext<SuperFakeProductsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SuperFakeProducts")));

            services.AddDbContext<SuperFakeWebDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SuperFakeWeb")));

            var sharedAssembly = typeof(ProductCreatedV1Notification).Assembly;
            var customerAssembly = typeof(CreateCustomerV1Command).Assembly;
            var productAssembly = typeof(CreateProductV1Command).Assembly;
            var orderAssembly = typeof(CreateOrderV1Command).Assembly;
            var WebAssembly = typeof(GetAllProductsV1Query).Assembly;

            services.AddMediatR(sharedAssembly, customerAssembly, productAssembly, orderAssembly, WebAssembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Components.LoggingBehavior<,>));
            services.AddTransient<ICreateCustomerV1CommandHandlerData, CreateCustomerV1CommandHandlerData>();
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
