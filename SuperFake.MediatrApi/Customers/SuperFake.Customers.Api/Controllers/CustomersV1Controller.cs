using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Customers.Data;
using SuperFake.Customers.Domain;
using System.Threading.Tasks;

namespace SuperFake.Customers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersV1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _mediator.Send(new GetCustomerDetailsV1Query { CustomerID = id });

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.ID)
            {
                return BadRequest();
            }

            await _mediator.Send(new UpdateCustomerV1Command { Customer = customer });

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            await _mediator.Send(new CreateCustomerV1Command { Customer = customer });

            return CreatedAtAction("GetCustomer", new { id = customer.ID }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _mediator.Send(new DeleteCustomerV1Command { CustomerID = id });

            return NoContent();
        }
    }
}
