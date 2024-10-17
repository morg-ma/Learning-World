using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning_World.Migrations
{
    /// <inheritdoc />
    public partial class paymentmethodtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "PaymentMethods");
        }
    }
}