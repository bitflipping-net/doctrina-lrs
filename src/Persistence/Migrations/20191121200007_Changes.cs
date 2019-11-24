using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctrina.Persistence.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentEntity_Statements_StatementEntityStatementId",
                table: "AttachmentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentEntity_SubStatements_SubStatementEntitySubStatementId",
                table: "AttachmentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ContextActivities_Category_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ContextActivities_Grouping_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Grouping");

            migrationBuilder.DropForeignKey(
                name: "FK_ContextActivities_Other_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Other");

            migrationBuilder.DropForeignKey(
                name: "FK_ContextActivities_Parent_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Parent");

            migrationBuilder.DropForeignKey(
                name: "FK_Statements_StatementRefEntity_Object_StatementRefId",
                table: "Statements");

            migrationBuilder.DropForeignKey(
                name: "FK_SubStatements_StatementRefEntity_Object_StatementRefId",
                table: "SubStatements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatementRefEntity",
                table: "StatementRefEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttachmentEntity",
                table: "AttachmentEntity");

            migrationBuilder.RenameTable(
                name: "StatementRefEntity",
                newName: "StatementRefs");

            migrationBuilder.RenameTable(
                name: "AttachmentEntity",
                newName: "Attachments");

            migrationBuilder.RenameIndex(
                name: "IX_StatementRefEntity_StatementId",
                table: "StatementRefs",
                newName: "IX_StatementRefs_StatementId");

            migrationBuilder.RenameIndex(
                name: "IX_AttachmentEntity_SubStatementEntitySubStatementId",
                table: "Attachments",
                newName: "IX_Attachments_SubStatementEntitySubStatementId");

            migrationBuilder.RenameIndex(
                name: "IX_AttachmentEntity_StatementEntityStatementId",
                table: "Attachments",
                newName: "IX_Attachments_StatementEntityStatementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatementRefs",
                table: "StatementRefs",
                column: "StatementRefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Statements_StatementEntityStatementId",
                table: "Attachments",
                column: "StatementEntityStatementId",
                principalTable: "Statements",
                principalColumn: "StatementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_SubStatements_SubStatementEntitySubStatementId",
                table: "Attachments",
                column: "SubStatementEntitySubStatementId",
                principalTable: "SubStatements",
                principalColumn: "SubStatementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContextActivities_Category_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Category",
                column: "ContextActivitiesEntityContextActivitiesId",
                principalTable: "ContextActivities",
                principalColumn: "ContextActivitiesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContextActivities_Grouping_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Grouping",
                column: "ContextActivitiesEntityContextActivitiesId",
                principalTable: "ContextActivities",
                principalColumn: "ContextActivitiesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContextActivities_Other_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Other",
                column: "ContextActivitiesEntityContextActivitiesId",
                principalTable: "ContextActivities",
                principalColumn: "ContextActivitiesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContextActivities_Parent_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Parent",
                column: "ContextActivitiesEntityContextActivitiesId",
                principalTable: "ContextActivities",
                principalColumn: "ContextActivitiesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statements_StatementRefs_Object_StatementRefId",
                table: "Statements",
                column: "Object_StatementRefId",
                principalTable: "StatementRefs",
                principalColumn: "StatementRefId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubStatements_StatementRefs_Object_StatementRefId",
                table: "SubStatements",
                column: "Object_StatementRefId",
                principalTable: "StatementRefs",
                principalColumn: "StatementRefId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Statements_StatementEntityStatementId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_SubStatements_SubStatementEntitySubStatementId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContextActivities_Category_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ContextActivities_Grouping_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Grouping");

            migrationBuilder.DropForeignKey(
                name: "FK_ContextActivities_Other_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Other");

            migrationBuilder.DropForeignKey(
                name: "FK_ContextActivities_Parent_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Parent");

            migrationBuilder.DropForeignKey(
                name: "FK_Statements_StatementRefs_Object_StatementRefId",
                table: "Statements");

            migrationBuilder.DropForeignKey(
                name: "FK_SubStatements_StatementRefs_Object_StatementRefId",
                table: "SubStatements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatementRefs",
                table: "StatementRefs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.RenameTable(
                name: "StatementRefs",
                newName: "StatementRefEntity");

            migrationBuilder.RenameTable(
                name: "Attachments",
                newName: "AttachmentEntity");

            migrationBuilder.RenameIndex(
                name: "IX_StatementRefs_StatementId",
                table: "StatementRefEntity",
                newName: "IX_StatementRefEntity_StatementId");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_SubStatementEntitySubStatementId",
                table: "AttachmentEntity",
                newName: "IX_AttachmentEntity_SubStatementEntitySubStatementId");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_StatementEntityStatementId",
                table: "AttachmentEntity",
                newName: "IX_AttachmentEntity_StatementEntityStatementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatementRefEntity",
                table: "StatementRefEntity",
                column: "StatementRefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttachmentEntity",
                table: "AttachmentEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentEntity_Statements_StatementEntityStatementId",
                table: "AttachmentEntity",
                column: "StatementEntityStatementId",
                principalTable: "Statements",
                principalColumn: "StatementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentEntity_SubStatements_SubStatementEntitySubStatementId",
                table: "AttachmentEntity",
                column: "SubStatementEntitySubStatementId",
                principalTable: "SubStatements",
                principalColumn: "SubStatementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContextActivities_Category_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Category",
                column: "ContextActivitiesEntityContextActivitiesId",
                principalTable: "ContextActivities",
                principalColumn: "ContextActivitiesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContextActivities_Grouping_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Grouping",
                column: "ContextActivitiesEntityContextActivitiesId",
                principalTable: "ContextActivities",
                principalColumn: "ContextActivitiesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContextActivities_Other_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Other",
                column: "ContextActivitiesEntityContextActivitiesId",
                principalTable: "ContextActivities",
                principalColumn: "ContextActivitiesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContextActivities_Parent_ContextActivities_ContextActivitiesEntityContextActivitiesId",
                table: "ContextActivities_Parent",
                column: "ContextActivitiesEntityContextActivitiesId",
                principalTable: "ContextActivities",
                principalColumn: "ContextActivitiesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Statements_StatementRefEntity_Object_StatementRefId",
                table: "Statements",
                column: "Object_StatementRefId",
                principalTable: "StatementRefEntity",
                principalColumn: "StatementRefId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubStatements_StatementRefEntity_Object_StatementRefId",
                table: "SubStatements",
                column: "Object_StatementRefId",
                principalTable: "StatementRefEntity",
                principalColumn: "StatementRefId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
