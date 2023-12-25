using Microsoft.EntityFrameworkCore.Migrations;

namespace PillApp.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillViewModel_Customers_CustomerId",
                table: "BillViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_BillViewModel_Products_ProductId",
                table: "BillViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillViewModel",
                table: "BillViewModel");

            migrationBuilder.RenameTable(
                name: "BillViewModel",
                newName: "BillVM");

            migrationBuilder.RenameIndex(
                name: "IX_BillViewModel_ProductId",
                table: "BillVM",
                newName: "IX_BillVM_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BillViewModel_CustomerId",
                table: "BillVM",
                newName: "IX_BillVM_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillVM",
                table: "BillVM",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillVM_Customers_CustomerId",
                table: "BillVM",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillVM_Products_ProductId",
                table: "BillVM",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillVM_Customers_CustomerId",
                table: "BillVM");

            migrationBuilder.DropForeignKey(
                name: "FK_BillVM_Products_ProductId",
                table: "BillVM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillVM",
                table: "BillVM");

            migrationBuilder.RenameTable(
                name: "BillVM",
                newName: "BillViewModel");

            migrationBuilder.RenameIndex(
                name: "IX_BillVM_ProductId",
                table: "BillViewModel",
                newName: "IX_BillViewModel_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BillVM_CustomerId",
                table: "BillViewModel",
                newName: "IX_BillViewModel_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillViewModel",
                table: "BillViewModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillViewModel_Customers_CustomerId",
                table: "BillViewModel",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillViewModel_Products_ProductId",
                table: "BillViewModel",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
