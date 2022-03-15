using GeneralStore.Data;
using GeneralStore.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Services
{
    public class CustomerService
    {
        private readonly Guid _userID;

        public CustomerService(Guid userId)
        {
            _userID = userId;
        }

        //Create a customer
        public bool CreateACustomer(CustomerCreate customerCreateModel)
        {
            if (customerCreateModel == null)
                return false;

            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    Customer customerToAdd = new Customer
                    {
                        FirstName = customerCreateModel.FirstName,
                        LastName = customerCreateModel.LastName
                    };
                    ctx.Customers.Add(customerToAdd);
                    if (ctx.SaveChanges() == 1)
                        return true;
                    return false;
                }

            }
            catch (Exception ex)
            {
                //Do something clever instead.
                Console.WriteLine(ex);
                return false;
            }
        }

        //get list of all customers
        public IEnumerable<CustomerDetail> GetAllCustomers()
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var listOfCustomers = ctx.Customers.Select(
                        cd => new CustomerDetail
                        {
                            CustomerId = cd.CustomerId,
                            FirstName = cd.FirstName,
                            LastName = cd.LastName,
                           // FullName = cd.FullName
                        });
                    return listOfCustomers.ToList();
                }
            }

            catch (Exception ex)
            {
                //Do something clever instead.
                Console.WriteLine(ex);
                return null;
            }
        }

        //get customer by ID
        public CustomerDetail GetCustomerById(int ? customerId)
        {
            if (customerId != null)
            {
                try
                {
                    using (var ctx = new ApplicationDbContext())
                    {
                        var customerToReturn = ctx.Customers.Find(customerId);
                        if (customerToReturn != null)
                        {
                            return new CustomerDetail
                            {
                                CustomerId = customerToReturn.CustomerId,
                                FirstName = customerToReturn.FirstName,
                                LastName = customerToReturn.LastName,

                            };
                        }
                        else
                            return null;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                    return null;
                }
            }
            return null;
        }

        //Update a customer
        public bool UpdateACustomer(int id, CustomerEdit model)
        {
            try
            {
                using(var ctx = new ApplicationDbContext())
                {
                    Customer customerToUpdate = ctx.Customers.Find(id);
                    if(customerToUpdate != null)
                    {
                        customerToUpdate.FirstName = model.FirstName;
                        customerToUpdate.LastName = model.LastName;
                    }
                        return ctx.SaveChanges() == 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
                return false;
            }
        }


        //Delete a customer
        public bool DeleteACustomer(int customerId)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var customerToDelete = ctx.Customers.Find(customerId);
                    if (customerToDelete != null)
                    {
                        ctx.Customers.Remove(customerToDelete);
                        return ctx.SaveChanges() == 1;
                    }
                    else
                        return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
                return false;
            }
        }
    }
}
