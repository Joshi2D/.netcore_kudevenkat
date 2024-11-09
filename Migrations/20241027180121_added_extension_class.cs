using Microsoft.EntityFrameworkCore.Migrations;

namespace Kudvenkat_.NET_Core.Migrations
{
    public partial class added_extension_class : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Name", "Photopath" },
                values: new object[,]
                {
                    { 1, 1, "ram@gmail.com", "Ram", "" },
                    { 2, 3, "sham@gmail.com", "Sham", "" },
                    { 3, 2, "shiv@gmail.com", "Shiv", "" },
                    { 10, 2, "raj@gmail.com", "Raj", "" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
