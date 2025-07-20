using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastTechFoods.Kitchen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToAlterCustomerIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Orders",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Orders",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");
        }
    }
}
