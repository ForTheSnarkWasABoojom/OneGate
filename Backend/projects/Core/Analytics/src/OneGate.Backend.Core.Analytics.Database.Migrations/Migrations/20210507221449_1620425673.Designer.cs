﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OneGate.Backend.Core.Analytics.Database;

namespace OneGate.Backend.Core.Analytics.Database.Migrations.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210507221449_1620425673")]
    partial class _1620425673
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");
#pragma warning restore 612, 618
        }
    }
}
