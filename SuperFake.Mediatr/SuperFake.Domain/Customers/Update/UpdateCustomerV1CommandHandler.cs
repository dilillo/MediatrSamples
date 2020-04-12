using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Domains
{
    public class UpdateCustomerV1CommandHandler : IRequestHandler<UpdateCustomerV1Command>
    {
        private readonly SuperFakeDbContext _dbContext;

        public UpdateCustomerV1CommandHandler(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateCustomerV1Command request, CancellationToken cancellationToken)
        {
            await VerifyCustomerExists(request.Customer.ID);

            await VerifyCustomerNameIsUnique(request.Customer.ID, request.Customer.FirstName, request.Customer.LastName);

            _dbContext.Update(request.Customer);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
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
