﻿// <auto-generated />
using Forest.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Forest.Migrations
{
    [DbContext(typeof(ChallengesContext))]
    [Migration("20220412102345_AddAnswer")]
    partial class AddAnswer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("Forest.Data.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChallengeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AnswerId");

                    b.HasIndex("ChallengeId")
                        .IsUnique();

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Forest.Data.Models.Challenge", b =>
                {
                    b.Property<int>("ChallengeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.HasKey("ChallengeId");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("Forest.Data.Models.ChallengeAttachment", b =>
                {
                    b.Property<int>("ChallengeAttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChallengeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ChallengeAttachmentId");

                    b.HasIndex("ChallengeId");

                    b.ToTable("ChallengeAttachments");
                });

            modelBuilder.Entity("Forest.Data.Models.Answer", b =>
                {
                    b.HasOne("Forest.Data.Models.Challenge", "Challenge")
                        .WithOne("Answer")
                        .HasForeignKey("Forest.Data.Models.Answer", "ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Challenge");
                });

            modelBuilder.Entity("Forest.Data.Models.ChallengeAttachment", b =>
                {
                    b.HasOne("Forest.Data.Models.Challenge", "Challenge")
                        .WithMany("Attachments")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Challenge");
                });

            modelBuilder.Entity("Forest.Data.Models.Challenge", b =>
                {
                    b.Navigation("Answer")
                        .IsRequired();

                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}
