using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking2.Migrations
{
    /// <inheritdoc />
    public partial class roomconfigurationedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 1003,
                column: "Name",
                value: "Sunflower");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 2003,
                column: "Name",
                value: "City");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 3003,
                column: "Name",
                value: "Piano");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 3004,
                column: "Name",
                value: "Nature");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 1003,
                column: "Name",
                value: "Cactus");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 2003,
                column: "Name",
                value: "Dragonfruit");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 3003,
                column: "Name",
                value: "Porcupine");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 3004,
                column: "Name",
                value: "Seahorse");
        }
    }
}
