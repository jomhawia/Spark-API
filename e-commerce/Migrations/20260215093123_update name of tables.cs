using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class updatenameoftables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_user_user_id",
                table: "address");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_user_UserId",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_item_cart_cart_id",
                table: "cart_item");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_item_product_variants_product_variant_id",
                table: "cart_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_user_UserId",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_order_order_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_product_variants_product_variant_id",
                table: "order_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_item",
                table: "order_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order",
                table: "order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart_item",
                table: "cart_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart",
                table: "cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_address",
                table: "address");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "order_item",
                newName: "order_items");

            migrationBuilder.RenameTable(
                name: "order",
                newName: "orders");

            migrationBuilder.RenameTable(
                name: "cart_item",
                newName: "cart_items");

            migrationBuilder.RenameTable(
                name: "cart",
                newName: "cartes");

            migrationBuilder.RenameTable(
                name: "address",
                newName: "addresses");

            migrationBuilder.RenameIndex(
                name: "IX_order_item_product_variant_id",
                table: "order_items",
                newName: "IX_order_items_product_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_item_order_id_product_variant_id",
                table: "order_items",
                newName: "IX_order_items_order_id_product_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_UserId",
                table: "orders",
                newName: "IX_orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_cart_item_product_variant_id",
                table: "cart_items",
                newName: "IX_cart_items_product_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_cart_item_cart_id_product_variant_id",
                table: "cart_items",
                newName: "IX_cart_items_cart_id_product_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_cart_UserId",
                table: "cartes",
                newName: "IX_cartes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_address_user_id_is_default",
                table: "addresses",
                newName: "IX_addresses_user_id_is_default");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_items",
                table: "order_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart_items",
                table: "cart_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartes",
                table: "cartes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_addresses",
                table: "addresses",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_users_user_id",
                table: "addresses",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_items_cartes_cart_id",
                table: "cart_items",
                column: "cart_id",
                principalTable: "cartes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_items_product_variants_product_variant_id",
                table: "cart_items",
                column: "product_variant_id",
                principalTable: "product_variants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartes_users_UserId",
                table: "cartes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_orders_order_id",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_product_variants_product_variant_id",
                table: "order_items",
                column: "product_variant_id",
                principalTable: "product_variants",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_users_user_id",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_items_cartes_cart_id",
                table: "cart_items");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_items_product_variants_product_variant_id",
                table: "cart_items");

            migrationBuilder.DropForeignKey(
                name: "FK_cartes_users_UserId",
                table: "cartes");

            migrationBuilder.DropForeignKey(
                name: "FK_order_items_orders_order_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_order_items_product_variants_product_variant_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_UserId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_items",
                table: "order_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartes",
                table: "cartes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart_items",
                table: "cart_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_addresses",
                table: "addresses");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "order");

            migrationBuilder.RenameTable(
                name: "order_items",
                newName: "order_item");

            migrationBuilder.RenameTable(
                name: "cartes",
                newName: "cart");

            migrationBuilder.RenameTable(
                name: "cart_items",
                newName: "cart_item");

            migrationBuilder.RenameTable(
                name: "addresses",
                newName: "address");

            migrationBuilder.RenameIndex(
                name: "IX_orders_UserId",
                table: "order",
                newName: "IX_order_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_order_items_product_variant_id",
                table: "order_item",
                newName: "IX_order_item_product_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_items_order_id_product_variant_id",
                table: "order_item",
                newName: "IX_order_item_order_id_product_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_cartes_UserId",
                table: "cart",
                newName: "IX_cart_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_cart_items_product_variant_id",
                table: "cart_item",
                newName: "IX_cart_item_product_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_cart_items_cart_id_product_variant_id",
                table: "cart_item",
                newName: "IX_cart_item_cart_id_product_variant_id");

            migrationBuilder.RenameIndex(
                name: "IX_addresses_user_id_is_default",
                table: "address",
                newName: "IX_address_user_id_is_default");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order",
                table: "order",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_item",
                table: "order_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart",
                table: "cart",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart_item",
                table: "cart_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_address",
                table: "address",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_address_user_user_id",
                table: "address",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_user_UserId",
                table: "cart",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_item_cart_cart_id",
                table: "cart_item",
                column: "cart_id",
                principalTable: "cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_item_product_variants_product_variant_id",
                table: "cart_item",
                column: "product_variant_id",
                principalTable: "product_variants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_user_UserId",
                table: "order",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_order_order_id",
                table: "order_item",
                column: "order_id",
                principalTable: "order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_product_variants_product_variant_id",
                table: "order_item",
                column: "product_variant_id",
                principalTable: "product_variants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
