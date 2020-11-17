using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctrina.Migrations
{
    public partial class PersonId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Persons_PersonId",
                table: "Agents");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Agents",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Persons_PersonId",
                table: "Agents",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Persons_PersonId",
                table: "Agents");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Persons_PersonId",
                table: "Agents",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
