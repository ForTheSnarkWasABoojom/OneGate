﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OneGate.Backend.Core.Users.Database;

namespace OneGate.Backend.Core.Users.Database.Migrations.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210507221439_1620425673")]
    partial class _1620425673
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("OneGate.Backend.Core.Users.Database.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("account");
                });

            modelBuilder.Entity("OneGate.Backend.Core.Users.Database.Models.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("administrator");
                });

            modelBuilder.Entity("OneGate.Backend.Core.Users.Database.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("AssetId")
                        .HasColumnType("integer")
                        .HasColumnName("asset_id");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("owner_id");

                    b.Property<float>("Quantity")
                        .HasColumnType("real")
                        .HasColumnName("quantity");

                    b.Property<string>("Side")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("side");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("order");

                    b.HasDiscriminator<string>("Type").HasValue("Order");
                });

            modelBuilder.Entity("OneGate.Backend.Core.Users.Database.Models.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("owner_id");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("portfolio");
                });

            modelBuilder.Entity("OneGate.Backend.Core.Users.Database.Models.LimitOrder", b =>
                {
                    b.HasBaseType("OneGate.Backend.Core.Users.Database.Models.Order");

                    b.Property<float>("Price")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("real")
                        .HasColumnName("price");

                    b.ToTable("order");

                    b.HasDiscriminator().HasValue("LIMIT");
                });

            modelBuilder.Entity("OneGate.Backend.Core.Users.Database.Models.MarketOrder", b =>
                {
                    b.HasBaseType("OneGate.Backend.Core.Users.Database.Models.Order");

                    b.ToTable("order");

                    b.HasDiscriminator().HasValue("MARKET");
                });

            modelBuilder.Entity("OneGate.Backend.Core.Users.Database.Models.StopOrder", b =>
                {
                    b.HasBaseType("OneGate.Backend.Core.Users.Database.Models.Order");

                    b.Property<float>("Price")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("real")
                        .HasColumnName("price");

                    b.ToTable("order");

                    b.HasDiscriminator().HasValue("STOP");
                });

            modelBuilder.Entity("OneGate.Backend.Core.Users.Database.Models.Order", b =>
                {
                    b.HasOne("OneGate.Backend.Core.Users.Database.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("OneGate.Backend.Core.Users.Database.Models.Portfolio", b =>
                {
                    b.HasOne("OneGate.Backend.Core.Users.Database.Models.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });
#pragma warning restore 612, 618
        }
    }
}
