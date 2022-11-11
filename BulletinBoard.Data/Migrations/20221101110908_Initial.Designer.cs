﻿// <auto-generated />
using System;
using BulletinBoard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BulletinBoard.Data.Migrations
{
    [DbContext(typeof(BulletinBoardDbContext))]
    [Migration("20221101110908_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("BulletinBoard.Data.Model.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommentPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecordId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CommentId");

                    b.HasIndex("RecordId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BulletinBoard.Data.Model.Record", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RecordPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RecordText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RecordTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RecordId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("BulletinBoard.Data.Model.Comment", b =>
                {
                    b.HasOne("BulletinBoard.Data.Model.Record", "Record")
                        .WithMany("Comments")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Record");
                });

            modelBuilder.Entity("BulletinBoard.Data.Model.Record", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
