using MediatR;

namespace SuperFake.Shared.Domain
{
    public class CustomerDeletedV1Notification : INotification
    {
        public int ID { get; set; }
    }
}
