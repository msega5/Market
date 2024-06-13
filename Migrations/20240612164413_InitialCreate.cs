using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GroupID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCount = table.Column<int>(type: "integer", nullable: false),
                    StorageName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("StoreID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    GroupID = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    ProductName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductID", x => x.ID);
                    table.ForeignKey(
                        name: "GroupToProduct",
                        column: x => x.ID,
                        principalTable: "ProductGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageName",
                columns: table => new
                {
                    ProductsID = table.Column<int>(type: "integer", nullable: false),
                    StoresID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageName", x => new { x.ProductsID, x.StoresID });
                    table.ForeignKey(
                        name: "FK_StorageName_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageName_Storage_StoresID",
                        column: x => x.StoresID,
                        principalTable: "Storage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_ProductName",
                table: "ProductGroups",
                column: "ProductName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageName_StoresID",
                table: "StorageName",
                column: "StoresID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageName");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "ProductGroups");
        }
    }
}
