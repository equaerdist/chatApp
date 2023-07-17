﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication5.Services;

#nullable disable

namespace WebApplication5.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication5.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastMessageTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("WebApplication5.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("WebApplication5.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApplication5.Models.UserSettingsForGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("ActiveNotifications")
                        .HasColumnType("boolean");

                    b.Property<bool>("ActiveSound")
                        .HasColumnType("boolean");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSettingsForGroup");
                });

            modelBuilder.Entity("WebApplication5.Models.UsersGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersGroups");
                });

            modelBuilder.Entity("WebApplication5.Models.Group", b =>
                {
                    b.OwnsOne("WebApplication5.Models.GroupSettings", "Settings", b1 =>
                        {
                            b1.Property<int>("GroupId")
                                .HasColumnType("integer");

                            b1.Property<bool>("IsPrivate")
                                .HasColumnType("boolean");

                            b1.Property<int>("MaxUsersAmount")
                                .HasColumnType("integer");

                            b1.Property<bool>("MessagesOnlyForAdmins")
                                .HasColumnType("boolean");

                            b1.HasKey("GroupId");

                            b1.ToTable("Groups");

                            b1.WithOwner()
                                .HasForeignKey("GroupId");
                        });

                    b.Navigation("Settings")
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication5.Models.Message", b =>
                {
                    b.HasOne("WebApplication5.Models.Group", "CreatingGroup")
                        .WithMany("Messages")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication5.Models.User", "Creator")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatingGroup");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("WebApplication5.Models.UserSettingsForGroup", b =>
                {
                    b.HasOne("WebApplication5.Models.Group", "Group")
                        .WithMany("UserSettings")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication5.Models.User", "User")
                        .WithMany("GroupSettings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication5.Models.UsersGroup", b =>
                {
                    b.HasOne("WebApplication5.Models.Group", "Group")
                        .WithMany("UsersGroup")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication5.Models.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication5.Models.Group", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserSettings");

                    b.Navigation("UsersGroup");
                });

            modelBuilder.Entity("WebApplication5.Models.User", b =>
                {
                    b.Navigation("GroupSettings");

                    b.Navigation("Messages");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
