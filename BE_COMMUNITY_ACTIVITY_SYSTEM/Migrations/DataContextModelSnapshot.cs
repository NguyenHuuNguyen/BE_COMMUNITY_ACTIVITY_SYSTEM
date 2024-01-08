﻿// <auto-generated />
using System;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Announcement", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Class", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AcademicYear")
                        .HasColumnType("int");

                    b.Property<string>("ClassPresidentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadTeacherId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MajorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClassPresidentId");

                    b.HasIndex("HeadTeacherId");

                    b.HasIndex("MajorId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.CommunityActivity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActivityTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdminNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClassPresidentEvaluationScore")
                        .HasColumnType("int");

                    b.Property<string>("ClassPresidentNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EvidentLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadTeacherNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MajorHeadNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SelfEvaluationScore")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StudentNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("CommunityActivities");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.CommunityActivityType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MaxScore")
                        .HasColumnType("int");

                    b.Property<int>("MinScore")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CommunityActivityTypes");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Major", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MajorHeadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MajorHeadId");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleName")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.RoleUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("RoleUsers");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Setting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ethnic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("IdentificationCardId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IdentificationCardIssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentificationCardIssuePlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Religion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ward")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Class", b =>
                {
                    b.HasOne("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.User", "ClassPresident")
                        .WithMany()
                        .HasForeignKey("ClassPresidentId");

                    b.HasOne("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.User", "HeadTeacher")
                        .WithMany()
                        .HasForeignKey("HeadTeacherId");

                    b.HasOne("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Major", "Major")
                        .WithMany("Classes")
                        .HasForeignKey("MajorId");

                    b.Navigation("ClassPresident");

                    b.Navigation("HeadTeacher");

                    b.Navigation("Major");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.CommunityActivity", b =>
                {
                    b.HasOne("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.CommunityActivityType", "CommunityActivityType")
                        .WithMany("CommunityActivities")
                        .HasForeignKey("ActivityTypeId");

                    b.HasOne("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.User", "User")
                        .WithMany("CommunityActivities")
                        .HasForeignKey("UserId");

                    b.Navigation("CommunityActivityType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Major", b =>
                {
                    b.HasOne("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.User", "MajorHead")
                        .WithMany()
                        .HasForeignKey("MajorHeadId");

                    b.Navigation("MajorHead");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.RoleUser", b =>
                {
                    b.HasOne("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId");

                    b.HasOne("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.User", "User")
                        .WithMany("RoleUsers")
                        .HasForeignKey("UserId");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.User", b =>
                {
                    b.HasOne("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Class", "Class")
                        .WithMany("Users")
                        .HasForeignKey("ClassId");

                    b.Navigation("Class");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Class", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.CommunityActivityType", b =>
                {
                    b.Navigation("CommunityActivities");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Major", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.Role", b =>
                {
                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("BE_COMMUNITY_ACTIVITY_SYSTEM.Models.User", b =>
                {
                    b.Navigation("CommunityActivities");

                    b.Navigation("RoleUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
