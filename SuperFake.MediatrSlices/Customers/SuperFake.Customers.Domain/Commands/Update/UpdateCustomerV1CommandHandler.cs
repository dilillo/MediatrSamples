using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Customers.Data;
using SuperFake.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Customers.Domain
{
    public class UpdateCustomerV1CommandHandler : IRequestHandler<UpdateCustomerV1Command>
    {
        private readonly SuperFakeCustomersDbContext _dbContext;
        private readonly IMediator _mediator;

        public UpdateCustomerV1CommandHandler(SuperFakeCustomersDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateCustomerV1Command request, CancellationToken cancellationToken)
        {
            await VerifyCustomerExists(request.Customer.ID);

            await VerifyCustomerNameIsUnique(request.Customer.ID, request.Customer.FirstName, request.Customer.LastName);

            _dbContext.Update(request.Customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await PublishCustomerUpdatedNotification(request.Customer, cancellationToken);

            return Unit.Value;
        }

        private async Task PublishCustomerUpdatedNotification(Customer customer, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new CustomerUpdatedV1Notification
            {
                FirstName = customer.FirstName,
                ID = customer.ID,
                LastName = customer.LastName

            }, cancellationToken);
        }

        private async Task VerifyCustomerNameIsUnique(int customerID, string customerFirstName, string customerLastName)
        {
            var nameExists = await _dbContext.Customers.AnyAsync(i => i.ID != customerID && i.FirstName == customerFirstName && i.LastName == customerLastName);

            if (nameExists)
                throw new UpdateCustomerNameMustBeUniqueException();
        }

        private async Task VerifyCustomerExists(int customerID)
        {
            var customerExists = await _dbContext.Customers.AnyAsync(e => e.ID == customerID);

            if (!customerExists)
                throw new UpdateCustomerDoesNotExistException();
        }
    }
}
