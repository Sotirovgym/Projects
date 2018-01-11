﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TeamBuilder.Data;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.Data.Migrations
{
    [DbContext(typeof(TeamBuilderDbContext))]
    [Migration("20171229174620_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamBuilder.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TeamBuilder.Models.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InvitedUserId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("InvitedUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("TeamBuilder.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Acronym")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasIndex("CreatorId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TeamBuilder.Models.TeamEvent", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<int>("TeamId");

                    b.HasKey("EventId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("EventTeams");
                });

            modelBuilder.Entity("TeamBuilder.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("FirstName")
                        .HasMaxLength(25);

                    b.Property<int>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .HasMaxLength(25);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasAlternateKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamBuilder.Models.UserTeam", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("UserId");

                    b.HasKey("TeamId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTeams");
                });

            modelBuilder.Entity("TeamBuilder.Models.Event", b =>
                {
                    b.HasOne("TeamBuilder.Models.User", "Creator")
                        .WithMany("CreatedEvents")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamBuilder.Models.Invitation", b =>
                {
                    b.HasOne("TeamBuilder.Models.User", "InvitedUser")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("InvitedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TeamBuilder.Models.Team", "Team")
                        .WithMany("Invitations")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TeamBuilder.Models.Team", b =>
                {
                    b.HasOne("TeamBuilder.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamBuilder.Models.TeamEvent", b =>
                {
                    b.HasOne("TeamBuilder.Models.Event", "Event")
                        .WithMany("EventTeams")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TeamBuilder.Models.Team", "Team")
                        .WithMany("EventTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TeamBuilder.Models.UserTeam", b =>
                {
                    b.HasOne("TeamBuilder.Models.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TeamBuilder.Models.User", "User")
                        .WithMany("CreatedUserTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
