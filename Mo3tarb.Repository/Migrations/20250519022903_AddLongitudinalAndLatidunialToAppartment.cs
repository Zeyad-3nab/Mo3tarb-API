using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mo3tarb.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddLongitudinalAndLatidunialToAppartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "address_Lat",
                table: "Apartments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "address_Lon",
                table: "Apartments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address_Lat",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "address_Lon",
                table: "Apartments");
        }
    }
}
