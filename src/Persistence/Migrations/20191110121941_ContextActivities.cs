using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctrina.Persistence.Migrations
{
    public partial class ContextActivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ContextActivities");

            migrationBuilder.DropColumn(
                name: "Grouping",
                table: "ContextActivities");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "ContextActivities");

            migrationBuilder.DropColumn(
                name: "Parent",
                table: "ContextActivities");

            migrationBuilder.CreateTable(
                name: "ContextActivities_Category",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ContextActivitiesEntityContextActivitiesId = table.Column<Guid>(nullable: false),
                    Hash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextActivities_Category", x => new { x.ContextActivitiesEntityContextActivitiesId, x.Id });
                    table.ForeignKey(
                        name: "FK_ContextActivities_Category_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                        column: x => x.ContextActivitiesEntityContextActivitiesId,
                        principalTable: "ContextActivities",
                        principalColumn: "ContextActivitiesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContextActivities_Grouping",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ContextActivitiesEntityContextActivitiesId = table.Column<Guid>(nullable: false),
                    Hash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextActivities_Grouping", x => new { x.ContextActivitiesEntityContextActivitiesId, x.Id });
                    table.ForeignKey(
                        name: "FK_ContextActivities_Grouping_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                        column: x => x.ContextActivitiesEntityContextActivitiesId,
                        principalTable: "ContextActivities",
                        principalColumn: "ContextActivitiesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContextActivities_Other",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ContextActivitiesEntityContextActivitiesId = table.Column<Guid>(nullable: false),
                    Hash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextActivities_Other", x => new { x.ContextActivitiesEntityContextActivitiesId, x.Id });
                    table.ForeignKey(
                        name: "FK_ContextActivities_Other_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                        column: x => x.ContextActivitiesEntityContextActivitiesId,
                        principalTable: "ContextActivities",
                        principalColumn: "ContextActivitiesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContextActivities_Parent",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ContextActivitiesEntityContextActivitiesId = table.Column<Guid>(nullable: false),
                    Hash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextActivities_Parent", x => new { x.ContextActivitiesEntityContextActivitiesId, x.Id });
                    table.ForeignKey(
                        name: "FK_ContextActivities_Parent_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                        column: x => x.ContextActivitiesEntityContextActivitiesId,
                        principalTable: "ContextActivities",
                        principalColumn: "ContextActivitiesId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContextActivities_Category");

            migrationBuilder.DropTable(
                name: "ContextActivities_Grouping");

            migrationBuilder.DropTable(
                name: "ContextActivities_Other");

            migrationBuilder.DropTable(
                name: "ContextActivities_Parent");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ContextActivities",
                type: "ntext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grouping",
                table: "ContextActivities",
                type: "ntext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "ContextActivities",
                type: "ntext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parent",
                table: "ContextActivities",
                type: "ntext",
                nullable: true);
        }
    }
}
