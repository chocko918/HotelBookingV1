using FluentValidation;
using HotelBooking2.Infrastructure;
using HotelBooking2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBooking2.CustomerValidation
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerDTO>
    {
        private readonly HotelBookingDbContext _context;

        public CreateCustomerValidator(HotelBookingDbContext context)
        {
            _context = context;

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MustAsync(BeUniqueEmail).WithMessage("Email already exists");

            RuleFor(c => c.CustomerName)
                .NotEmpty().WithMessage("Name is required")
                .Matches("^[a-zA-Z ]*$").WithMessage("Name can only consist of alphabets");

            RuleFor(c => c.Birthday)
                .Must(BeOlderThanEighteenYears).WithMessage("Customer must be older than 18 years old");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            return existingCustomer == null;
        }

        private bool BeOlderThanEighteenYears(DateTime birthday)
        {
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;
            if (birthday.Date > today.AddYears(-age)) age--;
            return age >= 18;
        }


    }
}
