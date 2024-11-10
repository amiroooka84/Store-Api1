using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApi.DAL.Migrations
{
    public partial class w1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesPath_Products_Productid",
                table: "ImagesPath");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Products_Productid",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Order_Orderid",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_Productid",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ImagesPath_Productid",
                table: "ImagesPath");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameColumn(
                name: "Productid",
                table: "ProductColors",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Productid",
                table: "ImagesPath",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductColors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ImagesPath",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Orders_Orderid",
                table: "ProductOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductColors",
                newName: "Productid");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ImagesPath",
                newName: "Productid");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Productid",
                table: "ProductColors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Productid",
                table: "ImagesPath",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_Productid",
                table: "ProductColors",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesPath_Productid",
                table: "ImagesPath",
                column: "Productid");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesPath_Products_Productid",
                table: "ImagesPath",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Products_Productid",
                table: "ProductColors",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Order_Orderid",
                table: "ProductOrders",
                column: "Orderid",
                principalTable: "Order",
                principalColumn: "id");
        }
    }
}
