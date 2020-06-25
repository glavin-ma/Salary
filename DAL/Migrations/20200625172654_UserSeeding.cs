using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UserSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "109ed6b6-a082-42f2-86a4-e53d1652d887");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a105232-4870-4572-bedd-abceffe8c936");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef4cda4e-591d-44cf-8cb3-e47640bc7625");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "109ed6b6-a082-42f2-86a4-e53d1652d887", "67f120bd-ca31-4163-99b6-93f4569bebc3", "Employee", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4a105232-4870-4572-bedd-abceffe8c936", "c5e2a911-0444-4b2c-88ad-5ca9bd82fd6c", "Manager", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ef4cda4e-591d-44cf-8cb3-e47640bc7625", "7ef35c0d-5c60-4b1f-bc0d-e34399eca56c", "Salesman", null });
        }
    }
}
