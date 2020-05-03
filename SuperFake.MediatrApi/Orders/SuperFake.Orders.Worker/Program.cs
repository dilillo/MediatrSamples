using Microsoft.Extensions.Configuration;
using SuperFake.Shared.Worker;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuperFake.Orders.Worker
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

            var productsNotificationListener = new NotificationListener(httpClient, serviceBusConnectionString, "products", "orders-api", notificationsApiUrl);
            var customersNotificationListener = new NotificationListener(httpClient, serviceBusConnectionString, "customers", "orders-api", notificationsApiUrl);

            productsNotificationListener.StartListening();
            customersNotificationListener.StartListening();

            Console.WriteLine("listening for notifications ... press any key to quit");

            Console.ReadKey();

            await productsNotificationListener.StopListening();
            await customersNotificationListener.StopListening();
        }
    }
}
