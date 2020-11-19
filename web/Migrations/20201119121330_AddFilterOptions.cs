using Microsoft.EntityFrameworkCore.Migrations;

namespace web.Migrations
{
    public partial class AddFilterOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    CodeName = table.Column<string>(nullable: true),
                    ListingType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceOption_ListingType_ListingType",
                        column: x => x.ListingType,
                        principalTable: "ListingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SizeOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    CodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YearOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    CodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearOption", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceOption_ListingType",
                table: "PriceOption",
                column: "ListingType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceOption");

            migrationBuilder.DropTable(
                name: "SizeOption");

            migrationBuilder.DropTable(
                name: "YearOption");
        }
    }
}
