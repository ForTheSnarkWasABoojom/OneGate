using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OneGate.Backend.Core.Timeseries.Database.Migrations.Migrations
{
    public partial class _1619712270 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "artifact",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false),
                    layer_id = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    buy_probability = table.Column<float>(type: "real", nullable: true),
                    sell_probability = table.Column<float>(type: "real", nullable: true),
                    hold_probability = table.Column<float>(type: "real", nullable: true),
                    value = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artifact", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "layer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    owner_id = table.Column<int>(type: "integer", nullable: false),
                    asset_id = table.Column<int>(type: "integer", nullable: false),
                    interval = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ohlc",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    asset_id = table.Column<int>(type: "integer", nullable: false),
                    interval = table.Column<string>(type: "text", nullable: false),
                    low = table.Column<double>(type: "double precision", nullable: false),
                    high = table.Column<double>(type: "double precision", nullable: false),
                    open = table.Column<double>(type: "double precision", nullable: false),
                    close = table.Column<double>(type: "double precision", nullable: false),
                    volume = table.Column<double>(type: "double precision", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ohlc", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_artifact_type_layer_id_timestamp",
                table: "artifact",
                columns: new[] { "type", "layer_id", "timestamp" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_layer_owner_id_asset_id_interval",
                table: "layer",
                columns: new[] { "owner_id", "asset_id", "interval" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ohlc_asset_id_interval_timestamp",
                table: "ohlc",
                columns: new[] { "asset_id", "interval", "timestamp" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artifact");

            migrationBuilder.DropTable(
                name: "layer");

            migrationBuilder.DropTable(
                name: "ohlc");
        }
    }
}
