﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WowStatCards.DataAccess;

#nullable disable

namespace WowStatCards.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WowStatCards.Models.Domain.Faction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Factions");
                });

            modelBuilder.Entity("WowStatCards.Models.Domain.StatCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Agility")
                        .HasColumnType("int");

                    b.Property<int?>("Armor")
                        .HasColumnType("int");

                    b.Property<double?>("AttackPower")
                        .HasColumnType("float");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AvgItemLevel")
                        .HasColumnType("int");

                    b.Property<int?>("BonusArmor")
                        .HasColumnType("int");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FactionId")
                        .HasColumnType("int");

                    b.Property<int?>("Health")
                        .HasColumnType("int");

                    b.Property<int?>("Intellect")
                        .HasColumnType("int");

                    b.Property<double?>("Lifesteal")
                        .HasColumnType("float");

                    b.Property<double?>("MainHandDamageMax")
                        .HasColumnType("float");

                    b.Property<double?>("MainHandDamageMin")
                        .HasColumnType("float");

                    b.Property<double?>("MainHandDps")
                        .HasColumnType("float");

                    b.Property<double?>("MainHandSpeed")
                        .HasColumnType("float");

                    b.Property<double?>("Mastery")
                        .HasColumnType("float");

                    b.Property<double?>("MeleeCrit")
                        .HasColumnType("float");

                    b.Property<double?>("MeleeHaste")
                        .HasColumnType("float");

                    b.Property<double?>("OffHandDamageMax")
                        .HasColumnType("float");

                    b.Property<double?>("OffHandDamageMin")
                        .HasColumnType("float");

                    b.Property<double?>("OffHandDps")
                        .HasColumnType("float");

                    b.Property<double?>("OffHandSpeed")
                        .HasColumnType("float");

                    b.Property<string>("Realm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RenderUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("SpellCrit")
                        .HasColumnType("float");

                    b.Property<int?>("SpellPower")
                        .HasColumnType("int");

                    b.Property<int?>("Stamina")
                        .HasColumnType("int");

                    b.Property<int?>("Strength")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Versatility")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FactionId");

                    b.ToTable("StatCards");
                });

            modelBuilder.Entity("WowStatCards.Models.Domain.StatCard", b =>
                {
                    b.HasOne("WowStatCards.Models.Domain.Faction", "Faction")
                        .WithMany()
                        .HasForeignKey("FactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faction");
                });
#pragma warning restore 612, 618
        }
    }
}
