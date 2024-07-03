using HotelBooking2.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking2.Repositories
{
    public interface ICustomerRepository
    {
        //Task<CreateCustomerDTO> CreateCustomer(CreateCustomerDTO customer);
        Task<Customer> GetCustomerByEmailAndPassword(string email, string password);
        Task<Customer> AuthenticateAsync(string email, string password);
        Task<Customer> RegisterAsync(CreateCustomerDTO customer);
        Task<Customer> GetCustomerByEmail(string email);
        Task<List<Customer>> GetAllCustomers();
        Task DeleteCustomer(string email, string password);
        Task UpdateCustomer(Customer existingCustomer, UpdateCustomerDTO updatedCustomerDto);



    }
}