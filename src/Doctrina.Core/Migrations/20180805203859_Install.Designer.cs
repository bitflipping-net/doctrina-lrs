﻿// <auto-generated />
using System;
using Doctrina.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doctrina.Core.Migrations
{
    [DbContext(typeof(DoctrinaContext))]
    [Migration("20180805203859_Install")]
    partial class Install
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Doctrina.Core.Data.ActivityEntity", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityId")
                        .IsRequired();

                    b.Property<Guid?>("AuthorityId");

                    b.Property<string>("CanonicalData")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.HasIndex("ActivityId");

                    b.HasIndex("AuthorityId")
                        .IsUnique()
                        .HasFilter("[AuthorityId] IS NOT NULL");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Doctrina.Core.Data.AgentEntity", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account_HomePage")
                        .HasMaxLength(2083);

                    b.Property<string>("Account_Name")
                        .HasMaxLength(50);

                    b.Property<string>("Mbox")
                        .HasMaxLength(128);

                    b.Property<string>("Mbox_SHA1SUM")
                        .HasMaxLength(40);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("OauthIdentifier")
                        .HasMaxLength(192);

                    b.Property<int>("ObjectType")
                        .HasMaxLength(6);

                    b.Property<string>("OpenId")
                        .HasMaxLength(2083);

                    b.HasKey("Key");

                    b.HasIndex("Mbox")
                        .IsUnique()
                        .HasFilter("[Mbox] IS NOT NULL");

                    b.HasIndex("Mbox_SHA1SUM")
                        .IsUnique()
                        .HasFilter("[Mbox_SHA1SUM] IS NOT NULL");

                    b.HasIndex("OpenId")
                        .IsUnique()
                        .HasFilter("[OpenId] IS NOT NULL");

                    b.HasIndex("Account_HomePage", "Account_Name")
                        .IsUnique()
                        .HasFilter("[Account_HomePage] IS NOT NULL AND [Account_Name] IS NOT NULL");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("Doctrina.Core.Data.AttachmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CanonicalData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Content");

                    b.Property<string>("ContentType")
                        .HasMaxLength(255);

                    b.Property<string>("Payload")
                        .HasMaxLength(150);

                    b.Property<Guid>("StatementId");

                    b.HasKey("Id");

                    b.HasIndex("StatementId");

                    b.ToTable("AttachmentEntity");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextActivitiesCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityId");

                    b.Property<Guid?>("ContextActivitiesEntityKey");

                    b.Property<Guid>("ContextId");

                    b.HasKey("Id");

                    b.HasIndex("ContextActivitiesEntityKey");

                    b.ToTable("ContextActivitiesCategory");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextActivitiesEntity", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Key");

                    b.ToTable("ContextActivities");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextActivitiesGrouping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityId");

                    b.Property<Guid?>("ContextActivitiesEntityKey");

                    b.Property<Guid>("ContextId");

                    b.HasKey("Id");

                    b.HasIndex("ContextActivitiesEntityKey");

                    b.ToTable("ContextActivitiesGrouping");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextActivitiesOther", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityId");

                    b.Property<Guid?>("ContextActivitiesEntityKey");

                    b.Property<Guid>("ContextId");

                    b.HasKey("Id");

                    b.HasIndex("ContextActivitiesEntityKey");

                    b.ToTable("ContextActivitiesOther");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextActivitiesParent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityId");

                    b.Property<Guid?>("ContextActivitiesEntityKey");

                    b.Property<Guid>("ContextId");

                    b.HasKey("Id");

                    b.HasIndex("ContextActivitiesEntityKey");

                    b.ToTable("ContextActivitiesParent");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextEntity", b =>
                {
                    b.Property<Guid>("ContextId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ContextActivitiesId");

                    b.Property<string>("Extensions");

                    b.Property<Guid?>("InstructorId");

                    b.Property<string>("Language");

                    b.Property<string>("Platform");

                    b.Property<Guid?>("Registration");

                    b.Property<string>("Revision");

                    b.Property<Guid?>("StatementId");

                    b.Property<Guid?>("TeamId");

                    b.HasKey("ContextId");

                    b.HasIndex("ContextActivitiesId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("TeamId");

                    b.ToTable("StatementContexts");
                });

            modelBuilder.Entity("Doctrina.Core.Data.DoctrinaUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Doctrina.Core.Data.Documents.ActivityProfileEntity", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActivityKey")
                        .HasMaxLength(2083);

                    b.Property<Guid>("AgentId");

                    b.Property<Guid>("DocumentId");

                    b.Property<string>("ProfileId")
                        .IsRequired()
                        .HasMaxLength(2083);

                    b.Property<Guid?>("RegistrationId");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Key");

                    b.HasIndex("ActivityKey");

                    b.HasIndex("AgentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("ActivityProfiles");
                });

            modelBuilder.Entity("Doctrina.Core.Data.Documents.ActivityStateEntity", b =>
                {
                    b.Property<Guid>("ActivityStateId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActivityKey")
                        .HasMaxLength(2083);

                    b.Property<Guid>("AgentId");

                    b.Property<Guid>("DocumentId");

                    b.Property<Guid?>("RegistrationId");

                    b.Property<string>("StateId")
                        .IsRequired()
                        .HasMaxLength(2083);

                    b.HasKey("ActivityStateId");

                    b.HasIndex("ActivityKey");

                    b.HasIndex("AgentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("ActivityStates");
                });

            modelBuilder.Entity("Doctrina.Core.Data.Documents.AgentProfileEntity", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AgentId");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<Guid>("DocumentId");

                    b.Property<string>("ETag")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ProfileId")
                        .IsRequired()
                        .HasMaxLength(2083);

                    b.Property<DateTime>("Updated");

                    b.HasKey("Key");

                    b.HasIndex("AgentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("AgentProfiles");
                });

            modelBuilder.Entity("Doctrina.Core.Data.Documents.DocumentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content");

                    b.Property<string>("ContentType")
                        .HasMaxLength(255);

                    b.Property<string>("ETag")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Doctrina.Core.Data.GroupMemberEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("AgentEntityKey");

                    b.Property<Guid>("GroupId");

                    b.Property<Guid>("MemberId");

                    b.HasKey("Id");

                    b.HasIndex("AgentEntityKey");

                    b.ToTable("GroupMembers");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ResultEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Completion");

                    b.Property<TimeSpan?>("Duration");

                    b.Property<string>("Extensions")
                        .HasColumnType("ntext");

                    b.Property<string>("Response");

                    b.Property<double?>("ScoreMax");

                    b.Property<double?>("ScoreMin");

                    b.Property<double?>("ScoreRaw");

                    b.Property<double?>("ScoreScaled");

                    b.Property<bool?>("Success");

                    b.HasKey("Id");

                    b.ToTable("ResultEntity");
                });

            modelBuilder.Entity("Doctrina.Core.Data.StatementEntity", b =>
                {
                    b.Property<Guid>("StatementId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActorKey");

                    b.Property<Guid?>("AuthorityId");

                    b.Property<Guid?>("ContextId");

                    b.Property<string>("FullStatement")
                        .HasColumnType("ntext");

                    b.Property<Guid?>("ObjectActivityKey");

                    b.Property<Guid?>("ObjectAgentKey");

                    b.Property<Guid?>("ObjectStatementRefId");

                    b.Property<Guid?>("ObjectSubStatementId");

                    b.Property<Guid?>("ResultId");

                    b.Property<DateTime>("Stored");

                    b.Property<DateTime>("Timestamp");

                    b.Property<Guid?>("User");

                    b.Property<Guid>("VerbKey");

                    b.Property<string>("Version")
                        .HasMaxLength(7);

                    b.Property<bool>("Voided");

                    b.HasKey("StatementId");

                    b.HasIndex("ActorKey");

                    b.HasIndex("AuthorityId");

                    b.HasIndex("ContextId");

                    b.HasIndex("ObjectActivityKey");

                    b.HasIndex("ObjectAgentKey");

                    b.HasIndex("ObjectSubStatementId");

                    b.HasIndex("ResultId");

                    b.HasIndex("VerbKey");

                    b.ToTable("Statements");
                });

            modelBuilder.Entity("Doctrina.Core.Data.SubStatementEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActorKey");

                    b.Property<Guid?>("ContextId");

                    b.Property<Guid?>("ObjectActivityKey");

                    b.Property<Guid?>("ObjectAgentKey");

                    b.Property<Guid?>("ObjectStatementRefId");

                    b.Property<int>("ObjectType");

                    b.Property<Guid?>("ResultId");

                    b.Property<DateTime?>("Timestamp");

                    b.Property<Guid>("VerbKey");

                    b.HasKey("Id");

                    b.HasIndex("ActorKey");

                    b.HasIndex("ContextId");

                    b.HasIndex("ObjectActivityKey");

                    b.HasIndex("ObjectAgentKey");

                    b.HasIndex("ResultId");

                    b.HasIndex("VerbKey");

                    b.ToTable("SubStatements");
                });

            modelBuilder.Entity("Doctrina.Core.Data.VerbEntity", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CanonicalData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id");

                    b.HasKey("Key");

                    b.ToTable("Verbs");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ActivityEntity", b =>
                {
                    b.HasOne("Doctrina.Core.Data.AgentEntity", "Authority")
                        .WithOne()
                        .HasForeignKey("Doctrina.Core.Data.ActivityEntity", "AuthorityId");
                });

            modelBuilder.Entity("Doctrina.Core.Data.AttachmentEntity", b =>
                {
                    b.HasOne("Doctrina.Core.Data.StatementEntity", "Statement")
                        .WithMany("Attachments")
                        .HasForeignKey("StatementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextActivitiesCategory", b =>
                {
                    b.HasOne("Doctrina.Core.Data.ContextActivitiesEntity")
                        .WithMany("Category")
                        .HasForeignKey("ContextActivitiesEntityKey");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextActivitiesGrouping", b =>
                {
                    b.HasOne("Doctrina.Core.Data.ContextActivitiesEntity")
                        .WithMany("Grouping")
                        .HasForeignKey("ContextActivitiesEntityKey");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextActivitiesOther", b =>
                {
                    b.HasOne("Doctrina.Core.Data.ContextActivitiesEntity")
                        .WithMany("Other")
                        .HasForeignKey("ContextActivitiesEntityKey");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextActivitiesParent", b =>
                {
                    b.HasOne("Doctrina.Core.Data.ContextActivitiesEntity")
                        .WithMany("Parent")
                        .HasForeignKey("ContextActivitiesEntityKey");
                });

            modelBuilder.Entity("Doctrina.Core.Data.ContextEntity", b =>
                {
                    b.HasOne("Doctrina.Core.Data.ContextActivitiesEntity", "ContextActivities")
                        .WithMany()
                        .HasForeignKey("ContextActivitiesId");

                    b.HasOne("Doctrina.Core.Data.AgentEntity", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId");

                    b.HasOne("Doctrina.Core.Data.AgentEntity", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Doctrina.Core.Data.Documents.ActivityProfileEntity", b =>
                {
                    b.HasOne("Doctrina.Core.Data.ActivityEntity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doctrina.Core.Data.AgentEntity", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doctrina.Core.Data.Documents.DocumentEntity", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Doctrina.Core.Data.Documents.ActivityStateEntity", b =>
                {
                    b.HasOne("Doctrina.Core.Data.ActivityEntity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doctrina.Core.Data.AgentEntity", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doctrina.Core.Data.Documents.DocumentEntity", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Doctrina.Core.Data.Documents.AgentProfileEntity", b =>
                {
                    b.HasOne("Doctrina.Core.Data.AgentEntity", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doctrina.Core.Data.Documents.DocumentEntity", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Doctrina.Core.Data.GroupMemberEntity", b =>
                {
                    b.HasOne("Doctrina.Core.Data.AgentEntity")
                        .WithMany("Members")
                        .HasForeignKey("AgentEntityKey");
                });

            modelBuilder.Entity("Doctrina.Core.Data.StatementEntity", b =>
                {
                    b.HasOne("Doctrina.Core.Data.AgentEntity", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doctrina.Core.Data.AgentEntity", "Authority")
                        .WithMany()
                        .HasForeignKey("AuthorityId");

                    b.HasOne("Doctrina.Core.Data.ContextEntity", "Context")
                        .WithMany()
                        .HasForeignKey("ContextId");

                    b.HasOne("Doctrina.Core.Data.ActivityEntity", "ObjectActivity")
                        .WithMany()
                        .HasForeignKey("ObjectActivityKey");

                    b.HasOne("Doctrina.Core.Data.AgentEntity", "ObjectAgent")
                        .WithMany()
                        .HasForeignKey("ObjectAgentKey");

                    b.HasOne("Doctrina.Core.Data.SubStatementEntity", "ObjectSubStatement")
                        .WithMany()
                        .HasForeignKey("ObjectSubStatementId");

                    b.HasOne("Doctrina.Core.Data.ResultEntity", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId");

                    b.HasOne("Doctrina.Core.Data.VerbEntity", "Verb")
                        .WithMany()
                        .HasForeignKey("VerbKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Doctrina.Core.Data.SubStatementEntity", b =>
                {
                    b.HasOne("Doctrina.Core.Data.AgentEntity", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doctrina.Core.Data.ContextEntity", "Context")
                        .WithMany()
                        .HasForeignKey("ContextId");

                    b.HasOne("Doctrina.Core.Data.ActivityEntity", "ObjectActivity")
                        .WithMany()
                        .HasForeignKey("ObjectActivityKey");

                    b.HasOne("Doctrina.Core.Data.AgentEntity", "ObjectAgent")
                        .WithMany()
                        .HasForeignKey("ObjectAgentKey");

                    b.HasOne("Doctrina.Core.Data.ResultEntity", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId");

                    b.HasOne("Doctrina.Core.Data.VerbEntity", "Verb")
                        .WithMany()
                        .HasForeignKey("VerbKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}