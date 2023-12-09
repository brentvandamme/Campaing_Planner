using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFDal.Migrations
{
    /// <inheritdoc />
    public partial class productplanningupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanningProduct_Planning_planningsId",
                table: "PlanningProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanningProduct_Product_ProductsId",
                table: "PlanningProduct");

            migrationBuilder.RenameColumn(
                name: "planningsId",
                table: "PlanningProduct",
                newName: "PlanningId");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "PlanningProduct",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanningProduct_planningsId",
                table: "PlanningProduct",
                newName: "IX_PlanningProduct_PlanningId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanningProduct_Planning_PlanningId",
                table: "PlanningProduct",
                column: "PlanningId",
                principalTable: "Planning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanningProduct_Product_ProductId",
                table: "PlanningProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanningProduct_Planning_PlanningId",
                table: "PlanningProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanningProduct_Product_ProductId",
                table: "PlanningProduct");

            migrationBuilder.RenameColumn(
                name: "PlanningId",
                table: "PlanningProduct",
                newName: "planningsId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "PlanningProduct",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanningProduct_PlanningId",
                table: "PlanningProduct",
                newName: "IX_PlanningProduct_planningsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanningProduct_Planning_planningsId",
                table: "PlanningProduct",
                column: "planningsId",
                principalTable: "Planning",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanningProduct_Product_ProductsId",
                table: "PlanningProduct",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
