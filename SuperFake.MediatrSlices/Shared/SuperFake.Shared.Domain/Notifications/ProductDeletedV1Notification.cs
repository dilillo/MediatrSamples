using MediatR;

namespace SuperFake.Shared.Domain
{
    public class ProductDeletedV1Notification : INotification
    {
        public int ID { get; set; }
    }
}
