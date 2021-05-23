using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OneGate.Backend.Core.Engines.Database.Migrations.Migrations
{
    public partial class _1619712270 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asset_mapping",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engine_id = table.Column<int>(type: "integer", nullable: false),
                    asset_id = table.Column<int>(type: "integer", nullable: false),
                    original_symbol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_mapping", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_asset_mapping_asset_id_engine_id",
                table: "asset_mapping",
                columns: new[] { "asset_id", "engine_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asset_mapping");
        }
    }
}
