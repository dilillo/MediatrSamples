using MediatR;

namespace SuperFake.Shared.Domain
{
    public class CustomerUpdatedV1Notification : INotification
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
