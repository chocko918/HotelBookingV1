using HotelBooking2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HotelBooking2.Infrastructure
{
    public class HotelBookingDbContext : DbContext
    {
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<BookingRoom> BookingRooms { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingRoom>()
                .HasKey(br => new { br.BookingID, br.RoomID });

            modelBuilder.Entity<BookingRoom>()
                .HasOne(br => br.Booking)
                .WithMany(b => b.BookingRooms)
                .HasForeignKey(br => br.BookingID);

            modelBuilder.Entity<BookingRoom>()
                .HasOne(br => br.Room)
                .WithMany(r => r.BookingRooms)
                .HasForeignKey(br => br.RoomID);

            modelBuilder.Entity<Room>()
                .HasKey(br => new { br.RoomID});

            modelBuilder.Entity<Cart>()
                .HasKey(br => new { br.ItemID });

            modelBuilder.ApplyConfiguration(new RoomConfiguration());




        }
    }
}
