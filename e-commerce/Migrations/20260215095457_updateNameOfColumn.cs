using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class updateNameOfColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartes_users_UserId",
                table: "cartes");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_UserId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "orders",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "orders",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_UserId",
                table: "orders",
                newName: "IX_orders_user_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "cartes",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_cartes_UserId",
                table: "cartes",
                newName: "IX_cartes_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cartes_users_user_id",
                table: "cartes",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartes_users_user_id",
                table: "cartes");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "orders",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_user_id",
                table: "orders",
                newName: "IX_orders_UserId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "cartes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_cartes_user_id",
                table: "cartes",
                newName: "IX_cartes_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartes_users_UserId",
                table: "cartes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_UserId",
                table: "orders",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
