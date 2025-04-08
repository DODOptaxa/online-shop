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


namespace Store.Data.EF.Store
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
                        Author = "D. Adams",
                        Title = "The Hitchhiker’s Guide to the Galaxy",
                        Description = "A comedic journey through space with a towel and a lot of absurdity.",
                        Price = 42.00m
                    },
                    new BookDto
                    {
                        Id = 2,
                        Isbn = "ISBN 98765-43210",
                        Author = "G. Orwell",
                        Title = "1984",
                        Description = "Big Brother is watching you. A dystopian classic about surveillance.",
                        Price = 19.84m
                    },
                    new BookDto
                    {
                        Id = 3,
                        Isbn = "ISBN 55555-66666",
                        Author = "A. Hitler",
                        Title = "Mein Kampf",
                        Description = "A controversial and ironic addition for historical infamy.",
                        Price = 14.88m
                    },
                    new BookDto
                    {
                        Id = 4,
                        Isbn = "ISBN 11111-22222",
                        Author = "J.K. Rowling",
                        Title = "Harry Potter and the Philosopher’s Stone",
                        Description = "A boy wizard discovers magic, broomsticks, and capitalism.",
                        Price = 25.99m
                    },
                    new BookDto
                    {
                        Id = 5,
                        Isbn = "ISBN 77777-88888",
                        Author = "S. King",
                        Title = "The Shining",
                        Description = "A writer goes mad in a haunted hotel. Redrum!",
                        Price = 31.50m
                    },
                    new BookDto
                    {
                        Id = 6,
                        Isbn = "ISBN 44444-33333",
                        Author = "E.L. James",
                        Title = "Fifty Shades of Grey",
                        Description = "A book that’s somehow both spicy and a punishment to read.",
                        Price = 9.99m
                    },
                    new BookDto
                    {
                        Id = 7,
                        Isbn = "ISBN 99999-00000",
                        Author = "R. Dahl",
                        Title = "Matilda",
                        Description = "A genius girl with telekinesis fights back against tyranny.",
                        Price = 12.75m
                    },
                    new BookDto
                    {
                        Id = 8,
                        Isbn = "ISBN 22222-11111",
                        Author = "T. Pratchett",
                        Title = "Good Omens",
                        Description = "An angel and demon team up to stop the apocalypse. Hilarious.",
                        Price = 18.30m
                    },
                    new BookDto
                    {
                        Id = 9,
                        Isbn = "ISBN 66666-77777",
                        Author = "F. Kafka",
                        Title = "The Metamorphosis",
                        Description = "Man wakes up as a bug. Life gets weirder from there.",
                        Price = 15.50m
                    },
                    new BookDto
                    {
                        Id = 10,
                        Isbn = "ISBN 88888-99999",
                        Author = "M. Twain",
                        Title = "The Adventures of Tom Sawyer",
                        Description = "A boy’s mischievous adventures in a simpler, yet chaotic time.",
                        Price = 22.75m
                    },
                    new BookDto
                    {
                        Id = 11,
                        Isbn = "ISBN 33333-44444",
                        Author = "L. Carroll",
                        Title = "Alice’s Adventures in Wonderland",
                        Description = "A girl falls into a rabbit hole of nonsense and tea parties.",
                        Price = 17.20m
                    },
                    new BookDto
                    {
                        Id = 12,
                        Isbn = "ISBN 55555-99999",
                        Author = "V. Nabokov",
                        Title = "Lolita",
                        Description = "A disturbing yet beautifully written tale of obsession.",
                        Price = 23.80m
                    },
                    new BookDto
                    {
                        Id = 13,
                        Isbn = "ISBN 77777-11111",
                        Author = "J.R.R. Tolkien",
                        Title = "The Lord of the Rings",
                        Description = "One ring to rule them all. Hobbits, elves, and epic battles.",
                        Price = 35.00m
                    },
                    new BookDto
                    {
                        Id = 14,
                        Isbn = "ISBN 22222-55555",
                        Author = "H. Melville",
                        Title = "Moby-Dick",
                        Description = "A man vs. a whale. Spoiler: the whale wins.",
                        Price = 29.95m
                    },
                    new BookDto
                    {
                        Id = 15,
                        Isbn = "ISBN 99999-22222",
                        Author = "D. Brown",
                        Title = "The Da Vinci Code",
                        Description = "Conspiracy, art, and a lot of running around Europe.",
                        Price = 26.30m
                    },
                    new BookDto
                    {
                        Id = 16,
                        Isbn = "ISBN 44444-66666",
                        Author = "A. Rand",
                        Title = "Atlas Shrugged",
                        Description = "Capitalism’s Bible, or a really long lecture—your choice.",
                        Price = 39.10m
                    },
                    new BookDto
                    {
                        Id = 17,
                        Isbn = "ISBN 11111-77777",
                        Author = "N. Gogol",
                        Title = "Dead Souls",
                        Description = "A man buys dead peasants. Russian satire at its finest.",
                        Price = 21.90m
                    },
                    new BookDto
                    {
                        Id = 18,
                        Isbn = "ISBN 88888-33333",
                        Author = "C. Palahniuk",
                        Title = "Fight Club",
                        Description = "The first rule: don’t talk about it. Oops.",
                        Price = 27.25m
                    },
                    new BookDto
                    {
                        Id = 19,
                        Isbn = "ISBN 66666-44444",
                        Author = "J. Austen",
                        Title = "Pride and Prejudice",
                        Description = "Love, snark, and Regency-era drama.",
                        Price = 14.50m
                    },
                    new BookDto
                    {
                        Id = 20,
                        Isbn = "ISBN 33333-88888",
                        Author = "K. Marx",
                        Title = "The Communist Manifesto",
                        Description = "Workers unite! A short book with big ideas.",
                        Price = 5.80m
                    },
                    new BookDto
                    {
                        Id = 21,
                        Isbn = "ISBN 55555-22222",
                        Author = "H.P. Lovecraft",
                        Title = "The Call of Cthulhu",
                        Description = "Cosmic horror with tentacles. Sweet dreams!",
                        Price = 13.00m
                    },
                    new BookDto
                    {
                        Id = 22,
                        Isbn = "ISBN 99999-55555",
                        Author = "E. Hemingway",
                        Title = "The Old Man and the Sea",
                        Description = "An old man fights a fish. It’s deeper than it sounds.",
                        Price = 16.30m
                    },
                    new BookDto
                    {
                        Id = 23,
                        Isbn = "ISBN 77777-33333",
                        Author = "A. Camus",
                        Title = "The Stranger",
                        Description = "Life is absurd, and so is this guy’s trial.",
                        Price = 18.60m
                    },
                    new BookDto
                    {
                        Id = 24,
                        Isbn = "ISBN 22222-99999",
                        Author = "R. Bradbury",
                        Title = "Fahrenheit 451",
                        Description = "Books burn at 451°F. A warning about censorship.",
                        Price = 20.20m
                    },
                    new BookDto
                    {
                        Id = 25,
                        Isbn = "ISBN 11111-66666",
                        Author = "W. Shakespeare",
                        Title = "Hamlet",
                        Description = "To be or not to be—ghosts, revenge, and skulls.",
                        Price = 11.90m
                    },
                    new BookDto
                    {
                        Id = 26,
                        Isbn = "ISBN 88888-44444",
                        Author = "T. Morrison",
                        Title = "Beloved",
                        Description = "A haunting tale of slavery’s aftermath.",
                        Price = 24.75m
                    },
                    new BookDto
                    {
                        Id = 27,
                        Isbn = "ISBN 66666-55555",
                        Author = "J. Swift",
                        Title = "Gulliver’s Travels",
                        Description = "Tiny people, giants, and a horse society. Satire galore.",
                        Price = 15.90m
                    },
                    new BookDto
                    {
                        Id = 28,
                        Isbn = "ISBN 33333-99999",
                        Author = "M. Shelley",
                        Title = "Frankenstein",
                        Description = "A scientist plays God, and it goes about as well as you’d expect.",
                        Price = 17.40m
                    },
                    new BookDto
                    {
                        Id = 29,
                        Isbn = "ISBN 55555-11111",
                        Author = "P. Coelho",
                        Title = "The Alchemist",
                        Description = "Follow your dreams, find treasure, and overthink everything.",
                        Price = 19.15m
                    },
                    new BookDto
                    {
                        Id = 30,
                        Isbn = "ISBN 99999-77777",
                        Author = "D. Trump",
                        Title = "The Art of the Deal",
                        Description = "Bigly business advice from a guy with gold hair.",
                        Price = 45.60m
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

