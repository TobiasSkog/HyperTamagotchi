﻿// <auto-generated />
using System;
using HyperTamagotchi_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HyperTamagotchi_MVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240516124401_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ExpectedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ShippingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderDate");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingCartId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("ShoppingCartId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.ShoppingItem", b =>
                {
                    b.Property<int>("ShoppingItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingItemId"));

                    b.Property<string>("CurrencyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<byte?>("Quantity")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Stock")
                        .HasColumnType("tinyint");

                    b.HasKey("ShoppingItemId");

                    b.ToTable("ShoppingItems");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ShoppingItem");

                    b.UseTphMappingStrategy();

                    b.HasData(
                        new
                        {
                            ShoppingItemId = 1,
                            CurrencyType = "SEK",
                            Description = "Restores 10 energy to your Tamagotchi",
                            Discount = 1f,
                            ImagePath = "none.png",
                            Name = "Banana",
                            Price = 25f,
                            Stock = (byte)50
                        },
                        new
                        {
                            ShoppingItemId = 2,
                            CurrencyType = "SEK",
                            Description = "Restores 25 energy to your Tamagotchi",
                            Discount = 1f,
                            ImagePath = "none.png",
                            Name = "Sports Drank",
                            Price = 50f,
                            Stock = (byte)25
                        },
                        new
                        {
                            ShoppingItemId = 3,
                            CurrencyType = "SEK",
                            Description = "Restores 1 energy to your Tamagotchi",
                            Discount = 1f,
                            ImagePath = "none.png",
                            Name = "Rice",
                            Price = 10f,
                            Stock = (byte)250
                        });
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.ShoppingItemOrder", b =>
                {
                    b.Property<int>("ShoppingItemId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("ShoppingItemId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("ShoppingItemOrders");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.ShoppingItemShoppingCart", b =>
                {
                    b.Property<int>("ShoppingItemId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasKey("ShoppingItemId", "ShoppingCartId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingItemShoppingCarts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.Tamagotchi", b =>
                {
                    b.HasBaseType("HyperTamagotchi_MVC.Models.ShoppingItem");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte>("Experience")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Mood")
                        .HasColumnType("tinyint");

                    b.Property<byte>("TamagotchiColor")
                        .HasColumnType("tinyint");

                    b.Property<byte>("TamagotchiStage")
                        .HasColumnType("tinyint");

                    b.Property<byte>("TamagotchiType")
                        .HasColumnType("tinyint");

                    b.HasIndex("CustomerId");

                    b.HasDiscriminator().HasValue("Tamagotchi");

                    b.HasData(
                        new
                        {
                            ShoppingItemId = 4,
                            CurrencyType = "SEK",
                            Description = "Meet the young developer Darin",
                            Discount = 1f,
                            ImagePath = "Assets/Tamagotchi/Developer/Dev_Egg_Default.png",
                            Name = "Developer Darin",
                            Price = 200f,
                            Stock = (byte)10,
                            Experience = (byte)0,
                            Mood = (byte)1,
                            TamagotchiColor = (byte)1,
                            TamagotchiStage = (byte)1,
                            TamagotchiType = (byte)3
                        },
                        new
                        {
                            ShoppingItemId = 5,
                            CurrencyType = "SEK",
                            Description = "Meet the senior developer Juaaaahhhn",
                            Discount = 1f,
                            ImagePath = "Assets/Tamagotchi/Developer/Dev_Child_Green.png",
                            Name = "Developer Juaaaahhhn",
                            Price = 255f,
                            Stock = (byte)2,
                            Experience = (byte)50,
                            Mood = (byte)5,
                            TamagotchiColor = (byte)3,
                            TamagotchiStage = (byte)2,
                            TamagotchiType = (byte)3
                        },
                        new
                        {
                            ShoppingItemId = 6,
                            CurrencyType = "SEK",
                            Description = "Meet the farmer Shaarraaa",
                            Discount = 1f,
                            ImagePath = "Assets/Tamagotchi/Farmer/Farmer_Child_Blue.png",
                            Name = "Farmer Shaarraaa",
                            Price = 200f,
                            Stock = (byte)6,
                            Experience = (byte)50,
                            Mood = (byte)1,
                            TamagotchiColor = (byte)4,
                            TamagotchiStage = (byte)2,
                            TamagotchiType = (byte)2
                        },
                        new
                        {
                            ShoppingItemId = 7,
                            CurrencyType = "SEK",
                            Description = "Meet the farmer Ghäärryyy",
                            Discount = 1f,
                            ImagePath = "Assets/Tamagotchi/Farmer/Farmer_Egg_Red.png",
                            Name = "Farmer Ghäärryyy",
                            Price = 50f,
                            Stock = (byte)3,
                            Experience = (byte)0,
                            Mood = (byte)2,
                            TamagotchiColor = (byte)2,
                            TamagotchiStage = (byte)1,
                            TamagotchiType = (byte)2
                        });
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.Customer", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasIndex("AddressId");

                    b.HasIndex("ShoppingCartId")
                        .IsUnique()
                        .HasFilter("[ShoppingCartId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.Order", b =>
                {
                    b.HasOne("HyperTamagotchi_MVC.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.ShoppingItemOrder", b =>
                {
                    b.HasOne("HyperTamagotchi_MVC.Models.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HyperTamagotchi_MVC.Models.ShoppingItem", "ShoppingItem")
                        .WithMany("Orders")
                        .HasForeignKey("ShoppingItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("ShoppingItem");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.ShoppingItemShoppingCart", b =>
                {
                    b.HasOne("HyperTamagotchi_MVC.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("Items")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HyperTamagotchi_MVC.Models.ShoppingItem", "ShoppingItem")
                        .WithMany("Items")
                        .HasForeignKey("ShoppingItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingCart");

                    b.Navigation("ShoppingItem");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.Tamagotchi", b =>
                {
                    b.HasOne("HyperTamagotchi_MVC.Models.Customer", null)
                        .WithMany("Tamagotchis")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.Customer", b =>
                {
                    b.HasOne("HyperTamagotchi_MVC.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HyperTamagotchi_MVC.Models.ShoppingCart", "ShoppingCart")
                        .WithOne("Customer")
                        .HasForeignKey("HyperTamagotchi_MVC.Models.Customer", "ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.ShoppingCart", b =>
                {
                    b.Navigation("Customer")
                        .IsRequired();

                    b.Navigation("Items");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.ShoppingItem", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("HyperTamagotchi_MVC.Models.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Tamagotchis");
                });
#pragma warning restore 612, 618
        }
    }
}
