using Microsoft.Extensions.Configuration;
using SuperFake.Shared.Worker;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuperFake.Web.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
                .Build();

            var httpClient = new HttpClient();

            var notificationsApiUrl = configuration["NotificationsApiUrl"];
            var serviceBusConnectionString = configuration["ServiceBusConnectionString"];

            var productsNotificationListener = new NotificationListener(httpClient, serviceBusConnectionString, "products", "web-api", notificationsApiUrl);
            var customersNotificationListener = new NotificationListener(httpClient, serviceBusConnectionString, "customers", "web-api", notificationsApiUrl);
            var ordersNotificationListener = new NotificationListener(httpClient, serviceBusConnectionString, "orders", "web-api", notificationsApiUrl);

            productsNotificationListener.StartListening();
            customersNotificationListener.StartListening();
            ordersNotificationListener.StartListening();

            Console.WriteLine("listening for notifications ... press any key to quit");

            Console.ReadKey();

            await productsNotificationListener.StopListening();
            await customersNotificationListener.StopListening();
            await ordersNotificationListener.StopListening();
        }
    }
}
