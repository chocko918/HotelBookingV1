using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking2.Models
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            // Specify default values for Name, Pax, and Price using Fluent API
            builder.Property(r => r.RoomID).IsRequired();
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Pax).IsRequired();
            builder.Property(r => r.Price).IsRequired().HasColumnType("decimal(18, 2)");

            // Level1
            builder.HasData(
                new Room { RoomID = 1001, Name = "Hibiscus", Pax = 1, Price = 80 }
            );

            builder.HasData(
                new Room { RoomID = 1002, Name = "Orchid", Pax = 2, Price = 150 }
            );

            builder.HasData(
                new Room { RoomID = 1003, Name = "Cactus", Pax = 3, Price = 220 }
            );

            builder.HasData(
                new Room { RoomID = 1004, Name = "Cactus", Pax = 4, Price = 300 }
            );

            // Level 2
            builder.HasData(
                new Room { RoomID = 2001, Name = "Papaya", Pax = 1, Price = 80 }
            );

            builder.HasData(
                new Room { RoomID = 2002, Name = "Pineapple", Pax = 2, Price = 150 }
            );

            builder.HasData(
                new Room { RoomID = 2003, Name = "Dragonfruit", Pax = 3, Price = 220 }
            );

            builder.HasData(
                new Room { RoomID = 2004, Name = "Orange", Pax = 4, Price = 300 }
            );

            // Level 3
            builder.HasData(
                new Room { RoomID = 3001, Name = "Panda", Pax = 1, Price = 80 }
            );

            builder.HasData(
                new Room { RoomID = 3002, Name = "Unicorn", Pax = 2, Price = 150 }
            );

            builder.HasData(
                new Room { RoomID = 3003, Name = "Porcupine", Pax = 3, Price = 220 }
            );

            builder.HasData(
                new Room { RoomID = 3004, Name = "Seahorse", Pax = 4, Price = 300 }
            );
        }
    }
}

