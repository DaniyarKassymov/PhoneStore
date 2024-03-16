using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lesson.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAuthRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuthRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    DisplayType = table.Column<string>(type: "text", nullable: true),
                    Ram = table.Column<short>(type: "smallint", nullable: true),
                    BatteryCapacity = table.Column<short>(type: "smallint", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhotoPath = table.Column<string>(type: "text", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    UserAuthRoleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_UserAuthRoles_UserAuthRoleId",
                        column: x => x.UserAuthRoleId,
                        principalTable: "UserAuthRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    ContactPhone = table.Column<string>(type: "text", nullable: true),
                    PhoneId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Link", "Name" },
                values: new object[,]
                {
                    { 1L, "https://apple.com", "Apple" },
                    { 2L, "https://apple.com", "Samsung" },
                    { 3L, "https://apple.com", "Xiaomi" },
                    { 4L, "https://huawei.com", "Huawei" }
                });

            migrationBuilder.InsertData(
                table: "UserAuthRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "user" }
                });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "BatteryCapacity", "BrandId", "Description", "DisplayType", "PhotoPath", "Price", "Ram", "Title" },
                values: new object[,]
                {
                    { 1L, (short)3483, 3L, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", "Steel", "uploads/IPhone_16_pro_max.jpg", 2370m, null, "Small Granite Sausages" },
                    { 2L, (short)4003, 4L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Metal", "uploads/IPhone_16_pro_max.jpg", 3502m, null, "Sleek Metal Keyboard" },
                    { 3L, (short)3349, 2L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Cotton", "uploads/IPhone_16_pro_max.jpg", 2346m, null, "Intelligent Granite Bacon" },
                    { 4L, (short)3088, 3L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Wooden", "uploads/IPhone_16_pro_max.jpg", 4116m, null, "Handcrafted Rubber Salad" },
                    { 5L, (short)3615, 3L, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", "Granite", "uploads/IPhone_16_pro_max.jpg", 2738m, null, "Fantastic Steel Mouse" },
                    { 6L, (short)3919, 2L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Plastic", "uploads/IPhone_16_pro_max.jpg", 1118m, null, "Generic Soft Chips" },
                    { 7L, (short)5202, 2L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Frozen", "uploads/IPhone_16_pro_max.jpg", 3243m, null, "Incredible Granite Keyboard" },
                    { 8L, (short)3353, 3L, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", "Plastic", "uploads/IPhone_16_pro_max.jpg", 3966m, null, "Fantastic Concrete Shoes" },
                    { 9L, (short)4752, 2L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Wooden", "uploads/IPhone_16_pro_max.jpg", 1023m, null, "Refined Granite Car" },
                    { 10L, (short)5114, 3L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Frozen", "uploads/IPhone_16_pro_max.jpg", 2079m, null, "Fantastic Cotton Chips" },
                    { 11L, (short)5139, 3L, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", "Fresh", "uploads/IPhone_16_pro_max.jpg", 4347m, null, "Awesome Soft Pizza" },
                    { 12L, (short)3779, 2L, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", "Granite", "uploads/IPhone_16_pro_max.jpg", 4157m, null, "Licensed Concrete Soap" },
                    { 13L, (short)4415, 3L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Rubber", "uploads/IPhone_16_pro_max.jpg", 2828m, null, "Sleek Granite Towels" },
                    { 14L, (short)4158, 4L, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", "Metal", "uploads/IPhone_16_pro_max.jpg", 3564m, null, "Handcrafted Rubber Chips" },
                    { 15L, (short)4967, 3L, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", "Frozen", "uploads/IPhone_16_pro_max.jpg", 2696m, null, "Generic Frozen Pizza" },
                    { 16L, (short)4235, 1L, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", "Cotton", "uploads/IPhone_16_pro_max.jpg", 3152m, null, "Unbranded Rubber Hat" },
                    { 17L, (short)3158, 2L, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", "Concrete", "uploads/IPhone_16_pro_max.jpg", 2196m, null, "Tasty Frozen Pizza" },
                    { 18L, (short)4323, 4L, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", "Plastic", "uploads/IPhone_16_pro_max.jpg", 3773m, null, "Practical Metal Keyboard" },
                    { 19L, (short)4697, 3L, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", "Frozen", "uploads/IPhone_16_pro_max.jpg", 3918m, null, "Awesome Wooden Sausages" },
                    { 20L, (short)5893, 3L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Fresh", "uploads/IPhone_16_pro_max.jpg", 1911m, null, "Licensed Wooden Chips" },
                    { 21L, (short)4501, 2L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Wooden", "uploads/IPhone_16_pro_max.jpg", 1149m, null, "Refined Granite Cheese" },
                    { 22L, (short)3589, 4L, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", "Steel", "uploads/IPhone_16_pro_max.jpg", 4126m, null, "Small Cotton Bacon" },
                    { 23L, (short)5232, 3L, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", "Concrete", "uploads/IPhone_16_pro_max.jpg", 4322m, null, "Intelligent Rubber Chair" },
                    { 24L, (short)3952, 3L, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", "Steel", "uploads/IPhone_16_pro_max.jpg", 1829m, null, "Awesome Wooden Shirt" },
                    { 25L, (short)4101, 1L, "The Football Is Good For Training And Recreational Purposes", "Fresh", "uploads/IPhone_16_pro_max.jpg", 4335m, null, "Practical Soft Pants" },
                    { 26L, (short)4201, 4L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Cotton", "uploads/IPhone_16_pro_max.jpg", 1187m, null, "Tasty Frozen Bike" },
                    { 27L, (short)4158, 2L, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", "Rubber", "uploads/IPhone_16_pro_max.jpg", 4497m, null, "Refined Concrete Computer" },
                    { 28L, (short)3151, 1L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Rubber", "uploads/IPhone_16_pro_max.jpg", 3136m, null, "Incredible Plastic Hat" },
                    { 29L, (short)5935, 2L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Concrete", "uploads/IPhone_16_pro_max.jpg", 3332m, null, "Intelligent Soft Bike" },
                    { 30L, (short)4906, 3L, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", "Steel", "uploads/IPhone_16_pro_max.jpg", 4479m, null, "Awesome Cotton Bike" },
                    { 31L, (short)5095, 4L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Granite", "uploads/IPhone_16_pro_max.jpg", 2272m, null, "Licensed Metal Soap" },
                    { 32L, (short)3741, 1L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Granite", "uploads/IPhone_16_pro_max.jpg", 1988m, null, "Rustic Cotton Pizza" },
                    { 33L, (short)3076, 2L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Frozen", "uploads/IPhone_16_pro_max.jpg", 2572m, null, "Practical Cotton Hat" },
                    { 34L, (short)4411, 3L, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", "Steel", "uploads/IPhone_16_pro_max.jpg", 2838m, null, "Awesome Concrete Chicken" },
                    { 35L, (short)5244, 3L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Rubber", "uploads/IPhone_16_pro_max.jpg", 1001m, null, "Rustic Frozen Table" },
                    { 36L, (short)3523, 2L, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", "Fresh", "uploads/IPhone_16_pro_max.jpg", 4093m, null, "Tasty Metal Chicken" },
                    { 37L, (short)5371, 3L, "The Football Is Good For Training And Recreational Purposes", "Fresh", "uploads/IPhone_16_pro_max.jpg", 3975m, null, "Generic Soft Gloves" },
                    { 38L, (short)3448, 1L, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", "Concrete", "uploads/IPhone_16_pro_max.jpg", 1466m, null, "Licensed Cotton Mouse" },
                    { 39L, (short)5457, 3L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Wooden", "uploads/IPhone_16_pro_max.jpg", 2905m, null, "Fantastic Fresh Fish" },
                    { 40L, (short)3421, 2L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Soft", "uploads/IPhone_16_pro_max.jpg", 1046m, null, "Small Plastic Pizza" },
                    { 41L, (short)4162, 3L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Frozen", "uploads/IPhone_16_pro_max.jpg", 2671m, null, "Practical Soft Bacon" },
                    { 42L, (short)5745, 2L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Wooden", "uploads/IPhone_16_pro_max.jpg", 2207m, null, "Tasty Rubber Gloves" },
                    { 43L, (short)4935, 3L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Plastic", "uploads/IPhone_16_pro_max.jpg", 2693m, null, "Awesome Steel Mouse" },
                    { 44L, (short)5333, 1L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Frozen", "uploads/IPhone_16_pro_max.jpg", 1947m, null, "Rustic Frozen Cheese" },
                    { 45L, (short)4127, 3L, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", "Fresh", "uploads/IPhone_16_pro_max.jpg", 4786m, null, "Sleek Rubber Shirt" },
                    { 46L, (short)4175, 1L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Rubber", "uploads/IPhone_16_pro_max.jpg", 3502m, null, "Incredible Plastic Computer" },
                    { 47L, (short)3936, 1L, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", "Concrete", "uploads/IPhone_16_pro_max.jpg", 1946m, null, "Handmade Steel Towels" },
                    { 48L, (short)4066, 3L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Metal", "uploads/IPhone_16_pro_max.jpg", 2897m, null, "Ergonomic Cotton Sausages" },
                    { 49L, (short)4512, 3L, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", "Soft", "uploads/IPhone_16_pro_max.jpg", 3179m, null, "Licensed Concrete Bacon" },
                    { 50L, (short)4965, 2L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Rubber", "uploads/IPhone_16_pro_max.jpg", 4325m, null, "Handcrafted Metal Shirt" },
                    { 51L, (short)5092, 3L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Cotton", "uploads/IPhone_16_pro_max.jpg", 1813m, null, "Fantastic Rubber Table" },
                    { 52L, (short)5242, 3L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Rubber", "uploads/IPhone_16_pro_max.jpg", 1011m, null, "Refined Wooden Chair" },
                    { 53L, (short)3368, 1L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Metal", "uploads/IPhone_16_pro_max.jpg", 3069m, null, "Small Wooden Bike" },
                    { 54L, (short)4191, 2L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Soft", "uploads/IPhone_16_pro_max.jpg", 4379m, null, "Fantastic Soft Car" },
                    { 55L, (short)4431, 2L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Frozen", "uploads/IPhone_16_pro_max.jpg", 3046m, null, "Tasty Granite Sausages" },
                    { 56L, (short)4625, 1L, "The Football Is Good For Training And Recreational Purposes", "Granite", "uploads/IPhone_16_pro_max.jpg", 1831m, null, "Tasty Cotton Car" },
                    { 57L, (short)4073, 3L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Frozen", "uploads/IPhone_16_pro_max.jpg", 3661m, null, "Incredible Concrete Chips" },
                    { 58L, (short)5509, 3L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Fresh", "uploads/IPhone_16_pro_max.jpg", 4543m, null, "Unbranded Concrete Gloves" },
                    { 59L, (short)3128, 3L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Wooden", "uploads/IPhone_16_pro_max.jpg", 4281m, null, "Rustic Steel Keyboard" },
                    { 60L, (short)5135, 3L, "The Football Is Good For Training And Recreational Purposes", "Rubber", "uploads/IPhone_16_pro_max.jpg", 2512m, null, "Licensed Plastic Tuna" },
                    { 61L, (short)4606, 2L, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", "Soft", "uploads/IPhone_16_pro_max.jpg", 2989m, null, "Awesome Wooden Gloves" },
                    { 62L, (short)5570, 3L, "The Football Is Good For Training And Recreational Purposes", "Metal", "uploads/IPhone_16_pro_max.jpg", 2941m, null, "Tasty Metal Keyboard" },
                    { 63L, (short)5212, 2L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Cotton", "uploads/IPhone_16_pro_max.jpg", 3409m, null, "Ergonomic Rubber Salad" },
                    { 64L, (short)5935, 3L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Concrete", "uploads/IPhone_16_pro_max.jpg", 2688m, null, "Unbranded Metal Tuna" },
                    { 65L, (short)3609, 3L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Granite", "uploads/IPhone_16_pro_max.jpg", 4238m, null, "Small Cotton Mouse" },
                    { 66L, (short)5027, 1L, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", "Granite", "uploads/IPhone_16_pro_max.jpg", 3413m, null, "Intelligent Rubber Pants" },
                    { 67L, (short)3141, 1L, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", "Metal", "uploads/IPhone_16_pro_max.jpg", 4512m, null, "Incredible Cotton Table" },
                    { 68L, (short)3148, 2L, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", "Steel", "uploads/IPhone_16_pro_max.jpg", 4660m, null, "Incredible Plastic Pants" },
                    { 69L, (short)4359, 2L, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", "Frozen", "uploads/IPhone_16_pro_max.jpg", 2542m, null, "Small Cotton Bacon" },
                    { 70L, (short)5149, 3L, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", "Granite", "uploads/IPhone_16_pro_max.jpg", 2444m, null, "Generic Granite Pants" },
                    { 71L, (short)4306, 2L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Granite", "uploads/IPhone_16_pro_max.jpg", 1557m, null, "Refined Steel Shoes" },
                    { 72L, (short)3108, 2L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Fresh", "uploads/IPhone_16_pro_max.jpg", 1784m, null, "Incredible Metal Fish" },
                    { 73L, (short)3347, 3L, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", "Soft", "uploads/IPhone_16_pro_max.jpg", 1319m, null, "Rustic Concrete Ball" },
                    { 74L, (short)4456, 3L, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", "Plastic", "uploads/IPhone_16_pro_max.jpg", 4125m, null, "Incredible Fresh Soap" },
                    { 75L, (short)3960, 3L, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", "Steel", "uploads/IPhone_16_pro_max.jpg", 2736m, null, "Unbranded Steel Pizza" },
                    { 76L, (short)4177, 1L, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", "Plastic", "uploads/IPhone_16_pro_max.jpg", 1246m, null, "Small Rubber Shirt" },
                    { 77L, (short)4111, 1L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Cotton", "uploads/IPhone_16_pro_max.jpg", 4955m, null, "Handmade Concrete Ball" },
                    { 78L, (short)3114, 2L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Steel", "uploads/IPhone_16_pro_max.jpg", 4522m, null, "Generic Steel Gloves" },
                    { 79L, (short)3187, 4L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Plastic", "uploads/IPhone_16_pro_max.jpg", 3203m, null, "Sleek Concrete Salad" },
                    { 80L, (short)5926, 4L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Cotton", "uploads/IPhone_16_pro_max.jpg", 1382m, null, "Handcrafted Cotton Table" },
                    { 81L, (short)4950, 4L, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", "Concrete", "uploads/IPhone_16_pro_max.jpg", 1898m, null, "Incredible Metal Car" },
                    { 82L, (short)3968, 3L, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", "Granite", "uploads/IPhone_16_pro_max.jpg", 1575m, null, "Tasty Metal Chicken" },
                    { 83L, (short)3336, 3L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Granite", "uploads/IPhone_16_pro_max.jpg", 4622m, null, "Small Granite Chips" },
                    { 84L, (short)5845, 3L, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", "Rubber", "uploads/IPhone_16_pro_max.jpg", 3667m, null, "Refined Wooden Shoes" },
                    { 85L, (short)5091, 2L, "The Football Is Good For Training And Recreational Purposes", "Fresh", "uploads/IPhone_16_pro_max.jpg", 1490m, null, "Incredible Metal Table" },
                    { 86L, (short)3780, 3L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Metal", "uploads/IPhone_16_pro_max.jpg", 3704m, null, "Handcrafted Fresh Gloves" },
                    { 87L, (short)5355, 1L, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Cotton", "uploads/IPhone_16_pro_max.jpg", 1702m, null, "Ergonomic Concrete Salad" },
                    { 88L, (short)3544, 2L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Fresh", "uploads/IPhone_16_pro_max.jpg", 3068m, null, "Small Granite Shirt" },
                    { 89L, (short)4911, 1L, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", "Metal", "uploads/IPhone_16_pro_max.jpg", 1604m, null, "Unbranded Metal Gloves" },
                    { 90L, (short)5119, 4L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Cotton", "uploads/IPhone_16_pro_max.jpg", 2825m, null, "Incredible Concrete Sausages" },
                    { 91L, (short)5717, 3L, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", "Soft", "uploads/IPhone_16_pro_max.jpg", 4824m, null, "Licensed Plastic Bike" },
                    { 92L, (short)4715, 4L, "The Football Is Good For Training And Recreational Purposes", "Soft", "uploads/IPhone_16_pro_max.jpg", 4454m, null, "Unbranded Rubber Gloves" },
                    { 93L, (short)5267, 1L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Granite", "uploads/IPhone_16_pro_max.jpg", 1920m, null, "Awesome Cotton Fish" },
                    { 94L, (short)5406, 2L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Fresh", "uploads/IPhone_16_pro_max.jpg", 2497m, null, "Practical Fresh Ball" },
                    { 95L, (short)3803, 1L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Granite", "uploads/IPhone_16_pro_max.jpg", 2362m, null, "Practical Rubber Chips" },
                    { 96L, (short)4446, 2L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Soft", "uploads/IPhone_16_pro_max.jpg", 2228m, null, "Sleek Wooden Car" },
                    { 97L, (short)4080, 1L, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", "Granite", "uploads/IPhone_16_pro_max.jpg", 1589m, null, "Awesome Cotton Ball" },
                    { 98L, (short)5733, 3L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Cotton", "uploads/IPhone_16_pro_max.jpg", 1060m, null, "Unbranded Fresh Hat" },
                    { 99L, (short)5253, 1L, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", "Metal", "uploads/IPhone_16_pro_max.jpg", 4705m, null, "Fantastic Fresh Ball" },
                    { 100L, (short)5780, 1L, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Cotton", "uploads/IPhone_16_pro_max.jpg", 3234m, null, "Handmade Granite Pants" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "BrandId", "Email", "Name", "Password", "UserAuthRoleId" },
                values: new object[,]
                {
                    { new Guid("5ed5a93e-2e3e-4b4c-b025-c332c5ed7984"), 42, 1L, null, "John", null, null },
                    { new Guid("6ef356a2-0cc5-4021-9353-22816c73b51a"), null, null, "admin@admin.admin", null, "admin", 1 },
                    { new Guid("78502abe-c874-46eb-9249-bf4728999b9b"), 19, 1L, null, "Harry", null, null },
                    { new Guid("78aba810-053b-42ea-afac-01258adb4bf4"), 54, 2L, null, "John", null, null },
                    { new Guid("7959adf8-e222-4da9-a76e-7414556da9e6"), 32, 1L, null, "Doe", null, null },
                    { new Guid("d1e16323-93ca-4e57-92a6-8efb54d73506"), 45, 1L, null, "Sam", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PhoneId",
                table: "Orders",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_BrandId",
                table: "Phones",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BrandId",
                table: "Users",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserAuthRoleId",
                table: "Users",
                column: "UserAuthRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "UserAuthRoles");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
