using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning_World.Migrations
{
    /// <inheritdoc />
    public partial class AddUserImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add the Image column to the Users table
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true // Allow null values
            );
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the Image column if rolling back the migration
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users"
            );
        }
    }

}
