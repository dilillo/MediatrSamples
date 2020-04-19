using MediatR;
using SuperFake.Customers.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Customers.Domain
{
    public class CreateCustomerV1CommandHandler : IRequestHandler<CreateCustomerV1Command>
    {
        private readonly ICreateCustomerV1CommandHandlerData _data;
        private readonly IMediator _mediator;

        public CreateCustomerV1CommandHandler(ICreateCustomerV1CommandHandlerData data, IMediator mediator)
        {
            _data = data;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateCustomerV1Command request, CancellationToken cancellationToken)
        {
            await VerifyCustomerNameIsUnique(request.Customer.FirstName, request.Customer.LastName, cancellationToken);

            await CreateCustomer(request.Customer, cancellationToken);

            await PublishCustomerCreatedNotification(request.Customer, cancellationToken);

            return Unit.Value;
        }

        private async Task CreateCustomer(Customer customer, CancellationToken cancellationToken)
        {
            _data.AddCustomer(customer);

            await _data.SaveChanges(cancellationToken);
        }

        private async Task PublishCustomerCreatedNotification(Customer customer, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new CustomerCreatedV1Notification
            {
                FirstName = customer.FirstName,
                ID = customer.ID,
                LastName = customer.LastName

            }, cancellationToken);
        }

        private async Task VerifyCustomerNameIsUnique(string customerFirstName, string customerLastName, CancellationToken cancellationToken)
        {
            var nameExists = await _data.CustomerNameExists(customerFirstName, customerLastName, cancellationToken);

            if (nameExists)
                throw new CreateCustomerNameMustBeUniqueException();
        }
    }
}
