using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication37.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_addtocard_productId",
                table: "addtocard",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_addtocard_product_productId",
                table: "addtocard",
                column: "productId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addtocard_product_productId",
                table: "addtocard");

            migrationBuilder.DropIndex(
                name: "IX_addtocard_productId",
                table: "addtocard");
        }
    }
}
