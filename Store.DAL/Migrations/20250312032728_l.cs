using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApi.DAL.Migrations
{
    public partial class l : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Orders_Orderid",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrders_Orderid",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Orderid",
                table: "ProductOrders",
                newName: "OrderId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "ProductOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "ProductOrders",
                newName: "Orderid");

            migrationBuilder.AlterColumn<int>(
                name: "Orderid",
                table: "ProductOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_Orderid",
                table: "ProductOrders",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Orders_Orderid",
                table: "ProductOrders",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id");
        }
    }
}
