using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctrina.Migrations
{
    public partial class CreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    API = table.Column<string>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Authority = table.Column<string>(nullable: false),
                    Scopes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "InteractionActivities",
                columns: table => new
                {
                    InteractionId = table.Column<Guid>(nullable: false),
                    InteractionType = table.Column<string>(nullable: false),
                    CorrectResponsesPattern = table.Column<string>(nullable: true),
                    ChoiceInteractionActivity_Choices = table.Column<string>(type: "ntext", nullable: true),
                    Scale = table.Column<string>(type: "ntext", nullable: true),
                    Source = table.Column<string>(type: "ntext", nullable: true),
                    Target = table.Column<string>(type: "ntext", nullable: true),
                    Steps = table.Column<string>(type: "ntext", nullable: true),
                    Choices = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionActivities", x => x.InteractionId);
                });

            migrationBuilder.CreateTable(
                name: "ObjectRelations",
                columns: table => new
                {
                    ParentId = table.Column<Guid>(nullable: false),
                    ChildObjectType = table.Column<string>(nullable: false),
                    ChildId = table.Column<Guid>(nullable: false),
                    ParentObjectType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectRelations", x => new { x.ChildObjectType, x.ParentId, x.ChildId });
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(nullable: false),
                    Success = table.Column<bool>(nullable: true),
                    Completion = table.Column<bool>(nullable: true),
                    Response = table.Column<string>(nullable: true),
                    DurationTicks = table.Column<long>(nullable: true),
                    Duration = table.Column<string>(nullable: true),
                    Extensions = table.Column<string>(type: "ntext", nullable: true),
                    Score_Scaled = table.Column<double>(nullable: true),
                    Score_Raw = table.Column<double>(nullable: true),
                    Score_Min = table.Column<double>(nullable: true),
                    Score_Max = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                });

            migrationBuilder.CreateTable(
                name: "Verbs",
                columns: table => new
                {
                    VerbId = table.Column<Guid>(nullable: false),
                    Hash = table.Column<string>(maxLength: 40, nullable: false),
                    Id = table.Column<string>(maxLength: 2083, nullable: false),
                    Display = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verbs", x => x.VerbId);
                });

            migrationBuilder.CreateTable(
                name: "ActivityDefinitions",
                columns: table => new
                {
                    ActivityDefinitionId = table.Column<Guid>(nullable: false),
                    Names = table.Column<string>(type: "ntext", nullable: true),
                    Descriptions = table.Column<string>(type: "ntext", nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MoreInfo = table.Column<string>(nullable: true),
                    InteractionActivityInteractionId = table.Column<Guid>(nullable: true),
                    Extensions = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDefinitions", x => x.ActivityDefinitionId);
                    table.ForeignKey(
                        name: "FK_ActivityDefinitions_InteractionActivities_InteractionActivityInteractionId",
                        column: x => x.InteractionActivityInteractionId,
                        principalTable: "InteractionActivities",
                        principalColumn: "InteractionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentId = table.Column<Guid>(nullable: false),
                    ObjectType = table.Column<string>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    IFI_Key = table.Column<string>(maxLength: 12, nullable: true),
                    IFI_Value = table.Column<string>(maxLength: 200, nullable: true),
                    PersonEntityPersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentId);
                    table.ForeignKey(
                        name: "FK_Agents_Persons_PersonEntityPersonId",
                        column: x => x.PersonEntityPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agents_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(nullable: false),
                    Hash = table.Column<string>(maxLength: 40, nullable: false),
                    Id = table.Column<string>(maxLength: 2083, nullable: false),
                    DefinitionActivityDefinitionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityDefinitions_DefinitionActivityDefinitionId",
                        column: x => x.DefinitionActivityDefinitionId,
                        principalTable: "ActivityDefinitions",
                        principalColumn: "ActivityDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contexts",
                columns: table => new
                {
                    ContextId = table.Column<Guid>(nullable: false),
                    Registration = table.Column<Guid>(nullable: true),
                    InstructorId = table.Column<Guid>(nullable: true),
                    TeamId = table.Column<Guid>(nullable: true),
                    Revision = table.Column<string>(nullable: true),
                    Platform = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Extensions = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contexts", x => x.ContextId);
                    table.ForeignKey(
                        name: "FK_Contexts_Agents_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contexts_Agents_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupMemberId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    AgentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => x.GroupMemberId);
                    table.ForeignKey(
                        name: "FK_GroupMembers_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId");
                    table.ForeignKey(
                        name: "FK_GroupMembers_Agents_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Agents",
                        principalColumn: "AgentId");
                    table.ForeignKey(
                        name: "FK_GroupMembers_Agents_GroupMemberId",
                        column: x => x.GroupMemberId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 2083, nullable: true),
                    AgentId = table.Column<Guid>(nullable: true),
                    RegistrationId = table.Column<Guid>(nullable: true),
                    ActivityId = table.Column<Guid>(nullable: true),
                    ContentType = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<byte[]>(nullable: true),
                    Checksum = table.Column<string>(maxLength: 32, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    DocumentType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Activities_ActivityId1",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Agents_AgentId1",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContextActivities",
                columns: table => new
                {
                    ContextId = table.Column<Guid>(nullable: false),
                    ContextType = table.Column<string>(nullable: false),
                    ActivityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextActivities", x => new { x.ContextId, x.ContextType, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_ContextActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContextActivities_Contexts_ContextId",
                        column: x => x.ContextId,
                        principalTable: "Contexts",
                        principalColumn: "ContextId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statements",
                columns: table => new
                {
                    StatementId = table.Column<Guid>(nullable: false),
                    ActorId = table.Column<Guid>(nullable: false),
                    VerbId = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false),
                    ResultId = table.Column<Guid>(nullable: true),
                    ContextId = table.Column<Guid>(nullable: true),
                    Stored = table.Column<DateTimeOffset>(nullable: false),
                    Version = table.Column<string>(maxLength: 7, nullable: true),
                    ClientId = table.Column<Guid>(nullable: false),
                    FullStatement = table.Column<string>(nullable: true),
                    VoidingStatementId = table.Column<Guid>(nullable: true),
                    ObjectType = table.Column<string>(nullable: false),
                    ObjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statements", x => x.StatementId);
                    table.ForeignKey(
                        name: "FK_Statements_Agents_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Agents",
                        principalColumn: "AgentId");
                    table.ForeignKey(
                        name: "FK_Statements_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Statements_Contexts_ContextId",
                        column: x => x.ContextId,
                        principalTable: "Contexts",
                        principalColumn: "ContextId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statements_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statements_Verbs_VerbId",
                        column: x => x.VerbId,
                        principalTable: "Verbs",
                        principalColumn: "VerbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statements_Statements_VoidingStatementId",
                        column: x => x.VoidingStatementId,
                        principalTable: "Statements",
                        principalColumn: "StatementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubStatements",
                columns: table => new
                {
                    SubStatementId = table.Column<Guid>(nullable: false),
                    VerbId = table.Column<Guid>(nullable: false),
                    ActorAgentId = table.Column<Guid>(nullable: false),
                    ObjectType = table.Column<string>(nullable: false),
                    ObjectId = table.Column<Guid>(nullable: false),
                    ContextId = table.Column<Guid>(nullable: true),
                    ResultId = table.Column<Guid>(nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubStatements", x => x.SubStatementId);
                    table.ForeignKey(
                        name: "FK_SubStatements_Agents_ActorAgentId",
                        column: x => x.ActorAgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubStatements_Contexts_ContextId",
                        column: x => x.ContextId,
                        principalTable: "Contexts",
                        principalColumn: "ContextId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubStatements_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubStatements_Verbs_VerbId",
                        column: x => x.VerbId,
                        principalTable: "Verbs",
                        principalColumn: "VerbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UsageType = table.Column<string>(maxLength: 2083, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    Display = table.Column<string>(type: "ntext", nullable: false),
                    ContentType = table.Column<string>(maxLength: 255, nullable: false),
                    Payload = table.Column<byte[]>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true),
                    SHA2 = table.Column<string>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    StatementEntityStatementId = table.Column<Guid>(nullable: true),
                    SubStatementEntitySubStatementId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Statements_StatementEntityStatementId",
                        column: x => x.StatementEntityStatementId,
                        principalTable: "Statements",
                        principalColumn: "StatementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_SubStatements_SubStatementEntitySubStatementId",
                        column: x => x.SubStatementEntitySubStatementId,
                        principalTable: "SubStatements",
                        principalColumn: "SubStatementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DefinitionActivityDefinitionId",
                table: "Activities",
                column: "DefinitionActivityDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Hash",
                table: "Activities",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Id",
                table: "Activities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDefinitions_InteractionActivityInteractionId",
                table: "ActivityDefinitions",
                column: "InteractionActivityInteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PersonEntityPersonId",
                table: "Agents",
                column: "PersonEntityPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PersonId",
                table: "Agents",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ObjectType_IFI_Key_IFI_Value",
                table: "Agents",
                columns: new[] { "ObjectType", "IFI_Key", "IFI_Value" });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_StatementEntityStatementId",
                table: "Attachments",
                column: "StatementEntityStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_SubStatementEntitySubStatementId",
                table: "Attachments",
                column: "SubStatementEntitySubStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContextActivities_ActivityId",
                table: "ContextActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contexts_InstructorId",
                table: "Contexts",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contexts_TeamId",
                table: "Contexts",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ActivityId",
                table: "Documents",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ActivityId1",
                table: "Documents",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AgentId",
                table: "Documents",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AgentId1",
                table: "Documents",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_AgentId",
                table: "GroupMembers",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_ActorId",
                table: "Statements",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_ClientId",
                table: "Statements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_ContextId",
                table: "Statements",
                column: "ContextId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_ResultId",
                table: "Statements",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_VerbId",
                table: "Statements",
                column: "VerbId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_VoidingStatementId",
                table: "Statements",
                column: "VoidingStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_SubStatements_ActorAgentId",
                table: "SubStatements",
                column: "ActorAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubStatements_ContextId",
                table: "SubStatements",
                column: "ContextId");

            migrationBuilder.CreateIndex(
                name: "IX_SubStatements_ResultId",
                table: "SubStatements",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_SubStatements_VerbId",
                table: "SubStatements",
                column: "VerbId");

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_Hash",
                table: "Verbs",
                column: "Hash",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "ContextActivities");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "ObjectRelations");

            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropTable(
                name: "SubStatements");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Contexts");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Verbs");

            migrationBuilder.DropTable(
                name: "ActivityDefinitions");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "InteractionActivities");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
