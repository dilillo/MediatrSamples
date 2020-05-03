using Microsoft.Extensions.Configuration;
using SuperFake.Shared.Worker;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuperFake.Customers.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
                .Build();

            //IServiceCollection services = new ServiceCollection();
            //services.AddDbContext<MyDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IMyService, MyService>();
            //IServiceProvider provider = services.BuildServiceProvider();

            //IMyService myService = provider.GetService<IMyService>();

            var httpClient = new HttpClient();

            var notificationsApiUrl = configuration["NotificationsApiUrl"];
            var serviceBusConnectionString = configuration["ServiceBusConnectionString"];

            var ordersNotificationListener = new NotificationListener(httpClient, serviceBusConnectionString, "orders", "customers-api", notificationsApiUrl);

            ordersNotificationListener.StartListening();

            Console.WriteLine("listening for notifications ... press any key to quit");

            Console.ReadKey();

            await ordersNotificationListener.StopListening();
        }
    }
}
