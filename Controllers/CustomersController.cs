using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using alintaApi.Data;
using alintaApi.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace alintaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private CustomersDbContext _customersDbContext;

        public ILogger<CustomersController> _logger { get; }

        public CustomersController(CustomersDbContext customersDBContext, ILogger<CustomersController> logger)
        {
            _customersDbContext = customersDBContext;
            _logger = logger;
        }

        /// <summary>
        ///     Get All the Customers within the DataSource.
        /// </summary>
        /// <param name="sort">
        ///     The order you want to arrange the Resulting data.
        /// </param>
        /// <returns>
        ///     An Object I can iterate over.
        /// </returns>
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IQueryable<Customer> GetAll()
        {
            try
            {
                IQueryable<Customer> customers;
                return customers = _customersDbContext.Customers;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Returns the result of a single Customer based on the Id .
        /// </summary>
        /// <param name="id">
        ///     The Id of the Customer.
        /// </param>
        /// <returns>
        ///     An ObjectResult with a value of the Customer Object .
        /// </returns>
        [HttpGet("{id}", Name = "Get")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var customer = await _customersDbContext.Customers.FindAsync(id);

                if (customer == null)
                {
                    return NotFound($"No Record found with id of {id}...");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogError(ex.Message);
                throw;
            }
        }

        

        /// <summary>
        ///     Search for the customers based on either their First Name, Last Name or both.
        /// </summary>
        /// <param name="firstName">
        ///     The First Name of the customer you wish to locate.
        /// </param>
        /// <param name="lastName">
        ///     The Last Name of the customer you wish to locate.
        /// </param>
        /// <returns>
        ///     Returns a list of customers that match the search criteria provided.
        /// </returns>
        [HttpGet]
        [Route("[action]")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> SearchByName(string firstName = "", string lastName = "")
        {
            try
            {
                var customers = await _customersDbContext.Customers.Where(c => c.firstName.Contains(firstName) && c.lastName.Contains(lastName)).FirstOrDefaultAsync();

                if (customers==null && customers.firstName == "")
                {
                    return NotFound("No customers found with given search parameters...");
                }
                else
                {
                    return Ok(customers);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Add a new customer to the data store.
        /// </summary>
        /// <param name="customer">
        ///     A Customer object is required to add the new databse.
        /// </param>
        /// <returns>
        ///     An IActionResult as well as the customer object provided.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            try
            {
                _customersDbContext.Customers.Add(customer);
               await _customersDbContext.SaveChangesAsync();

                return Ok(customer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Updates one or more property values of a Customer object.
        /// </summary>
        /// <param name="id">
        ///     The Id of the customer you want to update.
        /// </param>
        /// <param name="customer">
        ///     The Customer object with the modified field/s.
        /// </param>
        /// <returns>
        ///     Returns an IActionResult and the NEW Customer object.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            try
            {
                var entity = await _customersDbContext.Customers.FindAsync(id);

                if (entity == null)
                {
                    return NotFound($"Update Unsuccessful. No record found with id of {id}...");
                }
                else
                {
                    // Replacing the customer information in the data store with the information
                    // provided in the body of the PUT Request.
                    entity.firstName = customer.firstName;
                    entity.lastName = customer.lastName;
                    entity.dateOfBirth = customer.dateOfBirth;
                    await _customersDbContext.SaveChangesAsync();

                    return Ok(entity);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Removes a customer from the data source.
        /// </summary>
        /// <param name="id">
        ///     The Id of the customer you want to remove.
        /// </param>
        /// <returns>
        ///     An IActionResult of Ok.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customer = await _customersDbContext.Customers.FindAsync(id);

                if (customer == null)
                {
                    return NotFound($"Deletion Unsuccessful. No record found with id of {id}...");
                }
                else
                {
                    _customersDbContext.Customers.Remove(customer);
                    await _customersDbContext.SaveChangesAsync();

                    return Ok("Customer deleted successfully...");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
