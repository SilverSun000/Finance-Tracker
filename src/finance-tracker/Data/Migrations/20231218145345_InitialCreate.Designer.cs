﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace finance_tracker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231218145345_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.14");

            modelBuilder.Entity(".Tree<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RootId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RootId");

                    b.ToTable("Tree<string>");
                });

            modelBuilder.Entity(".TreeNode<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("CategoryTrees");
                });

            modelBuilder.Entity("Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TreeNode<string>Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TreeNode<string>Id");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryTreeId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryTreeId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity(".Tree<string>", b =>
                {
                    b.HasOne(".TreeNode<string>", "Root")
                        .WithMany()
                        .HasForeignKey("RootId");

                    b.Navigation("Root");
                });

            modelBuilder.Entity(".TreeNode<string>", b =>
                {
                    b.HasOne(".TreeNode<string>", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Transaction", b =>
                {
                    b.HasOne(".TreeNode<string>", null)
                        .WithMany("Transactions")
                        .HasForeignKey("TreeNode<string>Id");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.HasOne(".Tree<string>", "CategoryTree")
                        .WithOne()
                        .HasForeignKey("User", "CategoryTreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryTree");
                });

            modelBuilder.Entity(".TreeNode<string>", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}