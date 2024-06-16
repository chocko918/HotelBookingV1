using FluentValidation;
using HotelBooking2.Infrastructure;
using HotelBooking2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using HotelBooking2.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HotelBooking2.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly AbstractValidator<CreateCustomerDTO> _customerValidator;

        public CustomerRepository(HotelBookingDbContext context, AbstractValidator<CreateCustomerDTO> customerValidator)
        {
            _context = context;
            _customerValidator = customerValidator;
        }

        public async Task<Customer> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Customers.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null || !await PasswordHasherHelper.VerifyPassword(user.Password, password))
                return null;

            return user;
        }

        public async Task<Customer> RegisterAsync(CreateCustomerDTO customer)
        {
            var validationResult = await _customerValidator.ValidateAsync(customer);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var hashedPassword = await PasswordHasherHelper.HashPasswordAsync(customer.Password);

            var user = new Customer
            {
                Email = customer.Email,
                Password = hashedPassword,
                CustomerName = customer.CustomerName,
                Birthday = customer.Birthday
            };

            _context.Customers.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        //public async Task<CreateCustomerDTO> CreateCustomer(CreateCustomerDTO customer)
        //{
        //    var validationResult = await _customerValidator.ValidateAsync(customer);
        //    if (!validationResult.IsValid)
        //    {
        //        throw new ValidationException(validationResult.Errors);
        //    }

        //    var hashedPassword = await PasswordHasherHelper.HashPasswordAsync(customer.Password);

        //    Customer customerCreate = new Customer
        //    {
        //        CustomerID = Guid.NewGuid(),
       //        Email = customer.Email,
        //        Password = hashedPassword,
        //        Name = customer.Name,
        //        Birthday = customer.Birthday 
        //    };

        //    _context.Customers.Add(customerCreate);
        //    await _context.SaveChangesAsync();
        //    return customer;
        //}

        public async Task<Customer> GetCustomerByEmailAndPassword(string email, string password)
        {
            var customerPassword = await GetCustomerByEmail(email);

            // Check if customerPassword is not null
            if (customerPassword != null)
            {
                var isPasswordValid = await PasswordHasherHelper.VerifyPassword(customerPassword.Password, password);

                // Only query the database for customer if the password is valid
                if (isPasswordValid)
                {
                    return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
                }
            }

            return null; // Return null if customer not found or password is invalid
        }


        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task UpdateCustomer(Customer existingCustomer, UpdateCustomerDTO updatedCustomerDto)
        {
            if (!string.IsNullOrEmpty(updatedCustomerDto.CustomerName))
            {
                existingCustomer.CustomerName = updatedCustomerDto.CustomerName;
            }
            if (!string.IsNullOrEmpty(updatedCustomerDto.Email))
            {
                existingCustomer.Email = updatedCustomerDto.Email;
            }
            if (!string.IsNullOrEmpty(updatedCustomerDto.Password))
            {
                existingCustomer.Password = updatedCustomerDto.Password;
            }
            if (updatedCustomerDto.Birthday != default(DateTime))
            {
                existingCustomer.Birthday = updatedCustomerDto.Birthday;
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteCustomer(string email, string password)
        {
            var customer = await GetCustomerByEmailAndPassword(email, password);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

            }


        }
    }
}
