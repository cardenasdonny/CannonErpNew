﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pedido.Entities.DbContexts;

#nullable disable

namespace Pedido.Entities.Migrations
{
    [DbContext(typeof(RepositoryContextFactory))]
    partial class RepositoryContextFactoryModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Pedido")
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Pedido.Entities.Pedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PedidoId"), 1L, 1);

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("MetodoPago")
                        .HasColumnType("int");

                    b.Property<int>("PedidoEstado")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PedidoId");

                    b.ToTable("Pedidos", "Pedido");
                });

            modelBuilder.Entity("Pedido.Entities.PedidoDetalle", b =>
                {
                    b.Property<int>("PedidoDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PedidoDetalleId"), 1L, 1);

                    b.Property<int>("ArticuloId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int?>("PedidoId")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PedidoDetalleId");

                    b.HasIndex("PedidoId");

                    b.ToTable("PedidoDetalles", "Pedido");
                });

            modelBuilder.Entity("Pedido.Entities.PedidoDetalle", b =>
                {
                    b.HasOne("Pedido.Entities.Pedido", null)
                        .WithMany("Items")
                        .HasForeignKey("PedidoId");
                });

            modelBuilder.Entity("Pedido.Entities.Pedido", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}