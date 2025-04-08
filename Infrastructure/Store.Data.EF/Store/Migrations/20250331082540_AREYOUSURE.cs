using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class AREYOUSURE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "D. Adams", "A comedic journey through space with a towel and a lot of absurdity.", 42.00m, "The Hitchhiker’s Guide to the Galaxy" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "G. Orwell", "Big Brother is watching you. A dystopian classic about surveillance.", 19.84m, "1984" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "A. Hitler", "A controversial and ironic addition for historical infamy.", 14.88m, "Mein Kampf" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "J.K. Rowling", "A boy wizard discovers magic, broomsticks, and capitalism.", 25.99m, "Harry Potter and the Philosopher’s Stone" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "S. King", "A writer goes mad in a haunted hotel. Redrum!", 31.50m, "The Shining" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "E.L. James", "A book that’s somehow both spicy and a punishment to read.", 9.99m, "Fifty Shades of Grey" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "R. Dahl", "A genius girl with telekinesis fights back against tyranny.", 12.75m, "Matilda" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "T. Pratchett", "An angel and demon team up to stop the apocalypse. Hilarious.", 18.30m, "Good Omens" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "F. Kafka", "Man wakes up as a bug. Life gets weirder from there.", 15.50m, "The Metamorphosis" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "M. Twain", "A boy’s mischievous adventures in a simpler, yet chaotic time.", 22.75m, "The Adventures of Tom Sawyer" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "L. Carroll", "A girl falls into a rabbit hole of nonsense and tea parties.", 17.20m, "Alice’s Adventures in Wonderland" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "V. Nabokov", "A disturbing yet beautifully written tale of obsession.", 23.80m, "Lolita" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "J.R.R. Tolkien", "One ring to rule them all. Hobbits, elves, and epic battles.", 35.00m, "The Lord of the Rings" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "H. Melville", "A man vs. a whale. Spoiler: the whale wins.", 29.95m, "Moby-Dick" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "D. Brown", "Conspiracy, art, and a lot of running around Europe.", 26.30m, "The Da Vinci Code" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "A. Rand", "Capitalism’s Bible, or a really long lecture—your choice.", 39.10m, "Atlas Shrugged" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "N. Gogol", "A man buys dead peasants. Russian satire at its finest.", 21.90m, "Dead Souls" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "C. Palahniuk", "The first rule: don’t talk about it. Oops.", 27.25m, "Fight Club" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "J. Austen", "Love, snark, and Regency-era drama.", 14.50m, "Pride and Prejudice" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "K. Marx", "Workers unite! A short book with big ideas.", 5.80m, "The Communist Manifesto" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "H.P. Lovecraft", "Cosmic horror with tentacles. Sweet dreams!", 13.00m, "The Call of Cthulhu" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "E. Hemingway", "An old man fights a fish. It’s deeper than it sounds.", 16.30m, "The Old Man and the Sea" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "A. Camus", "Life is absurd, and so is this guy’s trial.", 18.60m, "The Stranger" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "R. Bradbury", "Books burn at 451°F. A warning about censorship.", 20.20m, "Fahrenheit 451" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "W. Shakespeare", "To be or not to be—ghosts, revenge, and skulls.", 11.90m, "Hamlet" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "T. Morrison", "A haunting tale of slavery’s aftermath.", 24.75m, "Beloved" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "J. Swift", "Tiny people, giants, and a horse society. Satire galore.", 15.90m, "Gulliver’s Travels" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "M. Shelley", "A scientist plays God, and it goes about as well as you’d expect.", 17.40m, "Frankenstein" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "P. Coelho", "Follow your dreams, find treasure, and overthink everything.", 19.15m, "The Alchemist" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "D. Trump", "Bigly business advice from a guy with gold hair.", 45.60m, "The Art of the Deal" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "D. Knuth", "This volume begins with basic programming concepts and advanced algorithms.", 52.00m, "Art of Programming" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "R. Martin", "A guide to writing clean, maintainable, and efficient code for developers.", 45.50m, "Clean Code" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "E. Gamma", "Explores classic software design patterns for object-oriented programming.", 67.99m, "Design Patterns" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "J. Bloch", "Practical advice on Java programming and best practices for developers.", 38.75m, "Effective Java" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "S. McConnell", "A comprehensive guide to software construction and best coding practices.", 59.90m, "Code Complete" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "M. Fowler", "Techniques for improving the design of existing code without changing its behavior.", 49.99m, "Refactoring" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "B. Kernighan", "A definitive guide to the C programming language by its creators.", 55.25m, "The C Programming Language" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "A. Tanenbaum", "An in-depth exploration of computer networking concepts and protocols.", 72.30m, "Computer Networks" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "P. Norvig", "A comprehensive introduction to artificial intelligence and its applications.", 68.50m, "Artificial Intelligence: A Guide" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "L. Bass", "Practical guidance on designing and implementing software architectures.", 54.75m, "Software Architecture in Practice" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "K. Beck", "A methodology for writing software by first writing tests, then code.", 41.20m, "Test-Driven Development" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "G. Booch", "Fundamentals of object-oriented design and analysis for software engineering.", 63.80m, "Object-Oriented Analysis and Design" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "F. Brooks", "Insights into software project management and the challenges of large-scale development.", 47.00m, "The Mythical Man-Month" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "S. Russell", "A foundational text on AI techniques and their modern applications.", 79.95m, "Artificial Intelligence: A Modern Approach" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "C. Hoare", "A theoretical exploration of concurrent programming and process communication.", 56.30m, "Communicating Sequential Processes" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "A. Stepanov", "A deep dive into the principles of generic programming and algorithms.", 50.10m, "Elements of Programming" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "D. Ritchie", "A classic guide to programming in the UNIX environment.", 44.90m, "The UNIX Programming Environment" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "B. Stroustrup", "A comprehensive reference for C++ programming by its creator.", 73.25m, "The C++ Programming Language" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "J. Meyer", "Principles and practices for building robust object-oriented software.", 61.50m, "Object-Oriented Software Construction" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "T. DeMarco", "A focus on the human side of software development and team dynamics.", 39.80m, "Peopleware" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "W. Stevens", "A detailed examination of TCP/IP protocols and network programming.", 65.00m, "TCP/IP Illustrated" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "D. Thomas", "An introduction to Ruby programming and its practical applications.", 42.30m, "Programming Ruby" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "E. Yourdon", "Techniques for structured analysis in software engineering.", 48.60m, "Modern Structured Analysis" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "P. Wegner", "A study of interactive systems and their design principles.", 57.20m, "Interactive Computing Systems" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "R. Sedgewick", "A comprehensive guide to algorithms and data structures.", 71.90m, "Algorithms" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "C. Alexander", "Architectural patterns applicable to software design and urban planning.", 55.75m, "A Pattern Language" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "J. Rumbaugh", "An introduction to UML for modeling software systems.", 43.90m, "Unified Modeling Language" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "G. Kiczales", "Explores aspect-oriented programming techniques and applications.", 60.40m, "Aspect-Oriented Programming" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "D. Parnas", "Fundamental principles of software design and modularization.", 46.15m, "Software Fundamentals" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Author", "Description", "Price", "Title" },
                values: new object[] { "A. Kay", "A historical overview of Smalltalk and object-oriented programming.", 53.60m, "The Early History of Smalltalk" });
        }
    }
}
