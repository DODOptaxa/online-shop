﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Data.EF;

#nullable disable

namespace Store.Data.EF.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20250305095920_FullCharsSearch")]
    partial class FullCharsSearch
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Store.Data.BookDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(305)
                        .HasColumnType("nvarchar(305)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "D. Knuth",
                            Description = "This volume begins with basic programming concepts and advanced algorithms.",
                            Isbn = "ISBN 12345-67890",
                            Price = 52.00m,
                            Title = "Art of Programming"
                        },
                        new
                        {
                            Id = 2,
                            Author = "R. Martin",
                            Description = "A guide to writing clean, maintainable, and efficient code for developers.",
                            Isbn = "ISBN 98765-43210",
                            Price = 45.50m,
                            Title = "Clean Code"
                        },
                        new
                        {
                            Id = 3,
                            Author = "E. Gamma",
                            Description = "Explores classic software design patterns for object-oriented programming.",
                            Isbn = "ISBN 55555-66666",
                            Price = 67.99m,
                            Title = "Design Patterns"
                        },
                        new
                        {
                            Id = 4,
                            Author = "J. Bloch",
                            Description = "Practical advice on Java programming and best practices for developers.",
                            Isbn = "ISBN 11111-22222",
                            Price = 38.75m,
                            Title = "Effective Java"
                        },
                        new
                        {
                            Id = 5,
                            Author = "S. McConnell",
                            Description = "A comprehensive guide to software construction and best coding practices.",
                            Isbn = "ISBN 77777-88888",
                            Price = 59.90m,
                            Title = "Code Complete"
                        },
                        new
                        {
                            Id = 6,
                            Author = "M. Fowler",
                            Description = "Techniques for improving the design of existing code without changing its behavior.",
                            Isbn = "ISBN 44444-33333",
                            Price = 49.99m,
                            Title = "Refactoring"
                        },
                        new
                        {
                            Id = 7,
                            Author = "B. Kernighan",
                            Description = "A definitive guide to the C programming language by its creators.",
                            Isbn = "ISBN 99999-00000",
                            Price = 55.25m,
                            Title = "The C Programming Language"
                        },
                        new
                        {
                            Id = 8,
                            Author = "A. Tanenbaum",
                            Description = "An in-depth exploration of computer networking concepts and protocols.",
                            Isbn = "ISBN 22222-11111",
                            Price = 72.30m,
                            Title = "Computer Networks"
                        },
                        new
                        {
                            Id = 9,
                            Author = "P. Norvig",
                            Description = "A comprehensive introduction to artificial intelligence and its applications.",
                            Isbn = "ISBN 66666-77777",
                            Price = 68.50m,
                            Title = "Artificial Intelligence: A Guide"
                        },
                        new
                        {
                            Id = 10,
                            Author = "L. Bass",
                            Description = "Practical guidance on designing and implementing software architectures.",
                            Isbn = "ISBN 88888-99999",
                            Price = 54.75m,
                            Title = "Software Architecture in Practice"
                        },
                        new
                        {
                            Id = 11,
                            Author = "K. Beck",
                            Description = "A methodology for writing software by first writing tests, then code.",
                            Isbn = "ISBN 33333-44444",
                            Price = 41.20m,
                            Title = "Test-Driven Development"
                        },
                        new
                        {
                            Id = 12,
                            Author = "G. Booch",
                            Description = "Fundamentals of object-oriented design and analysis for software engineering.",
                            Isbn = "ISBN 55555-99999",
                            Price = 63.80m,
                            Title = "Object-Oriented Analysis and Design"
                        },
                        new
                        {
                            Id = 13,
                            Author = "F. Brooks",
                            Description = "Insights into software project management and the challenges of large-scale development.",
                            Isbn = "ISBN 77777-11111",
                            Price = 47.00m,
                            Title = "The Mythical Man-Month"
                        },
                        new
                        {
                            Id = 14,
                            Author = "S. Russell",
                            Description = "A foundational text on AI techniques and their modern applications.",
                            Isbn = "ISBN 22222-55555",
                            Price = 79.95m,
                            Title = "Artificial Intelligence: A Modern Approach"
                        },
                        new
                        {
                            Id = 15,
                            Author = "C. Hoare",
                            Description = "A theoretical exploration of concurrent programming and process communication.",
                            Isbn = "ISBN 99999-22222",
                            Price = 56.30m,
                            Title = "Communicating Sequential Processes"
                        },
                        new
                        {
                            Id = 16,
                            Author = "A. Stepanov",
                            Description = "A deep dive into the principles of generic programming and algorithms.",
                            Isbn = "ISBN 44444-66666",
                            Price = 50.10m,
                            Title = "Elements of Programming"
                        },
                        new
                        {
                            Id = 17,
                            Author = "D. Ritchie",
                            Description = "A classic guide to programming in the UNIX environment.",
                            Isbn = "ISBN 11111-77777",
                            Price = 44.90m,
                            Title = "The UNIX Programming Environment"
                        },
                        new
                        {
                            Id = 18,
                            Author = "B. Stroustrup",
                            Description = "A comprehensive reference for C++ programming by its creator.",
                            Isbn = "ISBN 88888-33333",
                            Price = 73.25m,
                            Title = "The C++ Programming Language"
                        },
                        new
                        {
                            Id = 19,
                            Author = "J. Meyer",
                            Description = "Principles and practices for building robust object-oriented software.",
                            Isbn = "ISBN 66666-44444",
                            Price = 61.50m,
                            Title = "Object-Oriented Software Construction"
                        },
                        new
                        {
                            Id = 20,
                            Author = "T. DeMarco",
                            Description = "A focus on the human side of software development and team dynamics.",
                            Isbn = "ISBN 33333-88888",
                            Price = 39.80m,
                            Title = "Peopleware"
                        },
                        new
                        {
                            Id = 21,
                            Author = "W. Stevens",
                            Description = "A detailed examination of TCP/IP protocols and network programming.",
                            Isbn = "ISBN 55555-22222",
                            Price = 65.00m,
                            Title = "TCP/IP Illustrated"
                        },
                        new
                        {
                            Id = 22,
                            Author = "D. Thomas",
                            Description = "An introduction to Ruby programming and its practical applications.",
                            Isbn = "ISBN 99999-55555",
                            Price = 42.30m,
                            Title = "Programming Ruby"
                        },
                        new
                        {
                            Id = 23,
                            Author = "E. Yourdon",
                            Description = "Techniques for structured analysis in software engineering.",
                            Isbn = "ISBN 77777-33333",
                            Price = 48.60m,
                            Title = "Modern Structured Analysis"
                        },
                        new
                        {
                            Id = 24,
                            Author = "P. Wegner",
                            Description = "A study of interactive systems and their design principles.",
                            Isbn = "ISBN 22222-99999",
                            Price = 57.20m,
                            Title = "Interactive Computing Systems"
                        },
                        new
                        {
                            Id = 25,
                            Author = "R. Sedgewick",
                            Description = "A comprehensive guide to algorithms and data structures.",
                            Isbn = "ISBN 11111-66666",
                            Price = 71.90m,
                            Title = "Algorithms"
                        },
                        new
                        {
                            Id = 26,
                            Author = "C. Alexander",
                            Description = "Architectural patterns applicable to software design and urban planning.",
                            Isbn = "ISBN 88888-44444",
                            Price = 55.75m,
                            Title = "A Pattern Language"
                        },
                        new
                        {
                            Id = 27,
                            Author = "J. Rumbaugh",
                            Description = "An introduction to UML for modeling software systems.",
                            Isbn = "ISBN 66666-55555",
                            Price = 43.90m,
                            Title = "Unified Modeling Language"
                        },
                        new
                        {
                            Id = 28,
                            Author = "G. Kiczales",
                            Description = "Explores aspect-oriented programming techniques and applications.",
                            Isbn = "ISBN 33333-99999",
                            Price = 60.40m,
                            Title = "Aspect-Oriented Programming"
                        },
                        new
                        {
                            Id = 29,
                            Author = "D. Parnas",
                            Description = "Fundamental principles of software design and modularization.",
                            Isbn = "ISBN 55555-11111",
                            Price = 46.15m,
                            Title = "Software Fundamentals"
                        },
                        new
                        {
                            Id = 30,
                            Author = "A. Kay",
                            Description = "A historical overview of Smalltalk and object-oriented programming.",
                            Isbn = "ISBN 99999-77777",
                            Price = 53.60m,
                            Title = "The Early History of Smalltalk"
                        });
                });

            modelBuilder.Entity("Store.Data.OrderDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("DeliveryAmount")
                        .HasColumnType("money");

                    b.Property<string>("DeliveryCode")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("DeliveryDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DeliveryParameters")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("PaymentDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaymentParameters")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Store.Data.OrderItemDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Store.Data.OrderItemDto", b =>
                {
                    b.HasOne("Store.Data.OrderDto", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Store.Data.OrderDto", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
