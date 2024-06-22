using HotelBooking2.Models;
using HotelBooking2.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HotelBooking2.CustomerValidation;
using FluentValidation;
using HotelBooking2.Helpers;
using HotelBooking2.Service;
using System.Security.Claims;
using System.Globalization;

namespace HotelBooking2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtService _jwtService;

        public CustomerController(ICustomerRepository customerRepository, IJwtService JwtService)
        {
            _customerRepository = customerRepository;
            _jwtService = JwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateCustomerDTO customer)
        {
            try
            {
                // Create customer
                var createdCustomer = await _customerRepository.RegisterAsync(customer);
                if (createdCustomer == null)
                    return BadRequest("Username already exists");

                return Ok(createdCustomer);


            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try 
            { 
            var user = await _customerRepository.AuthenticateAsync(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { Message = "Invalid Email or password" });


            var token = await _jwtService.GenerateJwtTokenAsync(user);

            return Ok (new { Token = token, User = user });

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
             }
        }


        //[HttpPost("login")]
        //public async Task<IActionResult> Login(string email, string password)
        //{
        //    try
        //    {
        //        var customer = await _customerRepository.GetCustomerByEmailAndPassword(email, password);
        //        if (customer == null)
        //        {
        //            return BadRequest("Invalid email or password");
        //        }

        //        return Ok(customer);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}

        [HttpGet("customer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("UpdateCustomerInfo")]
        public async Task<IActionResult> UpdateCustomer(string email, string password, UpdateCustomerDTO updatedCustomerDto)
        {
            try
            {
                // Validate customer's credentials
                var customer = await _customerRepository.GetCustomerByEmailAndPassword(email, password);
                if (customer == null)
                {
                    return BadRequest("Invalid email or password");
                }

                // Update customer information
                await _customerRepository.UpdateCustomer(customer, updatedCustomerDto);

                return Ok($"Customer with email {email} has been updated");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(string email, string password)
        {
            try
            {
                // Validate customer's credentials
                var customer = await _customerRepository.GetCustomerByEmailAndPassword(email, password);
                if (customer == null)
                {
                    return BadRequest("Invalid email or password");
                }

                await _customerRepository.DeleteCustomer(email,password);
                return Ok($"Customer with email {email} has been deleted");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }

}
