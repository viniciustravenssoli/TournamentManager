﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TournamentAPI.Data;

#nullable disable

namespace TournamentAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240707053156_Adding_User")]
    partial class Adding_User
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("TournamentAPI.Models.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Participant1Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Participant2Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Round")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("WinnerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MatchId");

                    b.HasIndex("Participant1Id");

                    b.HasIndex("Participant2Id");

                    b.HasIndex("TournamentId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("TournamentAPI.Models.Participant", b =>
                {
                    b.Property<int>("ParticipantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ParticipantId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("TournamentAPI.Models.Tournament", b =>
                {
                    b.Property<int>("TournamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TournamentId");

                    b.HasIndex("UserId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("TournamentAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TournamentAPI.Models.Match", b =>
                {
                    b.HasOne("TournamentAPI.Models.Participant", "Participant1")
                        .WithMany()
                        .HasForeignKey("Participant1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TournamentAPI.Models.Participant", "Participant2")
                        .WithMany()
                        .HasForeignKey("Participant2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TournamentAPI.Models.Tournament", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TournamentAPI.Models.Participant", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");

                    b.Navigation("Participant1");

                    b.Navigation("Participant2");

                    b.Navigation("Tournament");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("TournamentAPI.Models.Participant", b =>
                {
                    b.HasOne("TournamentAPI.Models.Tournament", null)
                        .WithMany("Participants")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("TournamentAPI.Models.Tournament", b =>
                {
                    b.HasOne("TournamentAPI.Models.User", "User")
                        .WithMany("Tournaments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TournamentAPI.Models.Tournament", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("TournamentAPI.Models.User", b =>
                {
                    b.Navigation("Tournaments");
                });
#pragma warning restore 612, 618
        }
    }
}
