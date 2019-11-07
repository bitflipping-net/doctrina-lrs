using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctrina.Persistence.Migrations
{
    public partial class CreateDoctrinaSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgentAccounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(nullable: false),
                    HomePage = table.Column<string>(maxLength: 2083, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentAccounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "ContextActivities",
                columns: table => new
                {
                    ContextActivitiesId = table.Column<Guid>(nullable: false),
                    Parent = table.Column<string>(type: "ntext", nullable: true),
                    Grouping = table.Column<string>(type: "ntext", nullable: true),
                    Category = table.Column<string>(type: "ntext", nullable: true),
                    Other = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextActivities", x => x.ContextActivitiesId);
                });

            migrationBuilder.CreateTable(
                name: "InteractionActivities",
                columns: table => new
                {
                    InteractionId = table.Column<Guid>(nullable: false),
                    InteractionType = table.Column<string>(nullable: false),
                    CorrectResponsesPattern = table.Column<string>(nullable: true),
                    Choices = table.Column<string>(type: "ntext", nullable: true),
                    Scale = table.Column<string>(type: "ntext", nullable: true),
                    Source = table.Column<string>(type: "ntext", nullable: true),
                    Target = table.Column<string>(type: "ntext", nullable: true),
                    Steps = table.Column<string>(type: "ntext", nullable: true),
                    SequencingInteractionActivity_Choices = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionActivities", x => x.InteractionId);
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
                name: "StatementRefEntity",
                columns: table => new
                {
                    StatementRefId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementRefEntity", x => x.StatementRefId);
                });

            migrationBuilder.CreateTable(
                name: "Verbs",
                columns: table => new
                {
                    VerbId = table.Column<Guid>(nullable: false),
                    Hash = table.Column<string>(maxLength: 32, nullable: false),
                    Id = table.Column<string>(maxLength: 2083, nullable: false),
                    Display = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verbs", x => x.VerbId);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentId = table.Column<Guid>(nullable: false),
                    ObjectType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Mbox = table.Column<string>(maxLength: 128, nullable: true),
                    Mbox_SHA1SUM = table.Column<string>(maxLength: 40, nullable: true),
                    OpenId = table.Column<string>(nullable: true),
                    AccountId = table.Column<Guid>(nullable: true),
                    GroupEntityAgentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentId);
                    table.ForeignKey(
                        name: "FK_Agents_AgentAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AgentAccounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agents_Agents_GroupEntityAgentId",
                        column: x => x.GroupEntityAgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "AgentProfiles",
                columns: table => new
                {
                    AgentProfileId = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<string>(maxLength: 2083, nullable: false),
                    AgentId = table.Column<Guid>(nullable: true),
                    Document_ContentType = table.Column<string>(maxLength: 255, nullable: true),
                    Document_Content = table.Column<byte[]>(nullable: true),
                    Document_Checksum = table.Column<string>(maxLength: 50, nullable: true),
                    Document_LastModified = table.Column<DateTimeOffset>(nullable: true, defaultValue: new DateTimeOffset(new DateTime(2019, 10, 13, 18, 19, 47, 30, DateTimeKind.Unspecified).AddTicks(8770), new TimeSpan(0, 0, 0, 0, 0))),
                    Document_CreateDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentProfiles", x => x.AgentProfileId);
                    table.ForeignKey(
                        name: "FK_AgentProfiles_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contexts",
                columns: table => new
                {
                    ContextId = table.Column<Guid>(nullable: false),
                    Registration = table.Column<Guid>(nullable: true),
                    InstructorAgentId = table.Column<Guid>(nullable: true),
                    TeamAgentId = table.Column<Guid>(nullable: true),
                    Revision = table.Column<string>(nullable: true),
                    Platform = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Extensions = table.Column<string>(type: "ntext", nullable: true),
                    ContextActivitiesId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contexts", x => x.ContextId);
                    table.ForeignKey(
                        name: "FK_Contexts_ContextActivities_ContextActivitiesId",
                        column: x => x.ContextActivitiesId,
                        principalTable: "ContextActivities",
                        principalColumn: "ContextActivitiesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contexts_Agents_InstructorAgentId",
                        column: x => x.InstructorAgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contexts_Agents_TeamAgentId",
                        column: x => x.TeamAgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(nullable: false),
                    Hash = table.Column<string>(maxLength: 32, nullable: false),
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
                name: "ActivityProfiles",
                columns: table => new
                {
                    ActivityProfileId = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<string>(nullable: false),
                    ActivityId = table.Column<Guid>(nullable: true),
                    RegistrationId = table.Column<Guid>(nullable: true),
                    Document_ContentType = table.Column<string>(maxLength: 255, nullable: true),
                    Document_Content = table.Column<byte[]>(nullable: true),
                    Document_Checksum = table.Column<string>(maxLength: 50, nullable: true),
                    Document_LastModified = table.Column<DateTimeOffset>(nullable: true, defaultValue: new DateTimeOffset(new DateTime(2019, 10, 13, 18, 19, 47, 18, DateTimeKind.Unspecified).AddTicks(2782), new TimeSpan(0, 0, 0, 0, 0))),
                    Document_CreateDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityProfiles", x => x.ActivityProfileId);
                    table.ForeignKey(
                        name: "FK_ActivityProfiles_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityStates",
                columns: table => new
                {
                    ActivityStateId = table.Column<Guid>(nullable: false),
                    StateId = table.Column<string>(maxLength: 2083, nullable: false),
                    Registration = table.Column<Guid>(nullable: true),
                    Document_ContentType = table.Column<string>(maxLength: 255, nullable: true),
                    Document_Content = table.Column<byte[]>(nullable: true),
                    Document_Checksum = table.Column<string>(maxLength: 50, nullable: true),
                    Document_LastModified = table.Column<DateTimeOffset>(nullable: true, defaultValue: new DateTimeOffset(new DateTime(2019, 10, 13, 18, 19, 47, 26, DateTimeKind.Unspecified).AddTicks(952), new TimeSpan(0, 0, 0, 0, 0))),
                    Document_CreateDate = table.Column<DateTimeOffset>(nullable: true),
                    AgentId = table.Column<Guid>(nullable: true),
                    ActivityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStates", x => x.ActivityStateId);
                    table.ForeignKey(
                        name: "FK_ActivityStates_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityStates_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubStatements",
                columns: table => new
                {
                    SubStatementId = table.Column<Guid>(nullable: false),
                    VerbId = table.Column<Guid>(nullable: false),
                    ActorAgentId = table.Column<Guid>(nullable: false),
                    Object_ObjectType = table.Column<int>(nullable: true),
                    Object_AgentId = table.Column<Guid>(nullable: true),
                    Object_ActivityId = table.Column<Guid>(nullable: true),
                    Object_StatementRefId = table.Column<Guid>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_SubStatements_Activities_Object_ActivityId",
                        column: x => x.Object_ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubStatements_Agents_Object_AgentId",
                        column: x => x.Object_AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubStatements_StatementRefEntity_Object_StatementRefId",
                        column: x => x.Object_StatementRefId,
                        principalTable: "StatementRefEntity",
                        principalColumn: "StatementRefId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statements",
                columns: table => new
                {
                    StatementId = table.Column<Guid>(nullable: false),
                    ActorAgentId = table.Column<Guid>(nullable: false),
                    VerbId = table.Column<Guid>(nullable: false),
                    Object_ObjectType = table.Column<int>(nullable: true),
                    Object_AgentId = table.Column<Guid>(nullable: true),
                    Object_ActivityId = table.Column<Guid>(nullable: true),
                    Object_SubStatementId = table.Column<Guid>(nullable: true),
                    Object_StatementRefId = table.Column<Guid>(nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false),
                    ResultId = table.Column<Guid>(nullable: true),
                    ContextId = table.Column<Guid>(nullable: true),
                    Stored = table.Column<DateTimeOffset>(nullable: false),
                    Version = table.Column<string>(maxLength: 7, nullable: true),
                    AuthorityId = table.Column<Guid>(nullable: true),
                    FullStatement = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statements", x => x.StatementId);
                    table.ForeignKey(
                        name: "FK_Statements_Agents_ActorAgentId",
                        column: x => x.ActorAgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statements_Agents_AuthorityId",
                        column: x => x.AuthorityId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_Statements_Activities_Object_ActivityId",
                        column: x => x.Object_ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statements_Agents_Object_AgentId",
                        column: x => x.Object_AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statements_StatementRefEntity_Object_StatementRefId",
                        column: x => x.Object_StatementRefId,
                        principalTable: "StatementRefEntity",
                        principalColumn: "StatementRefId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statements_SubStatements_Object_SubStatementId",
                        column: x => x.Object_SubStatementId,
                        principalTable: "SubStatements",
                        principalColumn: "SubStatementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentEntity",
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
                    table.PrimaryKey("PK_AttachmentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttachmentEntity_Statements_StatementEntityStatementId",
                        column: x => x.StatementEntityStatementId,
                        principalTable: "Statements",
                        principalColumn: "StatementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttachmentEntity_SubStatements_SubStatementEntitySubStatementId",
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
                name: "IX_ActivityProfiles_ActivityId",
                table: "ActivityProfiles",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityStates_ActivityId",
                table: "ActivityStates",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityStates_AgentId",
                table: "ActivityStates",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentAccounts_HomePage_Name",
                table: "AgentAccounts",
                columns: new[] { "HomePage", "Name" },
                unique: true,
                filter: "[HomePage] IS NOT NULL AND [Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AgentProfiles_AgentId",
                table: "AgentProfiles",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentProfiles_ProfileId",
                table: "AgentProfiles",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AccountId",
                table: "Agents",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_GroupEntityAgentId",
                table: "Agents",
                column: "GroupEntityAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ObjectType_Mbox",
                table: "Agents",
                columns: new[] { "ObjectType", "Mbox" },
                unique: true,
                filter: "[Mbox] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ObjectType_Mbox_SHA1SUM",
                table: "Agents",
                columns: new[] { "ObjectType", "Mbox_SHA1SUM" },
                unique: true,
                filter: "[Mbox_SHA1SUM] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ObjectType_OpenId",
                table: "Agents",
                columns: new[] { "ObjectType", "OpenId" },
                unique: true,
                filter: "[OpenId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentEntity_StatementEntityStatementId",
                table: "AttachmentEntity",
                column: "StatementEntityStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentEntity_SubStatementEntitySubStatementId",
                table: "AttachmentEntity",
                column: "SubStatementEntitySubStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_Contexts_ContextActivitiesId",
                table: "Contexts",
                column: "ContextActivitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Contexts_InstructorAgentId",
                table: "Contexts",
                column: "InstructorAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contexts_TeamAgentId",
                table: "Contexts",
                column: "TeamAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementRefEntity_Id",
                table: "StatementRefEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_ActorAgentId",
                table: "Statements",
                column: "ActorAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_AuthorityId",
                table: "Statements",
                column: "AuthorityId");

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
                name: "IX_Statements_Object_ActivityId",
                table: "Statements",
                column: "Object_ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_Object_AgentId",
                table: "Statements",
                column: "Object_AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_Object_StatementRefId",
                table: "Statements",
                column: "Object_StatementRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_Object_SubStatementId",
                table: "Statements",
                column: "Object_SubStatementId");

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
                name: "IX_SubStatements_Object_ActivityId",
                table: "SubStatements",
                column: "Object_ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubStatements_Object_AgentId",
                table: "SubStatements",
                column: "Object_AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubStatements_Object_StatementRefId",
                table: "SubStatements",
                column: "Object_StatementRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_Hash",
                table: "Verbs",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Verbs_Id",
                table: "Verbs",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityProfiles");

            migrationBuilder.DropTable(
                name: "ActivityStates");

            migrationBuilder.DropTable(
                name: "AgentProfiles");

            migrationBuilder.DropTable(
                name: "AttachmentEntity");

            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropTable(
                name: "SubStatements");

            migrationBuilder.DropTable(
                name: "Contexts");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Verbs");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "StatementRefEntity");

            migrationBuilder.DropTable(
                name: "ContextActivities");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "ActivityDefinitions");

            migrationBuilder.DropTable(
                name: "AgentAccounts");

            migrationBuilder.DropTable(
                name: "InteractionActivities");
        }
    }
}
