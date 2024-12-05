﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241203013520_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatetimeCreate")
                        .HasColumnType("datetime2")
                        .HasColumnName("datetime_create");

                    b.Property<DateTime?>("DatetimeUpdated")
                        .HasColumnType("datetime2")
                        .HasColumnName("datetime_updated");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Function")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("function");

                    b.Property<int>("IdPermissionGroup")
                        .HasColumnType("int")
                        .HasColumnName("id_permission_group");

                    b.Property<int>("IdUserCreated")
                        .HasColumnType("int")
                        .HasColumnName("id_user_created");

                    b.Property<int?>("IdUserUpdated")
                        .HasColumnType("int")
                        .HasColumnName("id_user_updated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("password");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<bool>("StatusDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("status_deleted");

                    b.HasKey("Id");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("Domain.Entities.PermissionGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionEmployee")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("action_employee");

                    b.Property<string>("ActionPermissionGroup")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("action_permission_group");

                    b.Property<string>("ActionProduct")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("action_product");

                    b.Property<string>("ActionStockMovements")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("action_stock_movements");

                    b.Property<DateTime>("DatetimeCreate")
                        .HasColumnType("datetime2")
                        .HasColumnName("datetime_create");

                    b.Property<DateTime?>("DatetimeUpdated")
                        .HasColumnType("datetime2")
                        .HasColumnName("datetime_updated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasColumnName("description");

                    b.Property<int>("IdUserCreated")
                        .HasColumnType("int")
                        .HasColumnName("id_user_created");

                    b.Property<int?>("IdUserUpdated")
                        .HasColumnType("int")
                        .HasColumnName("id_user_updated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<bool>("StatusDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("status_deleted");

                    b.HasKey("Id");

                    b.ToTable("permissionGroups");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatetimeCreate")
                        .HasColumnType("datetime2")
                        .HasColumnName("datetime_create");

                    b.Property<DateTime?>("DatetimeUpdated")
                        .HasColumnType("datetime2")
                        .HasColumnName("datetime_updated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasColumnName("description");

                    b.Property<int>("IdUserCreated")
                        .HasColumnType("int")
                        .HasColumnName("id_user_created");

                    b.Property<int?>("IdUserUpdated")
                        .HasColumnType("int")
                        .HasColumnName("id_user_updated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<bool>("StatusDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("status_deleted");

                    b.Property<string>("UnitType")
                        .IsRequired()
                        .HasColumnType("char(2)")
                        .HasColumnName("unit_type");

                    b.HasKey("Id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Domain.Entities.StockMovements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatetimeCreate")
                        .HasColumnType("datetime2")
                        .HasColumnName("datetime_create");

                    b.Property<DateTime?>("DatetimeUpdated")
                        .HasColumnType("datetime2")
                        .HasColumnName("datetime_updated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("IdProduct")
                        .HasColumnType("int")
                        .HasColumnName("id_product");

                    b.Property<int>("IdUserCreated")
                        .HasColumnType("int")
                        .HasColumnName("id_user_created");

                    b.Property<int?>("IdUserUpdated")
                        .HasColumnType("int")
                        .HasColumnName("id_user_updated");

                    b.Property<DateTime>("MovementDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("movement_date");

                    b.Property<string>("MovementType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("movement_type");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<bool>("StatusDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("status_deleted");

                    b.HasKey("Id");

                    b.HasIndex("IdProduct");

                    b.ToTable("stocks");
                });

            modelBuilder.Entity("Domain.Entities.StockMovements", b =>
                {
                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
