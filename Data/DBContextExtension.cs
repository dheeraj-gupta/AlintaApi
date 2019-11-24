using alintaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alintaApi.Data
{
    public static class DbContextExtension
    {

        public static void EnsureSeeded(this CustomersDbContext context)
        {

            //Ensure we have some status
            if (context.Customers.Any())
            {
                return;
            }

            // Initial values of the customers in our Data Store.
            context.Customers.AddRange
            (
                 new Customer { Id = 1, firstName = "Alan", lastName = "Chen", dateOfBirth = new DateTime(1970, 2, 3) },
                    new Customer { Id = 2, firstName = "Mikka", lastName = "Singh", dateOfBirth = new DateTime(1985, 4, 2) },
                    new Customer { Id = 3, firstName = "Tom", lastName = "Hanks", dateOfBirth = new DateTime(1962, 3, 4) },
                    new Customer { Id = 4, firstName = "Julia", lastName = "Roberts", dateOfBirth = new DateTime(1972, 4, 4) }
            );

            context.SaveChanges();

        }

    }
}
