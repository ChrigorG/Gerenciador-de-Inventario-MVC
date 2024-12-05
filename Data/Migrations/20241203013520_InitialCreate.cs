using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_permission_group = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    function = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    status_deleted = table.Column<bool>(type: "bit", nullable: false),
                    id_user_created = table.Column<int>(type: "int", nullable: false),
                    datetime_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_user_updated = table.Column<int>(type: "int", nullable: true),
                    datetime_updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissionGroups",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    action_employee = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    action_product = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    action_stock_movements = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    action_permission_group = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    status_deleted = table.Column<bool>(type: "bit", nullable: false),
                    id_user_created = table.Column<int>(type: "int", nullable: false),
                    datetime_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_user_updated = table.Column<int>(type: "int", nullable: true),
                    datetime_updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissionGroups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    unit_type = table.Column<string>(type: "char(2)", nullable: false),
                    status_deleted = table.Column<bool>(type: "bit", nullable: false),
                    id_user_created = table.Column<int>(type: "int", nullable: false),
                    datetime_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_user_updated = table.Column<int>(type: "int", nullable: true),
                    datetime_updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stocks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movement_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    movement_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_product = table.Column<int>(type: "int", nullable: false),
                    status_deleted = table.Column<bool>(type: "bit", nullable: false),
                    id_user_created = table.Column<int>(type: "int", nullable: false),
                    datetime_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_user_updated = table.Column<int>(type: "int", nullable: true),
                    datetime_updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => x.id);
                    table.ForeignKey(
                        name: "FK_stocks_products_id_product",
                        column: x => x.id_product,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_stocks_id_product",
                table: "stocks",
                column: "id_product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "permissionGroups");

            migrationBuilder.DropTable(
                name: "stocks");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
