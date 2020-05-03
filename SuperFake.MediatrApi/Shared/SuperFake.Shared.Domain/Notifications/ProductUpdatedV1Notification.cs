using MediatR;

namespace SuperFake.Shared.Domain
{
    public class ProductUpdatedV1Notification : INotification
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
