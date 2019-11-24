using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using alintaApi.Models;

namespace alintaApi.Data
{
    // This class is the template for initializing items to the Customers Data Store.
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CustomersDbContext(serviceProvider.GetRequiredService<DbContextOptions<CustomersDbContext>>()))
            {
                // If the Customers Data Store already has data then 
                // just exit the Initialize method.
                if (context.Customers.Any())
                {
                    return;
                }

                // Initial values of the customers in our Data Store.
                context.Customers.AddRange
                (
                    new Customer { Id = 1, firstName = "Steve", lastName = "Rogers", dateOfBirth = new DateTime(1910, 1, 1) },
                    new Customer { Id = 2, firstName = "Tony", lastName = "Stark", dateOfBirth = new DateTime(1970, 2, 2) },
                    new Customer { Id = 3, firstName = "Bruce", lastName = "Banner", dateOfBirth = new DateTime(1975, 3, 3) },
                    new Customer { Id = 4, firstName = "Peter", lastName = "Parker", dateOfBirth = new DateTime(2002, 4, 4) }
                );

                context.SaveChanges();
            }
            
        }
    }
}
