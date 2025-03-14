using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Store.Data.EF
{
    public class StoreDbContext : DbContext
    {

        public DbSet<BookDto> Books { get; set; }

        public DbSet<OrderDto> Orders { get; set; }

        public DbSet<OrderItemDto> OrderItems { get; set; }

        public StoreDbContext() { }
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
            Console.WriteLine("COCK");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDto>(action =>
            {
                action.Property(dto => dto.Isbn)
                    .HasMaxLength(17)
                    .IsRequired();

                action.Property(dto => dto.Title)
                    .HasMaxLength(305)
                    .IsRequired();

                action.Property(dto => dto.Price)
                    .HasColumnType("money")
                    .IsRequired();

                action.HasData(
                    new BookDto
                    {
                        Id = 1,
                        Isbn = "ISBN 12345-67890",
                        Author = "D. Knuth",
                        Title = "Art of Programming",
                        Description = "This volume begins with basic programming concepts and advanced algorithms.",
                        Price = 52.00m
                    },
                    new BookDto
                    {
                        Id = 2,
                        Isbn = "ISBN 98765-43210",
                        Author = "R. Martin",
                        Title = "Clean Code",
                        Description = "A guide to writing clean, maintainable, and efficient code for developers.",
                        Price = 45.50m
                    },
                    new BookDto
                    {
                        Id = 3,
                        Isbn = "ISBN 55555-66666",
                        Author = "E. Gamma",
                        Title = "Design Patterns",
                        Description = "Explores classic software design patterns for object-oriented programming.",
                        Price = 67.99m
                    },
                    new BookDto
                    {
                        Id = 4,
                        Isbn = "ISBN 11111-22222",
                        Author = "J. Bloch",
                        Title = "Effective Java",
                        Description = "Practical advice on Java programming and best practices for developers.",
                        Price = 38.75m
                    },
                    new BookDto
                    {
                        Id = 5,
                        Isbn = "ISBN 77777-88888",
                        Author = "S. McConnell",
                        Title = "Code Complete",
                        Description = "A comprehensive guide to software construction and best coding practices.",
                        Price = 59.90m
                    },
                    new BookDto
                    {
                        Id = 6,
                        Isbn = "ISBN 44444-33333",
                        Author = "M. Fowler",
                        Title = "Refactoring",
                        Description = "Techniques for improving the design of existing code without changing its behavior.",
                        Price = 49.99m
                    },
                    new BookDto
                    {
                        Id = 7,
                        Isbn = "ISBN 99999-00000",
                        Author = "B. Kernighan",
                        Title = "The C Programming Language",
                        Description = "A definitive guide to the C programming language by its creators.",
                        Price = 55.25m
                    },
                    new BookDto
                    {
                        Id = 8,
                        Isbn = "ISBN 22222-11111",
                        Author = "A. Tanenbaum",
                        Title = "Computer Networks",
                        Description = "An in-depth exploration of computer networking concepts and protocols.",
                        Price = 72.30m
                    },
                    new BookDto
                    {
                        Id = 9,
                        Isbn = "ISBN 66666-77777",
                        Author = "P. Norvig",
                        Title = "Artificial Intelligence: A Guide",
                        Description = "A comprehensive introduction to artificial intelligence and its applications.",
                        Price = 68.50m
                    },
                    new BookDto
                    {
                        Id = 10,
                        Isbn = "ISBN 88888-99999",
                        Author = "L. Bass",
                        Title = "Software Architecture in Practice",
                        Description = "Practical guidance on designing and implementing software architectures.",
                        Price = 54.75m
                    },
                    new BookDto
                    {
                        Id = 11,
                        Isbn = "ISBN 33333-44444",
                        Author = "K. Beck",
                        Title = "Test-Driven Development",
                        Description = "A methodology for writing software by first writing tests, then code.",
                        Price = 41.20m
                    },
                    new BookDto
                    {
                        Id = 12,
                        Isbn = "ISBN 55555-99999",
                        Author = "G. Booch",
                        Title = "Object-Oriented Analysis and Design",
                        Description = "Fundamentals of object-oriented design and analysis for software engineering.",
                        Price = 63.80m
                    },
                    new BookDto
                    {
                        Id = 13,
                        Isbn = "ISBN 77777-11111",
                        Author = "F. Brooks",
                        Title = "The Mythical Man-Month",
                        Description = "Insights into software project management and the challenges of large-scale development.",
                        Price = 47.00m
                    },
                    new BookDto
                    {
                        Id = 14,
                        Isbn = "ISBN 22222-55555",
                        Author = "S. Russell",
                        Title = "Artificial Intelligence: A Modern Approach",
                        Description = "A foundational text on AI techniques and their modern applications.",
                        Price = 79.95m
                    },
                    new BookDto
                    {
                        Id = 15,
                        Isbn = "ISBN 99999-22222",
                        Author = "C. Hoare",
                        Title = "Communicating Sequential Processes",
                        Description = "A theoretical exploration of concurrent programming and process communication.",
                        Price = 56.30m
                    },
                    new BookDto
                    {
                        Id = 16,
                        Isbn = "ISBN 44444-66666",
                        Author = "A. Stepanov",
                        Title = "Elements of Programming",
                        Description = "A deep dive into the principles of generic programming and algorithms.",
                        Price = 50.10m
                    },
                    new BookDto
                    {
                        Id = 17,
                        Isbn = "ISBN 11111-77777",
                        Author = "D. Ritchie",
                        Title = "The UNIX Programming Environment",
                        Description = "A classic guide to programming in the UNIX environment.",
                        Price = 44.90m
                    },
                    new BookDto
                    {
                        Id = 18,
                        Isbn = "ISBN 88888-33333",
                        Author = "B. Stroustrup",
                        Title = "The C++ Programming Language",
                        Description = "A comprehensive reference for C++ programming by its creator.",
                        Price = 73.25m
                    },
                    new BookDto
                    {
                        Id = 19,
                        Isbn = "ISBN 66666-44444",
                        Author = "J. Meyer",
                        Title = "Object-Oriented Software Construction",
                        Description = "Principles and practices for building robust object-oriented software.",
                        Price = 61.50m
                    },
                    new BookDto
                    {
                        Id = 20,
                        Isbn = "ISBN 33333-88888",
                        Author = "T. DeMarco",
                        Title = "Peopleware",
                        Description = "A focus on the human side of software development and team dynamics.",
                        Price = 39.80m
                    },
                    new BookDto
                    {
                        Id = 21,
                        Isbn = "ISBN 55555-22222",
                        Author = "W. Stevens",
                        Title = "TCP/IP Illustrated",
                        Description = "A detailed examination of TCP/IP protocols and network programming.",
                        Price = 65.00m
                    },
                    new BookDto
                    {
                        Id = 22,
                        Isbn = "ISBN 99999-55555",
                        Author = "D. Thomas",
                        Title = "Programming Ruby",
                        Description = "An introduction to Ruby programming and its practical applications.",
                        Price = 42.30m
                    },
                    new BookDto
                    {
                        Id = 23,
                        Isbn = "ISBN 77777-33333",
                        Author = "E. Yourdon",
                        Title = "Modern Structured Analysis",
                        Description = "Techniques for structured analysis in software engineering.",
                        Price = 48.60m
                    },
                    new BookDto
                    {
                        Id = 24,
                        Isbn = "ISBN 22222-99999",
                        Author = "P. Wegner",
                        Title = "Interactive Computing Systems",
                        Description = "A study of interactive systems and their design principles.",
                        Price = 57.20m
                    },
                    new BookDto
                    {
                        Id = 25,
                        Isbn = "ISBN 11111-66666",
                        Author = "R. Sedgewick",
                        Title = "Algorithms",
                        Description = "A comprehensive guide to algorithms and data structures.",
                        Price = 71.90m
                    },
                    new BookDto
                    {
                        Id = 26,
                        Isbn = "ISBN 88888-44444",
                        Author = "C. Alexander",
                        Title = "A Pattern Language",
                        Description = "Architectural patterns applicable to software design and urban planning.",
                        Price = 55.75m
                    },
                    new BookDto
                    {
                        Id = 27,
                        Isbn = "ISBN 66666-55555",
                        Author = "J. Rumbaugh",
                        Title = "Unified Modeling Language",
                        Description = "An introduction to UML for modeling software systems.",
                        Price = 43.90m
                    },
                    new BookDto
                    {
                        Id = 28,
                        Isbn = "ISBN 33333-99999",
                        Author = "G. Kiczales",
                        Title = "Aspect-Oriented Programming",
                        Description = "Explores aspect-oriented programming techniques and applications.",
                        Price = 60.40m
                    },
                    new BookDto
                    {
                        Id = 29,
                        Isbn = "ISBN 55555-11111",
                        Author = "D. Parnas",
                        Title = "Software Fundamentals",
                        Description = "Fundamental principles of software design and modularization.",
                        Price = 46.15m
                    },
                    new BookDto
                    {
                        Id = 30,
                        Isbn = "ISBN 99999-77777",
                        Author = "A. Kay",
                        Title = "The Early History of Smalltalk",
                        Description = "A historical overview of Smalltalk and object-oriented programming.",
                        Price = 53.60m
                    }
                );
            });

            modelBuilder.Entity<OrderDto>(action =>
            {
                action.Property(dto => dto.CellPhone)
                    .HasMaxLength(20);

                action.Property(dto => dto.DeliveryCode)
                    .HasMaxLength(40);

                action.Property(dto => dto.DeliveryAmount)
                    .HasColumnType("money");

                action.Property(dto => dto.PaymentCode)
                    .HasMaxLength(10);

                action.Property(dto => dto.DeliveryParameters)
                      .HasConversion(
                          value => JsonConvert.SerializeObject(value),
                          value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);

                action.Property(dto => dto.PaymentParameters)
                      .HasConversion(
                          value => JsonConvert.SerializeObject(value),
                          value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);
            });

            modelBuilder.Entity<OrderItemDto>(action =>
            {
                action.Property(dto => dto.Id)
                    .IsRequired();

                action.Property(dto => dto.Price)
                    .HasColumnType("money")
                    .IsRequired();

                action.HasOne(dto => dto.Order)
                    .WithMany(dto => dto.Items)
                    .IsRequired();
            });
        }
        private static readonly ValueComparer DictionaryComparer =
        new ValueComparer<Dictionary<string, string>>(
        (dictionary1, dictionary2) => dictionary1.SequenceEqual(dictionary2),
        dictionary => dictionary.Aggregate(
            0,
            (a, p) => HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())
        ));
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEfRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StoreDbContext>
                (
                    options => { options.UseSqlServer(connectionString); },
                    ServiceLifetime.Transient
                );
            services.AddScoped<Dictionary<Type, StoreDbContext>>();
            services.AddSingleton<StoreDbContextFactory>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            return services;
        }
    }

}

