using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitHub.Migrations
{
    /// <inheritdoc />
    public partial class updateFieldsUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "address",
                table: "User",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "User",
                newName: "address");
        }
    }
}
