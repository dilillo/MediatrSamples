using MediatR;

namespace SuperFake.Shared.Domain
{
    public class OrderDeletedV1Notification : INotification
    {
        public int ID { get; set; }
    }
}
