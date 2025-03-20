using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class IdentityFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentParameters",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentDescription",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentCode",
                table: "Orders",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryParameters",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryDescription",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryCode",
                table: "Orders",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "CellPhone",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentParameters",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentDescription",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentCode",
                table: "Orders",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryParameters",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryDescription",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryCode",
                table: "Orders",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CellPhone",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Isbn", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "D. Knuth", "This volume begins with basic programming concepts and advanced algorithms.", "ISBN 12345-67890", 52.00m, "Art of Programming" },
                    { 2, "R. Martin", "A guide to writing clean, maintainable, and efficient code for developers.", "ISBN 98765-43210", 45.50m, "Clean Code" },
                    { 3, "E. Gamma", "Explores classic software design patterns for object-oriented programming.", "ISBN 55555-66666", 67.99m, "Design Patterns" },
                    { 4, "J. Bloch", "Practical advice on Java programming and best practices for developers.", "ISBN 11111-22222", 38.75m, "Effective Java" },
                    { 5, "S. McConnell", "A comprehensive guide to software construction and best coding practices.", "ISBN 77777-88888", 59.90m, "Code Complete" },
                    { 6, "M. Fowler", "Techniques for improving the design of existing code without changing its behavior.", "ISBN 44444-33333", 49.99m, "Refactoring" },
                    { 7, "B. Kernighan", "A definitive guide to the C programming language by its creators.", "ISBN 99999-00000", 55.25m, "The C Programming Language" },
                    { 8, "A. Tanenbaum", "An in-depth exploration of computer networking concepts and protocols.", "ISBN 22222-11111", 72.30m, "Computer Networks" },
                    { 9, "P. Norvig", "A comprehensive introduction to artificial intelligence and its applications.", "ISBN 66666-77777", 68.50m, "Artificial Intelligence: A Guide" },
                    { 10, "L. Bass", "Practical guidance on designing and implementing software architectures.", "ISBN 88888-99999", 54.75m, "Software Architecture in Practice" },
                    { 11, "K. Beck", "A methodology for writing software by first writing tests, then code.", "ISBN 33333-44444", 41.20m, "Test-Driven Development" },
                    { 12, "G. Booch", "Fundamentals of object-oriented design and analysis for software engineering.", "ISBN 55555-99999", 63.80m, "Object-Oriented Analysis and Design" },
                    { 13, "F. Brooks", "Insights into software project management and the challenges of large-scale development.", "ISBN 77777-11111", 47.00m, "The Mythical Man-Month" },
                    { 14, "S. Russell", "A foundational text on AI techniques and their modern applications.", "ISBN 22222-55555", 79.95m, "Artificial Intelligence: A Modern Approach" },
                    { 15, "C. Hoare", "A theoretical exploration of concurrent programming and process communication.", "ISBN 99999-22222", 56.30m, "Communicating Sequential Processes" },
                    { 16, "A. Stepanov", "A deep dive into the principles of generic programming and algorithms.", "ISBN 44444-66666", 50.10m, "Elements of Programming" },
                    { 17, "D. Ritchie", "A classic guide to programming in the UNIX environment.", "ISBN 11111-77777", 44.90m, "The UNIX Programming Environment" },
                    { 18, "B. Stroustrup", "A comprehensive reference for C++ programming by its creator.", "ISBN 88888-33333", 73.25m, "The C++ Programming Language" },
                    { 19, "J. Meyer", "Principles and practices for building robust object-oriented software.", "ISBN 66666-44444", 61.50m, "Object-Oriented Software Construction" },
                    { 20, "T. DeMarco", "A focus on the human side of software development and team dynamics.", "ISBN 33333-88888", 39.80m, "Peopleware" },
                    { 21, "W. Stevens", "A detailed examination of TCP/IP protocols and network programming.", "ISBN 55555-22222", 65.00m, "TCP/IP Illustrated" },
                    { 22, "D. Thomas", "An introduction to Ruby programming and its practical applications.", "ISBN 99999-55555", 42.30m, "Programming Ruby" },
                    { 23, "E. Yourdon", "Techniques for structured analysis in software engineering.", "ISBN 77777-33333", 48.60m, "Modern Structured Analysis" },
                    { 24, "P. Wegner", "A study of interactive systems and their design principles.", "ISBN 22222-99999", 57.20m, "Interactive Computing Systems" },
                    { 25, "R. Sedgewick", "A comprehensive guide to algorithms and data structures.", "ISBN 11111-66666", 71.90m, "Algorithms" },
                    { 26, "C. Alexander", "Architectural patterns applicable to software design and urban planning.", "ISBN 88888-44444", 55.75m, "A Pattern Language" },
                    { 27, "J. Rumbaugh", "An introduction to UML for modeling software systems.", "ISBN 66666-55555", 43.90m, "Unified Modeling Language" },
                    { 28, "G. Kiczales", "Explores aspect-oriented programming techniques and applications.", "ISBN 33333-99999", 60.40m, "Aspect-Oriented Programming" },
                    { 29, "D. Parnas", "Fundamental principles of software design and modularization.", "ISBN 55555-11111", 46.15m, "Software Fundamentals" },
                    { 30, "A. Kay", "A historical overview of Smalltalk and object-oriented programming.", "ISBN 99999-77777", 53.60m, "The Early History of Smalltalk" }
                });
        }
    }
}
