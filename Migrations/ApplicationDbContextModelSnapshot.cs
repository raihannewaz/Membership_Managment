﻿// <auto-generated />
using System;
using Membership_Managment.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Membership_Managment.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Membership_Managment.Models.Document", b =>
                {
                    b.Property<int>("DocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocId"));

                    b.Property<DateTime?>("ActionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ActionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("DocId");

                    b.HasIndex("MemberId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Membership_Managment.Models.DuePayment", b =>
                {
                    b.Property<int>("DuePaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DuePaymentID"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberPackageID")
                        .HasColumnType("int");

                    b.HasKey("DuePaymentID");

                    b.HasIndex("MemberPackageID");

                    b.ToTable("DuePayments");
                });

            modelBuilder.Entity("Membership_Managment.Models.FeeCollection", b =>
                {
                    b.Property<int>("CollectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollectionId"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("CollectionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CollectionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("CollectionId");

                    b.HasIndex("MemberId");

                    b.ToTable("FeeCollections");
                });

            modelBuilder.Entity("Membership_Managment.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<DateTime?>("ActionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ActionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ActivaitonDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal?>("MembershipAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermanentAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Phone")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PresentAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemberId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Membership_Managment.Models.MemberPackage", b =>
                {
                    b.Property<int>("MemberPackageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberPackageID"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MemberID")
                        .HasColumnType("int");

                    b.Property<int>("PackageID")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MemberPackageID");

                    b.HasIndex("MemberID");

                    b.HasIndex("PackageID");

                    b.ToTable("MemberPackages");
                });

            modelBuilder.Entity("Membership_Managment.Models.Package", b =>
                {
                    b.Property<int>("PackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageId"));

                    b.Property<int?>("Duration")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PackageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PackagePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PackageId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Membership_Managment.Models.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"));

                    b.Property<int>("AdvancePaymentDuration")
                        .HasColumnType("int");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MemberPackageID")
                        .HasColumnType("int");

                    b.Property<bool?>("PaidInAdvance")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentID");

                    b.HasIndex("MemberPackageID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Membership_Managment.Models.Document", b =>
                {
                    b.HasOne("Membership_Managment.Models.Member", "Member")
                        .WithMany("DocumentList")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Membership_Managment.Models.DuePayment", b =>
                {
                    b.HasOne("Membership_Managment.Models.MemberPackage", "MemberPackage")
                        .WithMany("DuePayment")
                        .HasForeignKey("MemberPackageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MemberPackage");
                });

            modelBuilder.Entity("Membership_Managment.Models.FeeCollection", b =>
                {
                    b.HasOne("Membership_Managment.Models.Member", "Member")
                        .WithMany("FeeCollection")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Membership_Managment.Models.MemberPackage", b =>
                {
                    b.HasOne("Membership_Managment.Models.Member", "Member")
                        .WithMany("MemberPackage")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership_Managment.Models.Package", "Package")
                        .WithMany("MemberPackage")
                        .HasForeignKey("PackageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("Membership_Managment.Models.Payment", b =>
                {
                    b.HasOne("Membership_Managment.Models.MemberPackage", "MemberPackage")
                        .WithMany("Payment")
                        .HasForeignKey("MemberPackageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MemberPackage");
                });

            modelBuilder.Entity("Membership_Managment.Models.Member", b =>
                {
                    b.Navigation("DocumentList");

                    b.Navigation("FeeCollection");

                    b.Navigation("MemberPackage");
                });

            modelBuilder.Entity("Membership_Managment.Models.MemberPackage", b =>
                {
                    b.Navigation("DuePayment");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("Membership_Managment.Models.Package", b =>
                {
                    b.Navigation("MemberPackage");
                });
#pragma warning restore 612, 618
        }
    }
}
