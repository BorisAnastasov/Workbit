using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Workbit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceEntries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ceos",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ceos", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_Ceos_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CeoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Ceos_CeoId",
                        column: x => x.CeoId,
                        principalTable: "Ceos",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentBudgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    TotalBudget = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BonusPool = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DateAllocated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDistributed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentBudgets_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Managers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), 0, "0c6f027d-4c4f-4c63-a1de-936157123cf2", new DateTime(1994, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.c.thomas@workbit.com", false, "Bob", "Thomas", false, null, "BOB.C.THOMAS@WORKBIT.COM", "BOB.C.THOMAS", "VRpu9tC+wv9+M7m+rSiaQA==.yzEM6o5xGadq8/h1l1VajOW98GyjlU7yuvkTqjioaDQ=", "+1-555-565-1317", false, "2b06417a14604b10845451069dfb2d06", false, "bob.c.thomas" },
                    { new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), 0, "60f8fb33-19f2-4b1a-89b6-bbfc7e680ee0", new DateTime(1991, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.d.young@workbit.com", false, "Emily", "Young", false, null, "EMILY.D.YOUNG@WORKBIT.COM", "EMILY.D.YOUNG", "uJPSq3R0COM6gzw8WpycKQ==.R43a25kK8Nvf7CmBx7XJaXWzXmAqdIJXm01WXcNymps=", "+1-555-181-7345", false, "30c2adf99ab84c59b3562f8bb6c82d09", false, "emily.d.young" },
                    { new Guid("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), 0, "9e74c3ee-d080-4327-a9e1-dc684b0f12fb", new DateTime(1978, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "carl.t.morgan@workbit.com", false, "Carl", "Morgan", false, null, "CARL.T.MORGAN@WORKBIT.COM", "CARL.T.MORGAN", "2dP8w+uLAp78GmilRHFVWQ==.gQTRER4EwpNjlre2EyAGVl1RZAhE83pZqRpbbxKH7IM=", "+1-555-751-5615", false, "64d07af78ed14620b34dbd0a4cb81d03", false, "carl.t.morgan" },
                    { new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), 0, "b4d008ce-7499-4b90-a8c6-82159ace0d15", new DateTime(1993, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "dave.r.walker@workbit.com", false, "Dave", "Walker", false, null, "DAVE.R.WALKER@WORKBIT.COM", "DAVE.R.WALKER", "SXNCNenE+VBLsVF0/AvKMA==.mejNeWU2rJA7acFd9fxwdqNMS8relhAm4qpcsh5S+/Y=", "+1-555-457-2369", false, "90e3b7f870884b4eb0fa847fe4c6bc08", false, "dave.r.walker" },
                    { new Guid("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801"), 0, "0976d9ca-8d40-41db-af3e-d82b9e7faa9d", new DateTime(1980, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.m.lewis@workbit.com", false, "John", "Lewis", false, null, "JOHN.M.LEWIS@WORKBIT.COM", "JOHN.M.LEWIS", "J4A0mxmgbDjp5K0LgyOBXA==.hV4/nEi6JdWlEdejuxS2PxsqWSaSGFRpof8yNxcA5Ts=", "+1-555-700-2268", false, "9a2f4b30c2fa4c77bf3a9b6a4cf11801", false, "john.m.lewis" },
                    { new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), 0, "2f03d821-b8d4-4174-9484-058862f14940", new DateTime(1990, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "frank.h.scott@workbit.com", false, "Frank", "Scott", false, null, "FRANK.H.SCOTT@WORKBIT.COM", "FRANK.H.SCOTT", "EIclFNThhFpqLtiWvsFefQ==.PaC4OAhYr3IXx8AIUiHAwWk0+uvCiR0lHrp1gcTF190=", "+1-555-833-5859", false, "a73cb7dedf184b6ea5730dcf1f703e10", false, "frank.h.scott" },
                    { new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), 0, "046b4ef7-c147-4a22-8332-dcddbc572f43", new DateTime(1997, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "harry.n.brooks@workbit.com", false, "Harry", "Brooks", false, null, "HARRY.N.BROOKS@WORKBIT.COM", "HARRY.N.BROOKS", "utN5qYL/PyGuQYXMJR3wxg==./c++V0V5W89OJciRNxCUl2r7NGRMprr4lFfPql4sLWg=", "+1-555-232-9155", false, "a802de4b6a4a4e15a8c341f5a6c8d012", false, "harry.n.brooks" },
                    { new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), 0, "2ccda79e-f672-4866-880b-4b75a13b0cbd", new DateTime(1992, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "claire.b.james@workbit.com", false, "Claire", "James", false, null, "CLAIRE.B.JAMES@WORKBIT.COM", "CLAIRE.B.JAMES", "cflCos/lz0CYB/eDULCvOw==.e/iq+pG4fsUOhmfu4w6rDJVWh1zV8FwqbBhvYA6fxmc=", "+1-555-831-6194", false, "ac2a1d43b4604f4e8617c2cfb61a8c07", false, "claire.b.james" },
                    { new Guid("b06c8a25-b13c-4d31-bb49-113aa0cc46b8"), 0, "093ae457-5b6d-4665-adb8-9f8ab52c77ab", new DateTime(1984, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "julia.p.schmidt@workbit.com", false, "Julia", "Schmidt", false, null, "JULIA.P.SCHMIDT@WORKBIT.COM", "JULIA.P.SCHMIDT", "lLSreNpORr3M2c0ceq3+1A==.OZJCo+BKG+7Gab/tqU1Ck8LSbk7aAXTEjWRpubu5CDc=", "+1-555-256-7850", false, "b06c8a25b13c4d31bb49113aa0cc46b8", false, "julia.p.schmidt" },
                    { new Guid("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), 0, "b4b0104d-2a7e-4016-8255-4a64e40ce6dc", new DateTime(1985, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "nina.v.hughes@workbit.com", false, "Nina", "Hughes", false, null, "NINA.V.HUGHES@WORKBIT.COM", "NINA.V.HUGHES", "YqlVCQilL8Pg7PSX8XDn0w==.Pdc/aA85EcIoq7pKLWC+OEBFrpL622x4uV9LjMdsJcg=", "+1-555-310-3315", false, "b0cf283419c543f5b29e9bb85a5a5d04", false, "nina.v.hughes" },
                    { new Guid("b23ae748-2292-4712-8778-3eb591c2f001"), 0, "87dc6d99-7383-439b-8f09-0453983c539e", new DateTime(1981, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "antoine.c.leblanc@workbit.com", false, "Antoine", "LeBlanc", false, null, "ANTOINE.C.LEBLANC@WORKBIT.COM", "ANTOINE.C.LEBLANC", "tW2AyM0xHrH+XFc518lBgA==.pnhbOXxaptKucgw44vgcGAS99ub5ptdjNY3HyxtebVA=", "+1-555-554-3881", false, "b23ae7482292471287783eb591c2f001", false, "antoine.c.leblanc" },
                    { new Guid("befa88e8-83fc-4b64-b4ce-7de0b97b6e51"), 0, "1142e819-ee67-40b0-a731-28d3c5e31c6b", new DateTime(1983, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.j.evans@workbit.com", false, "Michael", "Evans", false, null, "MICHAEL.J.EVANS@WORKBIT.COM", "MICHAEL.J.EVANS", "qrr1tZeCogOzhDP9Zzuejw==.u7/VPeiMolEAVaJ8G2Yx36k7RcN/BKimFhsg25I3FrI=", "+1-555-251-3363", false, "befa88e883fc4b64b4ce7de0b97b6e51", false, "michael.j.evans" },
                    { new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), 0, "20f4b3cd-8125-4ea7-a086-a1669ef1c489", new DateTime(1996, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "grace.l.adams@workbit.com", false, "Grace", "Adams", false, null, "GRACE.L.ADAMS@WORKBIT.COM", "GRACE.L.ADAMS", "OM4/sabqiVCg/nLYHdkyoA==.Cxj4kr26nnjAOwWIaj8pD7r6fxDqndtfoBehr1+A5kE=", "+1-555-139-7399", false, "cf9f7b3e6cdb4b9aa0b44e2f7d527e11", false, "grace.l.adams" },
                    { new Guid("d5e7f9a2-0ac3-4b6d-8c64-6fd8e4c0c013"), 0, "ff5ba9e3-1510-47d7-8692-bb09cff9faff", new DateTime(1994, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", false, "Admin", "Adminov", false, null, "ADMIN@GMAIL.COM", "ADMINADMIN", "dLl16+6FEx0zy2PmRO278A==.7n1Q+eXEdyKHQPg1s4nEG8ay53tQ4U7zSXk47T/t4ic=", "+1-555-581-8245", false, "d5e7f9a20ac34b6d8c646fd8e4c0c013", false, "adminadmin" },
                    { new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101"), 0, "340ed20f-5a74-4630-b612-a9ed6cd7a2c3", new DateTime(1996, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "ivy.m.chen@workbit.com", false, "Ivy", "Chen", false, null, "IVY.M.CHEN@WORKBIT.COM", "IVY.M.CHEN", "jx6J/Du0yQ+Tq9wLTd8ZMg==.K9tfZQguod52e8LX5qTyUzT4oc4Ih/nc5KZBbX3K5Rs=", "+1-555-722-5647", false, "e1a2b3c411114a1a8b2b111111111101", false, "ivy.m.chen" },
                    { new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102"), 0, "be038fb2-c8df-42d5-aaa3-410065fa972b", new DateTime(1993, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "jack.d.miller@workbit.com", false, "Jack", "Miller", false, null, "JACK.D.MILLER@WORKBIT.COM", "JACK.D.MILLER", "F9ESEENE59m4zn1MJY6QZA==.agwkZ58dzwV3jiiIUQd300b/coPW4XnWMrwuvvxo3hg=", "+1-555-267-1373", false, "e1a2b3c411114a1a8b2b111111111102", false, "jack.d.miller" },
                    { new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103"), 0, "228841e3-7d51-4aed-a099-f91858db00e4", new DateTime(1994, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "karen.l.silva@workbit.com", false, "Karen", "Silva", false, null, "KAREN.L.SILVA@WORKBIT.COM", "KAREN.L.SILVA", "rU0VonBDvHy52w2fQ2tilg==.HafTYwQcACX0UTjxHzycUyzG6/x4MEMAgicHjY5oW3g=", "+1-555-377-6247", false, "e1a2b3c411114a1a8b2b111111111103", false, "karen.l.silva" },
                    { new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104"), 0, "29586b9b-bea5-47d2-aa52-580d6e81522d", new DateTime(1992, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "leo.p.novak@workbit.com", false, "Leo", "Novak", false, null, "LEO.P.NOVAK@WORKBIT.COM", "LEO.P.NOVAK", "0zbjXxigDJ31zBNkMGgqOw==.Kowv3agTHezUW9Jbhdiw1wYGbrOJFFL5UAVRvA+y49k=", "+1-555-838-1035", false, "e1a2b3c411114a1a8b2b111111111104", false, "leo.p.novak" },
                    { new Guid("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), 0, "a9bd73e3-3bc8-4493-8d98-00978910f450", new DateTime(1982, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "lisa.r.anderson@workbit.com", false, "Lisa", "Anderson", false, null, "LISA.R.ANDERSON@WORKBIT.COM", "LISA.R.ANDERSON", "OFcznzYAyVzvuvfEgaFKNQ==.CpnSNp6yx8MsIZJeDlV3slWNvzT1j2Vm/8z8jnF0Fs0=", "+1-555-212-5704", false, "f83d8c210b434b158fd120f4e5c72f02", false, "lisa.r.anderson" },
                    { new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), 0, "e4eb9f2d-0cfd-419f-8b9b-6eaddbd5e813", new DateTime(1995, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.k.watson@workbit.com", false, "Alice", "Watson", false, null, "ALICE.K.WATSON@WORKBIT.COM", "ALICE.K.WATSON", "blbhk8ECTVesOhTQFolF2A==.rKc9wFKHrUPdbWqR0qri77ggv9YcFUcXswPKiyBsZj0=", "+1-555-442-3341", false, "f92e7b0f512340c89d288834a3c93005", false, "alice.k.watson" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Code", "Continent", "CurrencyCode", "Name" },
                values: new object[,]
                {
                    { "AD", "Europe", "EUR", "Andorra" },
                    { "AE", "Asia", "AED", "United Arab Emirates" },
                    { "AF", "Asia", "AFN", "Afghanistan" },
                    { "AG", "North America", "XCD", "Antigua and Barbuda" },
                    { "AI", "North America", "XCD", "Anguilla" },
                    { "AL", "Europe", "ALL", "Albania" },
                    { "AM", "Asia", "AMD", "Armenia" },
                    { "AO", "Africa", "AOA", "Angola" },
                    { "AQ", "Antarctica", "", "Antarctica" },
                    { "AR", "South America", "ARS", "Argentina" },
                    { "AS", "Oceania", "USD", "American Samoa" },
                    { "AT", "Europe", "EUR", "Austria" },
                    { "AU", "Oceania", "AUD", "Australia" },
                    { "AW", "North America", "AWG", "Aruba" },
                    { "AX", "Europe", "EUR", "Åland Islands" },
                    { "AZ", "Asia", "AZN", "Azerbaijan" },
                    { "BA", "Europe", "BAM", "Bosnia and Herzegovina" },
                    { "BB", "North America", "BBD", "Barbados" },
                    { "BD", "Asia", "BDT", "Bangladesh" },
                    { "BE", "Europe", "EUR", "Belgium" },
                    { "BF", "Africa", "XOF", "Burkina Faso" },
                    { "BG", "Europe", "BGN", "Bulgaria" },
                    { "BH", "Asia", "BHD", "Bahrain" },
                    { "BI", "Africa", "BIF", "Burundi" },
                    { "BJ", "Africa", "XOF", "Benin" },
                    { "BL", "North America", "EUR", "Saint Barthélemy" },
                    { "BM", "North America", "BMD", "Bermuda" },
                    { "BN", "Asia", "BND", "Brunei Darussalam" },
                    { "BO", "South America", "BOB", "Bolivia" },
                    { "BQ", "North America", "USD", "Bonaire, Sint Eustatius and Saba" },
                    { "BR", "South America", "BRL", "Brazil" },
                    { "BS", "North America", "BSD", "Bahamas" },
                    { "BT", "Asia", "BTN", "Bhutan" },
                    { "BV", "Antarctica", "NOK", "Bouvet Island" },
                    { "BW", "Africa", "BWP", "Botswana" },
                    { "BY", "Europe", "BYN", "Belarus" },
                    { "BZ", "North America", "BZD", "Belize" },
                    { "CA", "North America", "CAD", "Canada" },
                    { "CC", "Oceania", "AUD", "Cocos (Keeling) Islands" },
                    { "CD", "Africa", "CDF", "Congo (Democratic Republic)" },
                    { "CF", "Africa", "XAF", "Central African Republic" },
                    { "CG", "Africa", "XAF", "Congo" },
                    { "CH", "Europe", "CHF", "Switzerland" },
                    { "CI", "Africa", "XOF", "Côte d'Ivoire" },
                    { "CK", "Oceania", "NZD", "Cook Islands" },
                    { "CL", "South America", "CLP", "Chile" },
                    { "CM", "Africa", "XAF", "Cameroon" },
                    { "CN", "Asia", "CNY", "China" },
                    { "CO", "South America", "COP", "Colombia" },
                    { "CR", "North America", "CRC", "Costa Rica" },
                    { "CU", "North America", "CUP", "Cuba" },
                    { "CV", "Africa", "CVE", "Cabo Verde" },
                    { "CW", "North America", "ANG", "Curaçao" },
                    { "CX", "Oceania", "AUD", "Christmas Island" },
                    { "CY", "Europe", "EUR", "Cyprus" },
                    { "CZ", "Europe", "CZK", "Czechia" },
                    { "DE", "Europe", "EUR", "Germany" },
                    { "DJ", "Africa", "DJF", "Djibouti" },
                    { "DK", "Europe", "DKK", "Denmark" },
                    { "DM", "North America", "XCD", "Dominica" },
                    { "DO", "North America", "DOP", "Dominican Republic" },
                    { "DZ", "Africa", "DZD", "Algeria" },
                    { "EC", "South America", "USD", "Ecuador" },
                    { "EE", "Europe", "EUR", "Estonia" },
                    { "EG", "Africa", "EGP", "Egypt" },
                    { "EH", "Africa", "MAD", "Western Sahara" },
                    { "ER", "Africa", "ERN", "Eritrea" },
                    { "ES", "Europe", "EUR", "Spain" },
                    { "ET", "Africa", "ETB", "Ethiopia" },
                    { "FI", "Europe", "EUR", "Finland" },
                    { "FJ", "Oceania", "FJD", "Fiji" },
                    { "FK", "South America", "FKP", "Falkland Islands (Malvinas)" },
                    { "FM", "Oceania", "USD", "Micronesia (Federated States)" },
                    { "FO", "Europe", "DKK", "Faroe Islands" },
                    { "FR", "Europe", "EUR", "France" },
                    { "GA", "Africa", "XAF", "Gabon" },
                    { "GB", "Europe", "GBP", "United Kingdom" },
                    { "GD", "North America", "XCD", "Grenada" },
                    { "GE", "Asia", "GEL", "Georgia" },
                    { "GF", "South America", "EUR", "French Guiana" },
                    { "GG", "Europe", "GBP", "Guernsey" },
                    { "GH", "Africa", "GHS", "Ghana" },
                    { "GI", "Europe", "GIP", "Gibraltar" },
                    { "GL", "North America", "DKK", "Greenland" },
                    { "GM", "Africa", "GMD", "Gambia" },
                    { "GN", "Africa", "GNF", "Guinea" },
                    { "GP", "North America", "EUR", "Guadeloupe" },
                    { "GQ", "Africa", "XAF", "Equatorial Guinea" },
                    { "GR", "Europe", "EUR", "Greece" },
                    { "GS", "Antarctica", "GBP", "South Georgia and the South Sandwich Islands" },
                    { "GT", "North America", "GTQ", "Guatemala" },
                    { "GU", "Oceania", "USD", "Guam" },
                    { "GW", "Africa", "XOF", "Guinea-Bissau" },
                    { "GY", "South America", "GYD", "Guyana" },
                    { "HK", "Asia", "HKD", "Hong Kong" },
                    { "HM", "Antarctica", "AUD", "Heard Island and McDonald Islands" },
                    { "HN", "North America", "HNL", "Honduras" },
                    { "HR", "Europe", "EUR", "Croatia" },
                    { "HT", "North America", "HTG", "Haiti" },
                    { "HU", "Europe", "HUF", "Hungary" },
                    { "ID", "Asia", "IDR", "Indonesia" },
                    { "IE", "Europe", "EUR", "Ireland" },
                    { "IL", "Asia", "ILS", "Israel" },
                    { "IM", "Europe", "GBP", "Isle of Man" },
                    { "IN", "Asia", "INR", "India" },
                    { "IO", "Asia", "USD", "British Indian Ocean Territory" },
                    { "IQ", "Asia", "IQD", "Iraq" },
                    { "IR", "Asia", "IRR", "Iran" },
                    { "IS", "Europe", "ISK", "Iceland" },
                    { "IT", "Europe", "EUR", "Italy" },
                    { "JE", "Europe", "GBP", "Jersey" },
                    { "JM", "North America", "JMD", "Jamaica" },
                    { "JO", "Asia", "JOD", "Jordan" },
                    { "JP", "Asia", "JPY", "Japan" },
                    { "KE", "Africa", "KES", "Kenya" },
                    { "KG", "Asia", "KGS", "Kyrgyzstan" },
                    { "KH", "Asia", "KHR", "Cambodia" },
                    { "KI", "Oceania", "AUD", "Kiribati" },
                    { "KM", "Africa", "KMF", "Comoros" },
                    { "KN", "North America", "XCD", "Saint Kitts and Nevis" },
                    { "KP", "Asia", "KPW", "North Korea" },
                    { "KR", "Asia", "KRW", "South Korea" },
                    { "KW", "Asia", "KWD", "Kuwait" },
                    { "KY", "North America", "KYD", "Cayman Islands" },
                    { "KZ", "Asia", "KZT", "Kazakhstan" },
                    { "LA", "Asia", "LAK", "Laos" },
                    { "LB", "Asia", "LBP", "Lebanon" },
                    { "LC", "North America", "XCD", "Saint Lucia" },
                    { "LI", "Europe", "CHF", "Liechtenstein" },
                    { "LK", "Asia", "LKR", "Sri Lanka" },
                    { "LR", "Africa", "LRD", "Liberia" },
                    { "LS", "Africa", "LSL", "Lesotho" },
                    { "LT", "Europe", "EUR", "Lithuania" },
                    { "LU", "Europe", "EUR", "Luxembourg" },
                    { "LV", "Europe", "EUR", "Latvia" },
                    { "LY", "Africa", "LYD", "Libya" },
                    { "MA", "Africa", "MAD", "Morocco" },
                    { "MC", "Europe", "EUR", "Monaco" },
                    { "MD", "Europe", "MDL", "Moldova" },
                    { "ME", "Europe", "EUR", "Montenegro" },
                    { "MF", "North America", "EUR", "Saint Martin (French part)" },
                    { "MG", "Africa", "MGA", "Madagascar" },
                    { "MH", "Oceania", "USD", "Marshall Islands" },
                    { "MK", "Europe", "MKD", "North Macedonia" },
                    { "ML", "Africa", "XOF", "Mali" },
                    { "MM", "Asia", "MMK", "Myanmar" },
                    { "MN", "Asia", "MNT", "Mongolia" },
                    { "MO", "Asia", "MOP", "Macao" },
                    { "MP", "Oceania", "USD", "Northern Mariana Islands" },
                    { "MQ", "North America", "EUR", "Martinique" },
                    { "MR", "Africa", "MRU", "Mauritania" },
                    { "MS", "North America", "XCD", "Montserrat" },
                    { "MT", "Europe", "EUR", "Malta" },
                    { "MU", "Africa", "MUR", "Mauritius" },
                    { "MV", "Asia", "MVR", "Maldives" },
                    { "MW", "Africa", "MWK", "Malawi" },
                    { "MX", "North America", "MXN", "Mexico" },
                    { "MY", "Asia", "MYR", "Malaysia" },
                    { "MZ", "Africa", "MZN", "Mozambique" },
                    { "NA", "Africa", "NAD", "Namibia" },
                    { "NC", "Oceania", "XPF", "New Caledonia" },
                    { "NE", "Africa", "XOF", "Niger" },
                    { "NF", "Oceania", "AUD", "Norfolk Island" },
                    { "NG", "Africa", "NGN", "Nigeria" },
                    { "NI", "North America", "NIO", "Nicaragua" },
                    { "NL", "Europe", "EUR", "Netherlands" },
                    { "NO", "Europe", "NOK", "Norway" },
                    { "NP", "Asia", "NPR", "Nepal" },
                    { "NR", "Oceania", "AUD", "Nauru" },
                    { "NU", "Oceania", "NZD", "Niue" },
                    { "NZ", "Oceania", "NZD", "New Zealand" },
                    { "OM", "Asia", "OMR", "Oman" },
                    { "PA", "North America", "PAB", "Panama" },
                    { "PE", "South America", "PEN", "Peru" },
                    { "PF", "Oceania", "XPF", "French Polynesia" },
                    { "PG", "Oceania", "PGK", "Papua New Guinea" },
                    { "PH", "Asia", "PHP", "Philippines" },
                    { "PK", "Asia", "PKR", "Pakistan" },
                    { "PL", "Europe", "PLN", "Poland" },
                    { "PM", "North America", "EUR", "Saint Pierre and Miquelon" },
                    { "PN", "Oceania", "NZD", "Pitcairn" },
                    { "PR", "North America", "USD", "Puerto Rico" },
                    { "PS", "Asia", "ILS", "Palestine, State of" },
                    { "PT", "Europe", "EUR", "Portugal" },
                    { "PW", "Oceania", "USD", "Palau" },
                    { "PY", "South America", "PYG", "Paraguay" },
                    { "QA", "Asia", "QAR", "Qatar" },
                    { "RE", "Africa", "EUR", "Réunion" },
                    { "RO", "Europe", "RON", "Romania" },
                    { "RS", "Europe", "RSD", "Serbia" },
                    { "RU", "Europe", "RUB", "Russia" },
                    { "RW", "Africa", "RWF", "Rwanda" },
                    { "SA", "Asia", "SAR", "Saudi Arabia" },
                    { "SB", "Oceania", "SBD", "Solomon Islands" },
                    { "SC", "Africa", "SCR", "Seychelles" },
                    { "SD", "Africa", "SDG", "Sudan" },
                    { "SE", "Europe", "SEK", "Sweden" },
                    { "SG", "Asia", "SGD", "Singapore" },
                    { "SH", "Africa", "SHP", "Saint Helena, Ascension and Tristan da Cunha" },
                    { "SI", "Europe", "EUR", "Slovenia" },
                    { "SJ", "Europe", "NOK", "Svalbard and Jan Mayen" },
                    { "SK", "Europe", "EUR", "Slovakia" },
                    { "SL", "Africa", "SLL", "Sierra Leone" },
                    { "SM", "Europe", "EUR", "San Marino" },
                    { "SN", "Africa", "XOF", "Senegal" },
                    { "SO", "Africa", "SOS", "Somalia" },
                    { "SR", "South America", "SRD", "Suriname" },
                    { "SS", "Africa", "SSP", "South Sudan" },
                    { "ST", "Africa", "STN", "Sao Tome and Principe" },
                    { "SV", "North America", "USD", "El Salvador" },
                    { "SX", "North America", "ANG", "Sint Maarten (Dutch part)" },
                    { "SY", "Asia", "SYP", "Syrian Arab Republic" },
                    { "SZ", "Africa", "SZL", "Eswatini" },
                    { "TC", "North America", "USD", "Turks and Caicos Islands" },
                    { "TD", "Africa", "XAF", "Chad" },
                    { "TF", "Antarctica", "EUR", "French Southern Territories" },
                    { "TG", "Africa", "XOF", "Togo" },
                    { "TH", "Asia", "THB", "Thailand" },
                    { "TJ", "Asia", "TJS", "Tajikistan" },
                    { "TK", "Oceania", "NZD", "Tokelau" },
                    { "TL", "Asia", "USD", "Timor-Leste" },
                    { "TM", "Asia", "TMT", "Turkmenistan" },
                    { "TN", "Africa", "TND", "Tunisia" },
                    { "TO", "Oceania", "TOP", "Tonga" },
                    { "TR", "Asia", "TRY", "Turkey" },
                    { "TT", "North America", "TTD", "Trinidad and Tobago" },
                    { "TV", "Oceania", "AUD", "Tuvalu" },
                    { "TW", "Asia", "TWD", "Taiwan" },
                    { "TZ", "Africa", "TZS", "Tanzania" },
                    { "UA", "Europe", "UAH", "Ukraine" },
                    { "UG", "Africa", "UGX", "Uganda" },
                    { "UM", "Oceania", "USD", "United States Minor Outlying Islands" },
                    { "US", "North America", "USD", "United States" },
                    { "UY", "South America", "UYU", "Uruguay" },
                    { "UZ", "Asia", "UZS", "Uzbekistan" },
                    { "VA", "Europe", "EUR", "Holy See (Vatican City State)" },
                    { "VC", "North America", "XCD", "Saint Vincent and the Grenadines" },
                    { "VE", "South America", "VES", "Venezuela" },
                    { "VG", "North America", "USD", "Virgin Islands (British)" },
                    { "VI", "North America", "USD", "Virgin Islands (U.S.)" },
                    { "VN", "Asia", "VND", "Vietnam" },
                    { "VU", "Oceania", "VUV", "Vanuatu" },
                    { "WF", "Oceania", "XPF", "Wallis and Futuna" },
                    { "WS", "Oceania", "WST", "Samoa" },
                    { "YE", "Asia", "YER", "Yemen" },
                    { "YT", "Africa", "EUR", "Mayotte" },
                    { "ZA", "Africa", "ZAR", "South Africa" },
                    { "ZM", "Africa", "ZMW", "Zambia" },
                    { "ZW", "Africa", "ZWL", "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "AttendanceEntries",
                columns: new[] { "Id", "Timestamp", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 2, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 2, new DateTime(2025, 6, 2, 16, 37, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 3, new DateTime(2025, 6, 2, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 4, new DateTime(2025, 6, 2, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 5, new DateTime(2025, 6, 2, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 6, new DateTime(2025, 6, 2, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 7, new DateTime(2025, 6, 2, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 8, new DateTime(2025, 6, 2, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 9, new DateTime(2025, 6, 2, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 10, new DateTime(2025, 6, 2, 16, 52, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 11, new DateTime(2025, 6, 2, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 12, new DateTime(2025, 6, 2, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 13, new DateTime(2025, 6, 2, 9, 10, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 14, new DateTime(2025, 6, 2, 16, 53, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 15, new DateTime(2025, 6, 2, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 16, new DateTime(2025, 6, 2, 17, 12, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 17, new DateTime(2025, 6, 2, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 18, new DateTime(2025, 6, 2, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 19, new DateTime(2025, 6, 2, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 20, new DateTime(2025, 6, 2, 17, 24, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 21, new DateTime(2025, 6, 2, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 22, new DateTime(2025, 6, 2, 16, 41, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 23, new DateTime(2025, 6, 3, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 24, new DateTime(2025, 6, 3, 17, 19, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 25, new DateTime(2025, 6, 3, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 26, new DateTime(2025, 6, 3, 17, 14, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 27, new DateTime(2025, 6, 3, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 28, new DateTime(2025, 6, 3, 16, 31, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 29, new DateTime(2025, 6, 3, 9, 13, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 30, new DateTime(2025, 6, 3, 16, 36, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 31, new DateTime(2025, 6, 3, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 32, new DateTime(2025, 6, 3, 16, 47, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 33, new DateTime(2025, 6, 3, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 34, new DateTime(2025, 6, 3, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 35, new DateTime(2025, 6, 3, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 36, new DateTime(2025, 6, 3, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 37, new DateTime(2025, 6, 3, 9, 16, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 38, new DateTime(2025, 6, 3, 17, 7, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 39, new DateTime(2025, 6, 3, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 40, new DateTime(2025, 6, 3, 16, 51, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 41, new DateTime(2025, 6, 3, 8, 48, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 42, new DateTime(2025, 6, 3, 17, 13, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 43, new DateTime(2025, 6, 4, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 44, new DateTime(2025, 6, 4, 17, 17, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 45, new DateTime(2025, 6, 4, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 46, new DateTime(2025, 6, 4, 16, 59, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 47, new DateTime(2025, 6, 4, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 48, new DateTime(2025, 6, 4, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 49, new DateTime(2025, 6, 4, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 50, new DateTime(2025, 6, 4, 17, 24, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 51, new DateTime(2025, 6, 4, 9, 5, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 52, new DateTime(2025, 6, 4, 16, 52, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 53, new DateTime(2025, 6, 4, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 54, new DateTime(2025, 6, 4, 16, 52, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 55, new DateTime(2025, 6, 4, 9, 4, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 56, new DateTime(2025, 6, 4, 17, 17, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 57, new DateTime(2025, 6, 4, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 58, new DateTime(2025, 6, 4, 16, 47, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 59, new DateTime(2025, 6, 4, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 60, new DateTime(2025, 6, 4, 17, 14, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 61, new DateTime(2025, 6, 5, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 62, new DateTime(2025, 6, 5, 16, 49, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 63, new DateTime(2025, 6, 5, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 64, new DateTime(2025, 6, 5, 16, 36, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 65, new DateTime(2025, 6, 5, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 66, new DateTime(2025, 6, 5, 17, 16, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 67, new DateTime(2025, 6, 5, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 68, new DateTime(2025, 6, 5, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 69, new DateTime(2025, 6, 5, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 70, new DateTime(2025, 6, 5, 17, 29, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 71, new DateTime(2025, 6, 5, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 72, new DateTime(2025, 6, 5, 17, 12, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 73, new DateTime(2025, 6, 5, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 74, new DateTime(2025, 6, 5, 17, 29, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 75, new DateTime(2025, 6, 5, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 76, new DateTime(2025, 6, 5, 16, 50, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 77, new DateTime(2025, 6, 5, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 78, new DateTime(2025, 6, 5, 16, 43, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 79, new DateTime(2025, 6, 5, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 80, new DateTime(2025, 6, 5, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 81, new DateTime(2025, 6, 5, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 82, new DateTime(2025, 6, 5, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 83, new DateTime(2025, 6, 6, 8, 50, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 84, new DateTime(2025, 6, 6, 16, 36, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 85, new DateTime(2025, 6, 6, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 86, new DateTime(2025, 6, 6, 17, 15, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 87, new DateTime(2025, 6, 6, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 88, new DateTime(2025, 6, 6, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 89, new DateTime(2025, 6, 6, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 90, new DateTime(2025, 6, 6, 16, 44, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 91, new DateTime(2025, 6, 6, 9, 23, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 92, new DateTime(2025, 6, 6, 16, 46, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 93, new DateTime(2025, 6, 6, 9, 13, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 94, new DateTime(2025, 6, 6, 17, 7, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 95, new DateTime(2025, 6, 6, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 96, new DateTime(2025, 6, 6, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 97, new DateTime(2025, 6, 6, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 98, new DateTime(2025, 6, 6, 17, 29, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 99, new DateTime(2025, 6, 6, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 100, new DateTime(2025, 6, 6, 17, 23, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 101, new DateTime(2025, 6, 6, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 102, new DateTime(2025, 6, 6, 17, 3, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 103, new DateTime(2025, 6, 6, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 104, new DateTime(2025, 6, 6, 16, 37, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 105, new DateTime(2025, 6, 9, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 106, new DateTime(2025, 6, 9, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 107, new DateTime(2025, 6, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 108, new DateTime(2025, 6, 9, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 109, new DateTime(2025, 6, 9, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 110, new DateTime(2025, 6, 9, 17, 26, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 111, new DateTime(2025, 6, 9, 8, 50, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 112, new DateTime(2025, 6, 9, 16, 36, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 113, new DateTime(2025, 6, 9, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 114, new DateTime(2025, 6, 9, 17, 13, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 115, new DateTime(2025, 6, 9, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 116, new DateTime(2025, 6, 9, 16, 54, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 117, new DateTime(2025, 6, 9, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 118, new DateTime(2025, 6, 9, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 119, new DateTime(2025, 6, 9, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 120, new DateTime(2025, 6, 9, 16, 50, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 121, new DateTime(2025, 6, 9, 9, 12, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 122, new DateTime(2025, 6, 9, 17, 26, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 123, new DateTime(2025, 6, 9, 9, 18, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 124, new DateTime(2025, 6, 9, 16, 46, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 125, new DateTime(2025, 6, 9, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 126, new DateTime(2025, 6, 9, 17, 18, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 127, new DateTime(2025, 6, 10, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 128, new DateTime(2025, 6, 10, 17, 9, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 129, new DateTime(2025, 6, 10, 8, 57, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 130, new DateTime(2025, 6, 10, 16, 43, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 131, new DateTime(2025, 6, 10, 9, 13, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 132, new DateTime(2025, 6, 10, 16, 34, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 133, new DateTime(2025, 6, 10, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 134, new DateTime(2025, 6, 10, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 135, new DateTime(2025, 6, 10, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 136, new DateTime(2025, 6, 10, 17, 26, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 137, new DateTime(2025, 6, 10, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 138, new DateTime(2025, 6, 10, 17, 28, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 139, new DateTime(2025, 6, 10, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 140, new DateTime(2025, 6, 10, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 141, new DateTime(2025, 6, 10, 9, 7, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 142, new DateTime(2025, 6, 10, 17, 12, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 143, new DateTime(2025, 6, 10, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 144, new DateTime(2025, 6, 10, 16, 50, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 145, new DateTime(2025, 6, 10, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 146, new DateTime(2025, 6, 10, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 147, new DateTime(2025, 6, 10, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 148, new DateTime(2025, 6, 10, 17, 4, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 149, new DateTime(2025, 6, 10, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 150, new DateTime(2025, 6, 10, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 151, new DateTime(2025, 6, 11, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 152, new DateTime(2025, 6, 11, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 153, new DateTime(2025, 6, 11, 8, 57, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 154, new DateTime(2025, 6, 11, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 155, new DateTime(2025, 6, 11, 9, 1, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 156, new DateTime(2025, 6, 11, 16, 57, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 157, new DateTime(2025, 6, 11, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 158, new DateTime(2025, 6, 11, 17, 29, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 159, new DateTime(2025, 6, 11, 9, 23, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 160, new DateTime(2025, 6, 11, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 161, new DateTime(2025, 6, 11, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 162, new DateTime(2025, 6, 11, 17, 5, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 163, new DateTime(2025, 6, 11, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 164, new DateTime(2025, 6, 11, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 165, new DateTime(2025, 6, 11, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 166, new DateTime(2025, 6, 11, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 167, new DateTime(2025, 6, 11, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 168, new DateTime(2025, 6, 11, 17, 23, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 169, new DateTime(2025, 6, 12, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 170, new DateTime(2025, 6, 12, 16, 31, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 171, new DateTime(2025, 6, 12, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 172, new DateTime(2025, 6, 12, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 173, new DateTime(2025, 6, 12, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 174, new DateTime(2025, 6, 12, 16, 47, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 175, new DateTime(2025, 6, 12, 9, 18, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 176, new DateTime(2025, 6, 12, 16, 31, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 177, new DateTime(2025, 6, 12, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 178, new DateTime(2025, 6, 12, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 179, new DateTime(2025, 6, 12, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 180, new DateTime(2025, 6, 12, 16, 51, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 181, new DateTime(2025, 6, 12, 8, 54, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 182, new DateTime(2025, 6, 12, 17, 6, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 183, new DateTime(2025, 6, 12, 9, 12, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 184, new DateTime(2025, 6, 12, 16, 49, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 185, new DateTime(2025, 6, 12, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 186, new DateTime(2025, 6, 12, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 187, new DateTime(2025, 6, 12, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 188, new DateTime(2025, 6, 12, 17, 28, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 189, new DateTime(2025, 6, 12, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 190, new DateTime(2025, 6, 12, 17, 4, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 191, new DateTime(2025, 6, 12, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 192, new DateTime(2025, 6, 12, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 193, new DateTime(2025, 6, 13, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 194, new DateTime(2025, 6, 13, 17, 26, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 195, new DateTime(2025, 6, 13, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 196, new DateTime(2025, 6, 13, 16, 41, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 197, new DateTime(2025, 6, 13, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 198, new DateTime(2025, 6, 13, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 199, new DateTime(2025, 6, 13, 9, 12, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 200, new DateTime(2025, 6, 13, 16, 55, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 201, new DateTime(2025, 6, 13, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 202, new DateTime(2025, 6, 13, 17, 17, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 203, new DateTime(2025, 6, 13, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 204, new DateTime(2025, 6, 13, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 205, new DateTime(2025, 6, 13, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 206, new DateTime(2025, 6, 13, 17, 28, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 207, new DateTime(2025, 6, 13, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 208, new DateTime(2025, 6, 13, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 209, new DateTime(2025, 6, 13, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 210, new DateTime(2025, 6, 13, 17, 11, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 211, new DateTime(2025, 6, 13, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 212, new DateTime(2025, 6, 13, 17, 15, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 213, new DateTime(2025, 6, 13, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 214, new DateTime(2025, 6, 13, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 215, new DateTime(2025, 6, 13, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 216, new DateTime(2025, 6, 13, 16, 54, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 217, new DateTime(2025, 6, 16, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 218, new DateTime(2025, 6, 16, 17, 3, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 219, new DateTime(2025, 6, 16, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 220, new DateTime(2025, 6, 16, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 221, new DateTime(2025, 6, 16, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 222, new DateTime(2025, 6, 16, 16, 59, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 223, new DateTime(2025, 6, 16, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 224, new DateTime(2025, 6, 16, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 225, new DateTime(2025, 6, 16, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 226, new DateTime(2025, 6, 16, 16, 57, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 227, new DateTime(2025, 6, 16, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 228, new DateTime(2025, 6, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 229, new DateTime(2025, 6, 16, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 230, new DateTime(2025, 6, 16, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 231, new DateTime(2025, 6, 16, 9, 13, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 232, new DateTime(2025, 6, 16, 17, 20, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 233, new DateTime(2025, 6, 16, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 234, new DateTime(2025, 6, 16, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 235, new DateTime(2025, 6, 16, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 236, new DateTime(2025, 6, 16, 17, 7, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 237, new DateTime(2025, 6, 16, 9, 10, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 238, new DateTime(2025, 6, 16, 16, 39, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 239, new DateTime(2025, 6, 16, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 240, new DateTime(2025, 6, 16, 16, 52, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 241, new DateTime(2025, 6, 17, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 242, new DateTime(2025, 6, 17, 17, 9, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 243, new DateTime(2025, 6, 17, 9, 26, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 244, new DateTime(2025, 6, 17, 17, 19, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 245, new DateTime(2025, 6, 17, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 246, new DateTime(2025, 6, 17, 17, 5, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 247, new DateTime(2025, 6, 17, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 248, new DateTime(2025, 6, 17, 16, 47, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 249, new DateTime(2025, 6, 17, 8, 47, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 250, new DateTime(2025, 6, 17, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 251, new DateTime(2025, 6, 17, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 252, new DateTime(2025, 6, 17, 17, 1, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 253, new DateTime(2025, 6, 17, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 254, new DateTime(2025, 6, 17, 17, 13, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 255, new DateTime(2025, 6, 17, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 256, new DateTime(2025, 6, 17, 17, 23, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 257, new DateTime(2025, 6, 17, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 258, new DateTime(2025, 6, 17, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 259, new DateTime(2025, 6, 17, 9, 23, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 260, new DateTime(2025, 6, 17, 17, 26, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 261, new DateTime(2025, 6, 17, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 262, new DateTime(2025, 6, 17, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 263, new DateTime(2025, 6, 18, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 264, new DateTime(2025, 6, 18, 17, 20, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 265, new DateTime(2025, 6, 18, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 266, new DateTime(2025, 6, 18, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 267, new DateTime(2025, 6, 18, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 268, new DateTime(2025, 6, 18, 16, 56, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 269, new DateTime(2025, 6, 18, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 270, new DateTime(2025, 6, 18, 16, 46, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 271, new DateTime(2025, 6, 18, 9, 1, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 272, new DateTime(2025, 6, 18, 17, 4, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 273, new DateTime(2025, 6, 18, 9, 26, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 274, new DateTime(2025, 6, 18, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 275, new DateTime(2025, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 276, new DateTime(2025, 6, 18, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 277, new DateTime(2025, 6, 18, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 278, new DateTime(2025, 6, 18, 17, 28, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 279, new DateTime(2025, 6, 18, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 280, new DateTime(2025, 6, 18, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 281, new DateTime(2025, 6, 18, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 282, new DateTime(2025, 6, 18, 17, 3, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 283, new DateTime(2025, 6, 18, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 284, new DateTime(2025, 6, 18, 16, 37, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 285, new DateTime(2025, 6, 18, 9, 18, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 286, new DateTime(2025, 6, 18, 16, 36, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 287, new DateTime(2025, 6, 19, 9, 5, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 288, new DateTime(2025, 6, 19, 16, 52, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 289, new DateTime(2025, 6, 19, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 290, new DateTime(2025, 6, 19, 17, 12, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 291, new DateTime(2025, 6, 19, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 292, new DateTime(2025, 6, 19, 16, 58, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 293, new DateTime(2025, 6, 19, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 294, new DateTime(2025, 6, 19, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 295, new DateTime(2025, 6, 19, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 296, new DateTime(2025, 6, 19, 17, 25, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 297, new DateTime(2025, 6, 19, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 298, new DateTime(2025, 6, 19, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 299, new DateTime(2025, 6, 19, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 300, new DateTime(2025, 6, 19, 17, 19, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 301, new DateTime(2025, 6, 19, 8, 47, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 302, new DateTime(2025, 6, 19, 17, 19, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 303, new DateTime(2025, 6, 19, 9, 22, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 304, new DateTime(2025, 6, 19, 17, 17, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 305, new DateTime(2025, 6, 19, 8, 56, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 306, new DateTime(2025, 6, 19, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 307, new DateTime(2025, 6, 19, 9, 12, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 308, new DateTime(2025, 6, 19, 17, 6, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 309, new DateTime(2025, 6, 19, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 310, new DateTime(2025, 6, 19, 16, 57, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 311, new DateTime(2025, 6, 20, 8, 57, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 312, new DateTime(2025, 6, 20, 16, 52, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 313, new DateTime(2025, 6, 20, 8, 50, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 314, new DateTime(2025, 6, 20, 16, 36, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 315, new DateTime(2025, 6, 20, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 316, new DateTime(2025, 6, 20, 16, 51, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 317, new DateTime(2025, 6, 20, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 318, new DateTime(2025, 6, 20, 16, 39, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 319, new DateTime(2025, 6, 20, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 320, new DateTime(2025, 6, 20, 16, 51, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 321, new DateTime(2025, 6, 20, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 322, new DateTime(2025, 6, 20, 16, 50, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 323, new DateTime(2025, 6, 20, 8, 57, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 324, new DateTime(2025, 6, 20, 16, 54, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 325, new DateTime(2025, 6, 20, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 326, new DateTime(2025, 6, 20, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 327, new DateTime(2025, 6, 20, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 328, new DateTime(2025, 6, 20, 17, 14, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 329, new DateTime(2025, 6, 20, 9, 6, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 330, new DateTime(2025, 6, 20, 17, 24, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 331, new DateTime(2025, 6, 20, 9, 22, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 332, new DateTime(2025, 6, 20, 16, 36, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 333, new DateTime(2025, 6, 20, 8, 54, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 334, new DateTime(2025, 6, 20, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 335, new DateTime(2025, 6, 23, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 336, new DateTime(2025, 6, 23, 16, 47, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 337, new DateTime(2025, 6, 23, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 338, new DateTime(2025, 6, 23, 17, 2, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 339, new DateTime(2025, 6, 23, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 340, new DateTime(2025, 6, 23, 16, 39, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 341, new DateTime(2025, 6, 23, 8, 56, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 342, new DateTime(2025, 6, 23, 16, 58, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 343, new DateTime(2025, 6, 23, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 344, new DateTime(2025, 6, 23, 17, 9, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 345, new DateTime(2025, 6, 23, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 346, new DateTime(2025, 6, 23, 16, 59, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 347, new DateTime(2025, 6, 23, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 348, new DateTime(2025, 6, 23, 16, 43, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 349, new DateTime(2025, 6, 23, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 350, new DateTime(2025, 6, 23, 16, 51, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 351, new DateTime(2025, 6, 23, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 352, new DateTime(2025, 6, 23, 16, 56, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 353, new DateTime(2025, 6, 23, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 354, new DateTime(2025, 6, 23, 16, 39, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 355, new DateTime(2025, 6, 24, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 356, new DateTime(2025, 6, 24, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 357, new DateTime(2025, 6, 24, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 358, new DateTime(2025, 6, 24, 17, 3, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 359, new DateTime(2025, 6, 24, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 360, new DateTime(2025, 6, 24, 16, 34, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 361, new DateTime(2025, 6, 24, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 362, new DateTime(2025, 6, 24, 16, 47, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 363, new DateTime(2025, 6, 24, 8, 57, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 364, new DateTime(2025, 6, 24, 16, 34, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 365, new DateTime(2025, 6, 24, 8, 56, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 366, new DateTime(2025, 6, 24, 16, 39, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 367, new DateTime(2025, 6, 24, 9, 13, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 368, new DateTime(2025, 6, 24, 16, 55, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 369, new DateTime(2025, 6, 24, 9, 7, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 370, new DateTime(2025, 6, 24, 17, 13, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 371, new DateTime(2025, 6, 24, 9, 10, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 372, new DateTime(2025, 6, 24, 16, 51, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 373, new DateTime(2025, 6, 24, 9, 7, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 374, new DateTime(2025, 6, 24, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 375, new DateTime(2025, 6, 24, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 376, new DateTime(2025, 6, 24, 17, 5, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 377, new DateTime(2025, 6, 24, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 378, new DateTime(2025, 6, 24, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 379, new DateTime(2025, 6, 25, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 380, new DateTime(2025, 6, 25, 16, 43, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 381, new DateTime(2025, 6, 25, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 382, new DateTime(2025, 6, 25, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 383, new DateTime(2025, 6, 25, 9, 26, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 384, new DateTime(2025, 6, 25, 17, 6, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 385, new DateTime(2025, 6, 25, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 386, new DateTime(2025, 6, 25, 17, 23, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 387, new DateTime(2025, 6, 25, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 388, new DateTime(2025, 6, 25, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 389, new DateTime(2025, 6, 25, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 390, new DateTime(2025, 6, 25, 16, 59, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 391, new DateTime(2025, 6, 25, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 392, new DateTime(2025, 6, 25, 17, 28, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 393, new DateTime(2025, 6, 25, 8, 54, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 394, new DateTime(2025, 6, 25, 16, 54, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 395, new DateTime(2025, 6, 25, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 396, new DateTime(2025, 6, 25, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 397, new DateTime(2025, 6, 25, 9, 16, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 398, new DateTime(2025, 6, 25, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 399, new DateTime(2025, 6, 26, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 400, new DateTime(2025, 6, 26, 17, 11, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 401, new DateTime(2025, 6, 26, 9, 26, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 402, new DateTime(2025, 6, 26, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 403, new DateTime(2025, 6, 26, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 404, new DateTime(2025, 6, 26, 16, 57, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 405, new DateTime(2025, 6, 26, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 406, new DateTime(2025, 6, 26, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 407, new DateTime(2025, 6, 26, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 408, new DateTime(2025, 6, 26, 17, 7, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 409, new DateTime(2025, 6, 26, 8, 47, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 410, new DateTime(2025, 6, 26, 16, 34, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 411, new DateTime(2025, 6, 26, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 412, new DateTime(2025, 6, 26, 16, 54, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 413, new DateTime(2025, 6, 26, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 414, new DateTime(2025, 6, 26, 17, 19, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 415, new DateTime(2025, 6, 26, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 416, new DateTime(2025, 6, 26, 16, 39, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 417, new DateTime(2025, 6, 26, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 418, new DateTime(2025, 6, 26, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 419, new DateTime(2025, 6, 26, 8, 54, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 420, new DateTime(2025, 6, 26, 17, 5, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 421, new DateTime(2025, 6, 27, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 422, new DateTime(2025, 6, 27, 16, 41, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 423, new DateTime(2025, 6, 27, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 424, new DateTime(2025, 6, 27, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 425, new DateTime(2025, 6, 27, 9, 6, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 426, new DateTime(2025, 6, 27, 16, 57, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 427, new DateTime(2025, 6, 27, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 428, new DateTime(2025, 6, 27, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 429, new DateTime(2025, 6, 27, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 430, new DateTime(2025, 6, 27, 17, 14, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 431, new DateTime(2025, 6, 27, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 432, new DateTime(2025, 6, 27, 16, 36, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 433, new DateTime(2025, 6, 27, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 434, new DateTime(2025, 6, 27, 17, 7, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 435, new DateTime(2025, 6, 27, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 436, new DateTime(2025, 6, 27, 16, 52, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 437, new DateTime(2025, 6, 27, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 438, new DateTime(2025, 6, 27, 17, 8, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 439, new DateTime(2025, 6, 27, 9, 4, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 440, new DateTime(2025, 6, 27, 16, 31, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 441, new DateTime(2025, 6, 27, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 442, new DateTime(2025, 6, 27, 17, 15, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 443, new DateTime(2025, 6, 27, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 444, new DateTime(2025, 6, 27, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 445, new DateTime(2025, 6, 30, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 446, new DateTime(2025, 6, 30, 16, 36, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 447, new DateTime(2025, 6, 30, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 448, new DateTime(2025, 6, 30, 17, 24, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 449, new DateTime(2025, 6, 30, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 450, new DateTime(2025, 6, 30, 17, 9, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 451, new DateTime(2025, 6, 30, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 452, new DateTime(2025, 6, 30, 16, 57, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 453, new DateTime(2025, 6, 30, 8, 48, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 454, new DateTime(2025, 6, 30, 17, 18, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 455, new DateTime(2025, 6, 30, 9, 26, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 456, new DateTime(2025, 6, 30, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 457, new DateTime(2025, 6, 30, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 458, new DateTime(2025, 6, 30, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 459, new DateTime(2025, 6, 30, 8, 50, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 460, new DateTime(2025, 6, 30, 17, 17, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 461, new DateTime(2025, 6, 30, 9, 7, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 462, new DateTime(2025, 6, 30, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 463, new DateTime(2025, 6, 30, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 464, new DateTime(2025, 6, 30, 17, 12, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 465, new DateTime(2025, 6, 30, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 466, new DateTime(2025, 6, 30, 16, 54, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 467, new DateTime(2025, 6, 30, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 468, new DateTime(2025, 6, 30, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 469, new DateTime(2025, 7, 1, 9, 18, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 470, new DateTime(2025, 7, 1, 17, 13, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 471, new DateTime(2025, 7, 1, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 472, new DateTime(2025, 7, 1, 17, 12, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 473, new DateTime(2025, 7, 1, 9, 26, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 474, new DateTime(2025, 7, 1, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 475, new DateTime(2025, 7, 1, 9, 1, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 476, new DateTime(2025, 7, 1, 16, 43, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 477, new DateTime(2025, 7, 1, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 478, new DateTime(2025, 7, 1, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 479, new DateTime(2025, 7, 1, 9, 18, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 480, new DateTime(2025, 7, 1, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 481, new DateTime(2025, 7, 1, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 482, new DateTime(2025, 7, 1, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 483, new DateTime(2025, 7, 1, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 484, new DateTime(2025, 7, 1, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 485, new DateTime(2025, 7, 1, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 486, new DateTime(2025, 7, 1, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 487, new DateTime(2025, 7, 1, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 488, new DateTime(2025, 7, 1, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 489, new DateTime(2025, 7, 1, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 490, new DateTime(2025, 7, 1, 17, 12, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 491, new DateTime(2025, 7, 2, 8, 54, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 492, new DateTime(2025, 7, 2, 17, 25, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 493, new DateTime(2025, 7, 2, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 494, new DateTime(2025, 7, 2, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 495, new DateTime(2025, 7, 2, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 496, new DateTime(2025, 7, 2, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 497, new DateTime(2025, 7, 2, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 498, new DateTime(2025, 7, 2, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 499, new DateTime(2025, 7, 2, 8, 57, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 500, new DateTime(2025, 7, 2, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 501, new DateTime(2025, 7, 2, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 502, new DateTime(2025, 7, 2, 17, 9, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 503, new DateTime(2025, 7, 2, 9, 16, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 504, new DateTime(2025, 7, 2, 16, 37, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 505, new DateTime(2025, 7, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 506, new DateTime(2025, 7, 2, 16, 55, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 507, new DateTime(2025, 7, 2, 9, 6, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 508, new DateTime(2025, 7, 2, 17, 9, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 509, new DateTime(2025, 7, 2, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 510, new DateTime(2025, 7, 2, 17, 19, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 511, new DateTime(2025, 7, 2, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 512, new DateTime(2025, 7, 2, 17, 5, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 513, new DateTime(2025, 7, 2, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 514, new DateTime(2025, 7, 2, 17, 19, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 515, new DateTime(2025, 7, 3, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 516, new DateTime(2025, 7, 3, 17, 28, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 517, new DateTime(2025, 7, 3, 9, 7, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 518, new DateTime(2025, 7, 3, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 519, new DateTime(2025, 7, 3, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 520, new DateTime(2025, 7, 3, 16, 46, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 521, new DateTime(2025, 7, 3, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 522, new DateTime(2025, 7, 3, 17, 6, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 523, new DateTime(2025, 7, 3, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 524, new DateTime(2025, 7, 3, 16, 53, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 525, new DateTime(2025, 7, 3, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 526, new DateTime(2025, 7, 3, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 527, new DateTime(2025, 7, 3, 8, 47, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 528, new DateTime(2025, 7, 3, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 529, new DateTime(2025, 7, 3, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 530, new DateTime(2025, 7, 3, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 531, new DateTime(2025, 7, 3, 9, 1, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 532, new DateTime(2025, 7, 3, 16, 34, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 533, new DateTime(2025, 7, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 534, new DateTime(2025, 7, 3, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 535, new DateTime(2025, 7, 3, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 536, new DateTime(2025, 7, 3, 17, 20, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 537, new DateTime(2025, 7, 4, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 538, new DateTime(2025, 7, 4, 17, 11, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 539, new DateTime(2025, 7, 4, 8, 54, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 540, new DateTime(2025, 7, 4, 16, 56, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 541, new DateTime(2025, 7, 4, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 542, new DateTime(2025, 7, 4, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 543, new DateTime(2025, 7, 4, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 544, new DateTime(2025, 7, 4, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 545, new DateTime(2025, 7, 4, 9, 22, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 546, new DateTime(2025, 7, 4, 16, 56, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 547, new DateTime(2025, 7, 4, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 548, new DateTime(2025, 7, 4, 17, 1, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 549, new DateTime(2025, 7, 4, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 550, new DateTime(2025, 7, 4, 17, 28, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 551, new DateTime(2025, 7, 4, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 552, new DateTime(2025, 7, 4, 17, 9, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 553, new DateTime(2025, 7, 4, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 554, new DateTime(2025, 7, 4, 17, 6, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 555, new DateTime(2025, 7, 4, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 556, new DateTime(2025, 7, 4, 17, 25, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 557, new DateTime(2025, 7, 4, 9, 12, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 558, new DateTime(2025, 7, 4, 16, 55, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 559, new DateTime(2025, 7, 4, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 560, new DateTime(2025, 7, 4, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 561, new DateTime(2025, 7, 7, 8, 47, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 562, new DateTime(2025, 7, 7, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 563, new DateTime(2025, 7, 7, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 564, new DateTime(2025, 7, 7, 16, 59, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 565, new DateTime(2025, 7, 7, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 566, new DateTime(2025, 7, 7, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 567, new DateTime(2025, 7, 7, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 568, new DateTime(2025, 7, 7, 16, 44, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 569, new DateTime(2025, 7, 7, 9, 6, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 570, new DateTime(2025, 7, 7, 17, 19, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 571, new DateTime(2025, 7, 7, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 572, new DateTime(2025, 7, 7, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 573, new DateTime(2025, 7, 7, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 574, new DateTime(2025, 7, 7, 17, 14, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 575, new DateTime(2025, 7, 7, 8, 48, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 576, new DateTime(2025, 7, 7, 17, 20, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 577, new DateTime(2025, 7, 7, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 578, new DateTime(2025, 7, 7, 16, 43, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 579, new DateTime(2025, 7, 7, 9, 10, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 580, new DateTime(2025, 7, 7, 17, 8, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 581, new DateTime(2025, 7, 8, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 582, new DateTime(2025, 7, 8, 16, 34, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 583, new DateTime(2025, 7, 8, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 584, new DateTime(2025, 7, 8, 17, 16, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 585, new DateTime(2025, 7, 8, 9, 6, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 586, new DateTime(2025, 7, 8, 16, 47, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 587, new DateTime(2025, 7, 8, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 588, new DateTime(2025, 7, 8, 16, 44, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 589, new DateTime(2025, 7, 8, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 590, new DateTime(2025, 7, 8, 16, 47, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 591, new DateTime(2025, 7, 8, 9, 10, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 592, new DateTime(2025, 7, 8, 17, 18, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 593, new DateTime(2025, 7, 8, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 594, new DateTime(2025, 7, 8, 16, 34, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 595, new DateTime(2025, 7, 8, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 596, new DateTime(2025, 7, 8, 17, 9, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 597, new DateTime(2025, 7, 8, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 598, new DateTime(2025, 7, 8, 17, 26, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 599, new DateTime(2025, 7, 8, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 600, new DateTime(2025, 7, 8, 16, 41, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 601, new DateTime(2025, 7, 8, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 602, new DateTime(2025, 7, 8, 17, 23, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 603, new DateTime(2025, 7, 9, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 604, new DateTime(2025, 7, 9, 16, 53, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 605, new DateTime(2025, 7, 9, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 606, new DateTime(2025, 7, 9, 16, 31, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 607, new DateTime(2025, 7, 9, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 608, new DateTime(2025, 7, 9, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 609, new DateTime(2025, 7, 9, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 610, new DateTime(2025, 7, 9, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 611, new DateTime(2025, 7, 9, 9, 6, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 612, new DateTime(2025, 7, 9, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 613, new DateTime(2025, 7, 9, 8, 50, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 614, new DateTime(2025, 7, 9, 17, 29, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 615, new DateTime(2025, 7, 9, 9, 1, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 616, new DateTime(2025, 7, 9, 16, 55, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 617, new DateTime(2025, 7, 9, 9, 5, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 618, new DateTime(2025, 7, 9, 16, 55, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 619, new DateTime(2025, 7, 9, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 620, new DateTime(2025, 7, 9, 16, 57, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 621, new DateTime(2025, 7, 9, 9, 10, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 622, new DateTime(2025, 7, 9, 16, 39, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 623, new DateTime(2025, 7, 9, 9, 26, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 624, new DateTime(2025, 7, 9, 17, 15, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 625, new DateTime(2025, 7, 9, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 626, new DateTime(2025, 7, 9, 17, 16, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 627, new DateTime(2025, 7, 10, 9, 18, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 628, new DateTime(2025, 7, 10, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 629, new DateTime(2025, 7, 10, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 630, new DateTime(2025, 7, 10, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 631, new DateTime(2025, 7, 10, 8, 50, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 632, new DateTime(2025, 7, 10, 16, 39, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 633, new DateTime(2025, 7, 10, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 634, new DateTime(2025, 7, 10, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 635, new DateTime(2025, 7, 10, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 636, new DateTime(2025, 7, 10, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 637, new DateTime(2025, 7, 10, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 638, new DateTime(2025, 7, 10, 16, 58, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 639, new DateTime(2025, 7, 10, 9, 10, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 640, new DateTime(2025, 7, 10, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 641, new DateTime(2025, 7, 10, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 642, new DateTime(2025, 7, 10, 17, 3, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 643, new DateTime(2025, 7, 10, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 644, new DateTime(2025, 7, 10, 17, 23, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 645, new DateTime(2025, 7, 10, 9, 12, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 646, new DateTime(2025, 7, 10, 17, 14, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 647, new DateTime(2025, 7, 10, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 648, new DateTime(2025, 7, 10, 16, 47, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 649, new DateTime(2025, 7, 11, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 650, new DateTime(2025, 7, 11, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 651, new DateTime(2025, 7, 11, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 652, new DateTime(2025, 7, 11, 16, 43, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 653, new DateTime(2025, 7, 11, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 654, new DateTime(2025, 7, 11, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 655, new DateTime(2025, 7, 11, 9, 26, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 656, new DateTime(2025, 7, 11, 17, 9, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 657, new DateTime(2025, 7, 11, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 658, new DateTime(2025, 7, 11, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 659, new DateTime(2025, 7, 11, 8, 48, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 660, new DateTime(2025, 7, 11, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 661, new DateTime(2025, 7, 11, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 662, new DateTime(2025, 7, 11, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 663, new DateTime(2025, 7, 11, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 664, new DateTime(2025, 7, 11, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 665, new DateTime(2025, 7, 11, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 666, new DateTime(2025, 7, 11, 16, 53, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 667, new DateTime(2025, 7, 11, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 668, new DateTime(2025, 7, 11, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 669, new DateTime(2025, 7, 11, 9, 5, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 670, new DateTime(2025, 7, 11, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 671, new DateTime(2025, 7, 11, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 672, new DateTime(2025, 7, 11, 17, 7, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 673, new DateTime(2025, 7, 14, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 674, new DateTime(2025, 7, 14, 17, 16, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 675, new DateTime(2025, 7, 14, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 676, new DateTime(2025, 7, 14, 17, 2, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 677, new DateTime(2025, 7, 14, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 678, new DateTime(2025, 7, 14, 16, 34, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 679, new DateTime(2025, 7, 14, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 680, new DateTime(2025, 7, 14, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 681, new DateTime(2025, 7, 14, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 682, new DateTime(2025, 7, 14, 17, 1, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 683, new DateTime(2025, 7, 14, 9, 12, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 684, new DateTime(2025, 7, 14, 17, 25, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 685, new DateTime(2025, 7, 14, 9, 5, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 686, new DateTime(2025, 7, 14, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 687, new DateTime(2025, 7, 14, 9, 18, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 688, new DateTime(2025, 7, 14, 16, 41, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 689, new DateTime(2025, 7, 14, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 690, new DateTime(2025, 7, 14, 16, 50, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 691, new DateTime(2025, 7, 14, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 692, new DateTime(2025, 7, 14, 16, 58, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 693, new DateTime(2025, 7, 14, 9, 22, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 694, new DateTime(2025, 7, 14, 17, 13, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 695, new DateTime(2025, 7, 14, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 696, new DateTime(2025, 7, 14, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 697, new DateTime(2025, 7, 15, 9, 13, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 698, new DateTime(2025, 7, 15, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 699, new DateTime(2025, 7, 15, 8, 57, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 700, new DateTime(2025, 7, 15, 16, 46, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 701, new DateTime(2025, 7, 15, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 702, new DateTime(2025, 7, 15, 17, 8, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 703, new DateTime(2025, 7, 15, 9, 1, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 704, new DateTime(2025, 7, 15, 17, 4, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 705, new DateTime(2025, 7, 15, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 706, new DateTime(2025, 7, 15, 17, 20, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 707, new DateTime(2025, 7, 15, 9, 7, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 708, new DateTime(2025, 7, 15, 17, 15, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 709, new DateTime(2025, 7, 15, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 710, new DateTime(2025, 7, 15, 16, 37, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 711, new DateTime(2025, 7, 15, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 712, new DateTime(2025, 7, 15, 17, 28, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 713, new DateTime(2025, 7, 15, 9, 23, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 714, new DateTime(2025, 7, 15, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 715, new DateTime(2025, 7, 15, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 716, new DateTime(2025, 7, 15, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 717, new DateTime(2025, 7, 15, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 718, new DateTime(2025, 7, 15, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 719, new DateTime(2025, 7, 15, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 720, new DateTime(2025, 7, 15, 17, 29, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 721, new DateTime(2025, 7, 16, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 722, new DateTime(2025, 7, 16, 17, 25, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 723, new DateTime(2025, 7, 16, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 724, new DateTime(2025, 7, 16, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 725, new DateTime(2025, 7, 16, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 726, new DateTime(2025, 7, 16, 17, 18, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 727, new DateTime(2025, 7, 16, 9, 13, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 728, new DateTime(2025, 7, 16, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 729, new DateTime(2025, 7, 16, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 730, new DateTime(2025, 7, 16, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 731, new DateTime(2025, 7, 16, 8, 48, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 732, new DateTime(2025, 7, 16, 17, 8, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 733, new DateTime(2025, 7, 16, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 734, new DateTime(2025, 7, 16, 16, 57, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 735, new DateTime(2025, 7, 16, 9, 12, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 736, new DateTime(2025, 7, 16, 17, 29, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 737, new DateTime(2025, 7, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 738, new DateTime(2025, 7, 16, 16, 52, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 739, new DateTime(2025, 7, 16, 8, 57, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 740, new DateTime(2025, 7, 16, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 741, new DateTime(2025, 7, 17, 9, 23, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 742, new DateTime(2025, 7, 17, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 743, new DateTime(2025, 7, 17, 9, 10, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 744, new DateTime(2025, 7, 17, 16, 59, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 745, new DateTime(2025, 7, 17, 9, 16, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 746, new DateTime(2025, 7, 17, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 747, new DateTime(2025, 7, 17, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 748, new DateTime(2025, 7, 17, 16, 57, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 749, new DateTime(2025, 7, 17, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 750, new DateTime(2025, 7, 17, 16, 44, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 751, new DateTime(2025, 7, 17, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 752, new DateTime(2025, 7, 17, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 753, new DateTime(2025, 7, 17, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 754, new DateTime(2025, 7, 17, 17, 22, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 755, new DateTime(2025, 7, 17, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 756, new DateTime(2025, 7, 17, 17, 28, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 757, new DateTime(2025, 7, 17, 8, 59, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 758, new DateTime(2025, 7, 17, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 759, new DateTime(2025, 7, 17, 8, 54, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 760, new DateTime(2025, 7, 17, 17, 26, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 761, new DateTime(2025, 7, 17, 9, 7, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 762, new DateTime(2025, 7, 17, 16, 37, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 763, new DateTime(2025, 7, 18, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 764, new DateTime(2025, 7, 18, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 765, new DateTime(2025, 7, 18, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 766, new DateTime(2025, 7, 18, 17, 6, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 767, new DateTime(2025, 7, 18, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 768, new DateTime(2025, 7, 18, 17, 5, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 769, new DateTime(2025, 7, 18, 9, 18, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 770, new DateTime(2025, 7, 18, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 771, new DateTime(2025, 7, 18, 9, 7, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 772, new DateTime(2025, 7, 18, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 773, new DateTime(2025, 7, 18, 9, 1, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 774, new DateTime(2025, 7, 18, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 775, new DateTime(2025, 7, 18, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 776, new DateTime(2025, 7, 18, 16, 55, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 777, new DateTime(2025, 7, 18, 8, 47, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 778, new DateTime(2025, 7, 18, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 779, new DateTime(2025, 7, 18, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 780, new DateTime(2025, 7, 18, 16, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 781, new DateTime(2025, 7, 18, 9, 6, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 782, new DateTime(2025, 7, 18, 17, 20, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 783, new DateTime(2025, 7, 18, 9, 4, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 784, new DateTime(2025, 7, 18, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 785, new DateTime(2025, 7, 18, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 786, new DateTime(2025, 7, 18, 16, 42, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 787, new DateTime(2025, 7, 21, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 788, new DateTime(2025, 7, 21, 17, 18, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 789, new DateTime(2025, 7, 21, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 790, new DateTime(2025, 7, 21, 17, 21, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 791, new DateTime(2025, 7, 21, 9, 15, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 792, new DateTime(2025, 7, 21, 16, 56, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 793, new DateTime(2025, 7, 21, 9, 3, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 794, new DateTime(2025, 7, 21, 17, 19, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 795, new DateTime(2025, 7, 21, 9, 22, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 796, new DateTime(2025, 7, 21, 17, 16, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 797, new DateTime(2025, 7, 21, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 798, new DateTime(2025, 7, 21, 17, 12, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 799, new DateTime(2025, 7, 21, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 800, new DateTime(2025, 7, 21, 16, 49, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 801, new DateTime(2025, 7, 21, 9, 10, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 802, new DateTime(2025, 7, 21, 16, 56, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 803, new DateTime(2025, 7, 21, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 804, new DateTime(2025, 7, 21, 17, 15, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 805, new DateTime(2025, 7, 21, 8, 58, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 806, new DateTime(2025, 7, 21, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 807, new DateTime(2025, 7, 21, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 808, new DateTime(2025, 7, 21, 17, 18, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 809, new DateTime(2025, 7, 22, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 810, new DateTime(2025, 7, 22, 16, 52, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 811, new DateTime(2025, 7, 22, 8, 50, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 812, new DateTime(2025, 7, 22, 17, 3, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 813, new DateTime(2025, 7, 22, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 814, new DateTime(2025, 7, 22, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 815, new DateTime(2025, 7, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 816, new DateTime(2025, 7, 22, 16, 34, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 817, new DateTime(2025, 7, 22, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 818, new DateTime(2025, 7, 22, 16, 53, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 819, new DateTime(2025, 7, 22, 9, 29, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 820, new DateTime(2025, 7, 22, 16, 43, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 821, new DateTime(2025, 7, 22, 9, 7, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 822, new DateTime(2025, 7, 22, 17, 23, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 823, new DateTime(2025, 7, 22, 8, 55, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 824, new DateTime(2025, 7, 22, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 825, new DateTime(2025, 7, 22, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 826, new DateTime(2025, 7, 22, 17, 1, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 827, new DateTime(2025, 7, 22, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 828, new DateTime(2025, 7, 22, 17, 25, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 829, new DateTime(2025, 7, 22, 8, 47, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 830, new DateTime(2025, 7, 22, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 831, new DateTime(2025, 7, 23, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 832, new DateTime(2025, 7, 23, 16, 53, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 833, new DateTime(2025, 7, 23, 9, 22, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 834, new DateTime(2025, 7, 23, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 835, new DateTime(2025, 7, 23, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 836, new DateTime(2025, 7, 23, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 837, new DateTime(2025, 7, 23, 8, 53, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 838, new DateTime(2025, 7, 23, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 839, new DateTime(2025, 7, 23, 9, 16, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 840, new DateTime(2025, 7, 23, 17, 15, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 841, new DateTime(2025, 7, 23, 9, 11, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 842, new DateTime(2025, 7, 23, 16, 33, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 843, new DateTime(2025, 7, 23, 8, 50, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 844, new DateTime(2025, 7, 23, 16, 48, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 845, new DateTime(2025, 7, 23, 8, 48, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 846, new DateTime(2025, 7, 23, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 847, new DateTime(2025, 7, 23, 9, 28, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 848, new DateTime(2025, 7, 23, 17, 6, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 849, new DateTime(2025, 7, 23, 9, 14, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 850, new DateTime(2025, 7, 23, 16, 35, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 851, new DateTime(2025, 7, 23, 9, 2, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 852, new DateTime(2025, 7, 23, 16, 46, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 853, new DateTime(2025, 7, 23, 9, 16, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 854, new DateTime(2025, 7, 23, 17, 18, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 855, new DateTime(2025, 7, 24, 9, 5, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 856, new DateTime(2025, 7, 24, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 857, new DateTime(2025, 7, 24, 8, 51, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 858, new DateTime(2025, 7, 24, 17, 18, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 859, new DateTime(2025, 7, 24, 8, 46, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 860, new DateTime(2025, 7, 24, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 861, new DateTime(2025, 7, 24, 9, 19, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 862, new DateTime(2025, 7, 24, 16, 39, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 863, new DateTime(2025, 7, 24, 9, 24, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 864, new DateTime(2025, 7, 24, 17, 16, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 865, new DateTime(2025, 7, 24, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 866, new DateTime(2025, 7, 24, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012") },
                    { 867, new DateTime(2025, 7, 24, 8, 56, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 868, new DateTime(2025, 7, 24, 17, 24, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 869, new DateTime(2025, 7, 24, 9, 9, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 870, new DateTime(2025, 7, 24, 17, 5, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 871, new DateTime(2025, 7, 24, 8, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 872, new DateTime(2025, 7, 24, 17, 16, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 873, new DateTime(2025, 7, 24, 9, 21, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 874, new DateTime(2025, 7, 24, 16, 49, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 875, new DateTime(2025, 7, 25, 9, 27, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 876, new DateTime(2025, 7, 25, 17, 4, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 877, new DateTime(2025, 7, 25, 9, 25, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 878, new DateTime(2025, 7, 25, 16, 38, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 879, new DateTime(2025, 7, 25, 8, 52, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 880, new DateTime(2025, 7, 25, 16, 32, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 881, new DateTime(2025, 7, 25, 8, 49, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 882, new DateTime(2025, 7, 25, 16, 51, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 883, new DateTime(2025, 7, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 884, new DateTime(2025, 7, 25, 17, 13, 0, 0, DateTimeKind.Unspecified), 1, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09") },
                    { 885, new DateTime(2025, 7, 25, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 886, new DateTime(2025, 7, 25, 16, 50, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10") },
                    { 887, new DateTime(2025, 7, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 888, new DateTime(2025, 7, 25, 17, 23, 0, 0, DateTimeKind.Unspecified), 1, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11") },
                    { 889, new DateTime(2025, 7, 25, 9, 17, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 890, new DateTime(2025, 7, 25, 17, 3, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101") },
                    { 891, new DateTime(2025, 7, 25, 9, 8, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 892, new DateTime(2025, 7, 25, 17, 27, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102") },
                    { 893, new DateTime(2025, 7, 25, 9, 20, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 894, new DateTime(2025, 7, 25, 17, 18, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103") },
                    { 895, new DateTime(2025, 7, 25, 8, 54, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") },
                    { 896, new DateTime(2025, 7, 25, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104") }
                });

            migrationBuilder.InsertData(
                table: "Ceos",
                column: "ApplicationUserId",
                value: new Guid("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801"));

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Bonus", "Notes", "PaymentDate", "RecipientId", "Salary", "Taxes" },
                values: new object[,]
                {
                    { 1, 149.66m, "Full salary + punctuality bonus", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), 2800m, 589.93m },
                    { 2, 56.36m, "Late arrivals detected, adjusted salary", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), 5000m, 1011.27m },
                    { 3, 47.19m, "One absence day deducted", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), 4700m, 949.44m },
                    { 4, 288.57m, "Early leaves adjusted, bonus for extra work", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), 6900m, 1437.71m },
                    { 5, 56.59m, "Standard payout", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), 4200m, 851.32m },
                    { 6, 136.55m, "Exceeded marketing targets", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), 6500m, 1327.31m },
                    { 7, 173.86m, "One day unpaid leave adjustment", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), 3000m, 634.77m },
                    { 8, 246.20m, "On-time delivery and initiative bonus", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), 6000m, 1249.24m },
                    { 9, 66.68m, "Strong sales pipeline growth", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101"), 4800m, 973.34m },
                    { 10, 377.58m, "Senior engineering deliverables on track", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102"), 6200m, 1315.52m },
                    { 11, 84.45m, "New hire ramp-up period", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103"), 4500m, 916.89m },
                    { 12, 104.99m, "Standard payout", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104"), 5100m, 1041.00m },
                    { 13, 113.26m, "Full salary + punctuality bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), 2800m, 582.65m },
                    { 14, 128.09m, "Late arrivals detected, adjusted salary", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), 5000m, 1025.62m },
                    { 15, 143.24m, "One absence day deducted", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), 4700m, 968.65m },
                    { 16, 143.66m, "Early leaves adjusted, bonus for extra work", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), 6900m, 1408.73m },
                    { 17, 173.86m, "Standard payout", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), 4200m, 874.77m },
                    { 18, 18.37m, "Exceeded marketing targets", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), 6500m, 1303.67m },
                    { 19, 195.39m, "One day unpaid leave adjustment", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), 3000m, 639.08m },
                    { 20, 277.05m, "On-time delivery and initiative bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), 6000m, 1255.41m },
                    { 21, 152.63m, "Strong sales pipeline growth", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101"), 4800m, 990.53m },
                    { 22, 75.49m, "Senior engineering deliverables on track", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102"), 6200m, 1255.10m },
                    { 23, 32.44m, "New hire ramp-up period", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103"), 4500m, 906.49m },
                    { 24, 287.70m, "Standard payout", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104"), 5100m, 1077.54m }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "CeoId", "ContactPhone", "CountryCode", "Name" },
                values: new object[] { 1, "123 Business Blvd, New York, NY, USA", new Guid("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801"), "+1-555-0000", "US", "Workbit Solutions Inc." });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CompanyId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Handles recruitment, payroll, employee relations, and organizational policies.", "Human Resources" },
                    { 2, 1, "Oversees infrastructure, software development, cybersecurity, and IT support.", "IT" },
                    { 3, 1, "Manages budgeting, forecasting, accounting, and financial reporting.", "Finance" },
                    { 4, 1, "Develops brand strategy, campaigns, and customer outreach initiatives.", "Marketing" },
                    { 5, 1, "Drives revenue growth through client acquisition and account management.", "Sales" }
                });

            migrationBuilder.InsertData(
                table: "DepartmentBudgets",
                columns: new[] { "Id", "BonusPool", "DateAllocated", "DepartmentId", "IsDistributed", "TotalBudget" },
                values: new object[,]
                {
                    { 1, 5000m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, 50000m },
                    { 2, 10000m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, 80000m },
                    { 3, 7000m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, 60000m },
                    { 4, 4000m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, false, 45000m }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "BaseSalary", "DepartmentId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 6000m, 2, "Develops and maintains enterprise-grade software applications.", "Software Engineer" },
                    { 2, 4500m, 1, "Manages employee onboarding, benefits, and HR compliance.", "HR Specialist" },
                    { 3, 5000m, 3, "Analyzes financial data and prepares budget reports for strategic decisions.", "Financial Analyst" },
                    { 4, 4000m, 4, "Supports marketing campaigns, communications, and brand development.", "Marketing Coordinator" },
                    { 5, 4800m, 5, "Manages client relationships and drives new business acquisition.", "Sales Representative" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ApplicationUserId", "DepartmentId", "Iban" },
                values: new object[,]
                {
                    { new Guid("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), 2, "CfDJ8GddfhDREYlNrAGHBbNJFExw98fuwmNPbkxIDdOrd2doGyTB5EjOW5rT_gLTgJkFSbGQmX1mF4NTsxwHQc4s6M0yGeX311bdhFpFX2SRMaQ_C587yHvzETW9zn5SQX-UJIj_mbewWzP9QOFAuSQqDn8GOCDl3JYU9QEnR65Xt7nH7WRe8rl0MErhSCP8Qm3DPpTTMVcoLm8vxUyikWEmvmCUFc8zq6nZxiCKr9FuCyUDl2GllnbMoYnmL21HCDLBe64Br70ruAAe0WjHXZ5MroeVrWryVXNKp3Xy_oo1C_ER0dC9H959tSnrT-Bcw6x9PA" },
                    { new Guid("b06c8a25-b13c-4d31-bb49-113aa0cc46b8"), 2, "CfDJ8GddfhDREYlNrAGHBbNJFEyssRiQ7twolNO8Ideyo2TAU9O2hjS22KIIRxifJEAPFz3isU3FZwwUeef0ObOw9mbjZCUN56-8U396tzFzmPety3ftAt7psYR1z_DlyUmDJBrJHNSaP9mNpVyWIXNTwixkm6DFrZzP-co9R1mCYcszRyBxRqI82ydKT3Qp0yfjoBZ0JuUC-56-EPaLpg2aqhUeaEYo8RQ2Nvua3RH9UbrBI2sbvOApIl395ac9o1HHFE9IqsKak4NjBAf02dGQC8b01BpEKqdlhzv_iQ8MsLhcqgpUZl_rPukTa4eJ9Ye39A" },
                    { new Guid("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), 3, "CfDJ8GddfhDREYlNrAGHBbNJFEyYtpCZYL8gtmmwCBM62WtHrQXKKlHra1SHL6CgwsUL7OoSpCZaTMRfdZUvPv-vqb-seaZz-hmEOWZCa1MnfkhoLg_hM9rQ_lx3Tcw3iTUU6M3cxWoPfHEg7hSbZ92hzzR22TEgCa7TflDA-0fjz557zJSRvwR7rrwf2k6gewDpzMu2iC2n_Fxea4elhPhdNRv007z-WFmqiu9W5mygi0p3pGTWss5hqxD6fBwivRnp0jiV44RdO4dAmImIV6bYAScv1OKYKSxuokT6-G0nTp43vxGufmo_Y7e8RSdrWoEACA" },
                    { new Guid("b23ae748-2292-4712-8778-3eb591c2f001"), 3, "CfDJ8GddfhDREYlNrAGHBbNJFEwDgIubj4Em5MouN-MXLTSigtrYB7LDV8zNp1fSRy3HEhnjjrpV_AI9vQUV_1PgTQQ9h-UwhwSo2Kej4vcXwTurOIzTRxA7fICZBB-g_QCKu7-GF8jEOx2ddmBT48Ck3clv5aci43amthc3rXQ1JnX6EabH2oRlq00VMhxHw14B0tYo56cDepJOlMmQcLZSnvfU0tH5-dodcROSJjEnjZVcEB2MXj5R931eTHJSPWJ6bdfuRWphYKsp5S4Qsoqo7F3QWK8zjQB611UORjEv6QuOYnyApubEHQNA-wNpU2sXnQ" },
                    { new Guid("befa88e8-83fc-4b64-b4ce-7de0b97b6e51"), 1, "CfDJ8GddfhDREYlNrAGHBbNJFEyX2a3kH6eXvk8CdGlnM3s6NXOGIOFQrUIyXZqvAniUG7MdmJwTqd_de1HwafnvwPd8GV9KVnQsofG6QtvTf-dlqxC_lG2iN-P_5wfyyhstxwYgFnouYQJvatHuFUOuywfB_ymAI6FvjucCNLEv87qYbJ6UDptrCEzmmxnCs7QJCVL6609LyTCyrei6nMWFVsOCoH8N7eokUFs27xP-AUEEK5e7oxvBFXdtL4MeKiZ9Ypc9Rol9WNKnpsbDY3V10kg9Q1jzzuZd8hW82z9ZhT3uJHVBanbI1WFowberaVDryg" },
                    { new Guid("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), 1, "CfDJ8GddfhDREYlNrAGHBbNJFEwJpdkUklpKHf-iSqIFUSDfMfk9BtMyQryqXiMigOmpV_glL2jfo857D6Ban9FZGZ5tR1SCPKGXpi-MUTNH_I_4HE0Av-2p3e_JcDpxJhc4w9o6s2fjv_idyyN_XsFKUWu4sYiDyGTCgIJ5D8O0kJY0lv6JtFqAmnD9DMc_kQINfChA7VfeNvx8OLEZmEspfC_btmOTKy3A_z1wHJhA158GdHQ_vlJ2KCLTrtK-N7DatcK_BpnmddoCV9gbEi67HZb3Qr26WHV89zTKi-vMibDFZYhes1eHng5Qxu-I8h5qPQ" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ApplicationUserId", "CountryCode", "Iban", "JobId", "Level" },
                values: new object[,]
                {
                    { new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), "IT", "CfDJ8GddfhDREYlNrAGHBbNJFExgsAk-R-nkY7dNupl9YX6RuOSRDJ8V62tPsGH4yXDeEvqO9yAzmy2GX1WAKrhPj0n2mkQdYaaqtYLA4zz7n8BJ_y414eqXqCKWX8Be4pYlhqaFb2NRwOAgKm2JdVNYelhWjMdPdR4OxmTWJrkSzXMcYSqP7U_5s6knfPW8DBBqkssi3DMvNQLlSU3vn3N8sWi3y2IrwHugYiKcz8iJRxkmdCbBMlLKhUEqnFsJJmXo20TMPoP0M-5JDTvRi6cSDTZY67AxmfkwJTnyycZyAXViAxvVSK7we8pyfA4KaS5M8g", 1, 1 },
                    { new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), "NL", "CfDJ8GddfhDREYlNrAGHBbNJFEyvQhVI3LPyYI0ZTu9jAZ6NH97x5I7srHb0d9853nVqVgIVVlh-zz04Do1CRqb1FQq7cHHU6cKwpWcZtx1bSC3TzPkNnuAlsYQWhaxDiiExDx3p9B5mKlJShvDIoYykhtYMn3FluY0-_zmwi9qaJYDdIALjrrea5FIaQDTwIaMv19VD8994AFMXtXMvahTkf2V8xHHCDTg0oCjk6sPZyhSwoRamFqt_j3HvjQ_TSvpy19bsl8yqoQtGOUAYmTH3DgcFHISQPcqcsgt5B8CQkx3YBEV8aGVyeOQnfFi8Ka20rg", 3, 2 },
                    { new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), "RO", "CfDJ8GddfhDREYlNrAGHBbNJFEz7sU1NotJ1Ym_JiGcFTyR4kFNtN_Xhv1kGTj2mq6G7ryqhBEv7hNAtKY-91j7_fsYLRwIvlEqO0wjU1uOP7-5CAKAHNJStYesePftqRNTZCldSmUqLwW1y2GBDul4AuX5GXLAHimCplv63FyyFA6MxrI5gl8Szt6nM9WMrbjek_EoBLYP9UF917hLxLB1lVueHYzI4MZmuc0--i2l0V-PEEieQ3gC-WnHw6Gr1L8dUfgFsq-AyDAGfyXwlWl7iH31I0miHmct6r9tmntB-Hmu-wwiAfYc8ODY4jiNYy1ewZg", 2, 3 },
                    { new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), "PL", "CfDJ8GddfhDREYlNrAGHBbNJFEwRLTyHB14zpbku1WllDb4U82f4G2BtGJ858YQbdYIaxSNsDm5wlbkYNcyfgzUV34ORP0wz1cUBWWiSsU8rrjnXo_xGF7kYEj32F1wAFh6UTFP4q9t6cnIk7u3sYs7ZmJqLEf71lkpAXHczd0rFdcO9bqe5yl3Se5iVr4pIbx3GTnz6szDxNw4ToULhwv-7-F7C5f9YhBNM_bGGip0MD4usIQ_P7dk7gmhmW-sgCkd4DBfmgtWmGlj54Bgoev4JFpGmaNpiikD8NeVEsZhVlGQk3XRhfEshvdT0hZty3v6niA", 3, 1 },
                    { new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), "IE", "CfDJ8GddfhDREYlNrAGHBbNJFEyq9PoXJnK-AmnxwAPks8-VEsRtBzU6Dy5KZ9gx1URxoQGDmS7-NB7JWGcMiCuyJoJNyr3gc4cIkHYUXSlMJ6lyu-C_V4nIZ6dQJJ95CksB6odSNjiKjyXkq1X3J0roOpLJn5hxHvhH8QA07NhiLMUMMfwOF3FYTmy6Sqh9Dz95WwNhRAVjSnNxl1eO716d_CI1tTyfWEPFao8OgDBN0WqDJc_8l1EAL3efoPotF_q0ezr52udNoF25dTP4QSgDG7up0CHl2DtNad1on9Uyq5Mv466EZ3kcLSNI1qev1uf4KA", 4, 4 },
                    { new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), "ES", "CfDJ8GddfhDREYlNrAGHBbNJFEym1L0oyMcJfHvCfdgJr7SGYpSb7GqYtxI2J9J7WIvAUIhSO1M1JXF_jkcrXq-1E6vk_8k0rS8h-S-TBRtvrclIuTfXBcyJvOa5PsIv2e3xGjDK6hsupfkmGNrS2uGsHoJU0LhGId1MVbXSke3BgvnTChO12o4QU6PNDh3yxeqXeqyS39RZIDpIS-ADL3wr7jc9EFN_carnrs8f9PB8Dzz8Zin8xaOqukq7ndg3rSyUinvgGxLIT8pCtwnfzjZ1NwRERA2b2mtTKqUnF8ewX_uCFjH7x7S6HcfjOWdzMv0xCg", 2, 4 },
                    { new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), "SE", "CfDJ8GddfhDREYlNrAGHBbNJFExStqeU7v-WLo1NtAxyGhs4WpT_4aOuQZm8TiRjpGAfxi4ApHpPXW7VW8avdliRBE8xO5nE2r8V1oVhGQPw1ot552UUGPB6ZoHI-3e9ZRQrzGpeuhzPxCMgfLDN1PmDdRa8lnvOfIt3eBDFyDE7e3F7OE9Gwj9d2-sH_oNmkHal6m8K4Wsg-3xG3fSNeZDBE9dK_3WlNHWF4n44zEKiIc3tlo42HleEcMr9vasQxEM0kMNZ_SrLSakN4Jfc7PvhOyLNPr17v-Jx9XFKq08VTb-QGrZcyk8RjdvmF5n0vMbcFQ", 4, 3 },
                    { new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111101"), "PT", "CfDJ8GddfhDREYlNrAGHBbNJFEwu8bHVbGfOeZ3qOP6Xbo39IWcuImIFbMWt0kbD6tPiCKnOj3EaqRN8-vMAwakhgKn1djxfg23jY3aEQS60QoDq5RYQnPEiFBJ9g3ctSK9sCNxJC-no8SG65uSFj65aYHqTjzKMgGTrai4U10mWKyWLZwYaRTdRLAGFseB1NFIgVfgUW0D6mI5cuiFbNQ4paNsAiMBovm-QoBlSRFFMALD6epXduUNYVHsVrxiEq_HwLUDvIHL59hubf4aXoz_MeIXN3LJ717U4f6o4E6zd6s59NMeJa7e8q6PS-ZT5tc82zw", 5, 2 },
                    { new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111102"), "DE", "CfDJ8GddfhDREYlNrAGHBbNJFExgzzEraLLObluoB5FNVja-EGdIsnr8DgSYypXLnALJEWeW5kgD50HR6dynLZJpusUOAuoVRhMLhsJDZJrwJx9uoB9KKoMfE04gdKbEE9CT0cmlY32ZKJwzF3DxwX32cUbK8z1Ln-Vwz_xBm1ia__597ewo91QvTyOL05RClO6VuwEdlXwve52aouSNvQEUbC0qsRPWF4gc0BeCp_zwD1mQm5yt2v63VdfXGFMwG1uhwY6vKfCAvJ_kH8sdAFYZ0Ie3JKiUF0ZXtcZdtvaZ4eRKRK1gTVUxsE2pW-u8tsvWXA", 1, 3 },
                    { new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111103"), "FR", "CfDJ8GddfhDREYlNrAGHBbNJFEyyFMKmrCNtrxHGcru8jrlIPMWzNbfzy4Pl1xaOvXcKRPa5SRAK9jaTOdjpDrp840_FBOjTU2K3s-ZnRpKRLXvgbiH42G4IDZdXJ0tu6qP9O55F-2QjhflytJdG5hcL9hC-a8IYjml5gtMu8iAG5VF1bHEx3bymqGkZh2PVdZEXiL0eU_mC35GjbkYcgXCaLh0nntuiWsazxn-6NGloQmuxEmPBGh9xb6BgLQnumGe4BzXiCimJtbwSE6RvpqqlFRD2Uq3Bw3XGsjGn-UCkpwbmBNcLYWEnNAG30KJNKpdjxg", 5, 1 },
                    { new Guid("e1a2b3c4-1111-4a1a-8b2b-111111111104"), "CZ", "CfDJ8GddfhDREYlNrAGHBbNJFEyz73bW3G6h5w5PjVkUnyTtJtxxeBUkrcNjF6G-wbVoZrCfrHeN0WR3lDcVN91xHHXi49LWKOk0h2LKkDMuFRr3ur-VPuLTE1Nxv8O4b1HQe2gYupr1JmNJWUyjM22gAnsZlST6kSbZEZAfQVVDa_uWsDVDEovaAv2mmZtEinUbXwEJvj1HX7B0sbAlqy1AelqbDdQtA0JnDdS9ARbRD13kZV4YZWYm95CdmTO0hJEHKnxRycjx9bCNU0xFqJ6grCZS7s_j4RzoDKnAJO_9-IAQ6jyiqFFsvN5XdD8A75zANA", 3, 2 },
                    { new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), "BG", "CfDJ8GddfhDREYlNrAGHBbNJFEwMW0yfNdVhsONnTbEcbTUJGuITmSktjgGR4nDuUvZDjcpBuV38HxSD6QsuAuh7ZaSzwJKeMJYr11WyL-n9ua2qyo7KWNmvEkhpIP9lavWZQhULR63pjwVFv9XoqYK6L9rkdyQLJtZtXj4auTSNF45jpjmpMmghZA3EYYN7sDksEPz6qBSBXSxTaG-sq_Caj-r02sKzj4KhupHrqgUGFOpml9R9JeLzLPGNY8s5tHtpe_ekLyfzHAKlpu8YH5rxGyddHpXklqAitKoYEgF_aRefascmghwmYE1y29COgfRS4g", 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceEntries_UserId",
                table: "AttendanceEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CeoId",
                table: "Companies",
                column: "CeoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryCode",
                table: "Companies",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBudgets_DepartmentId",
                table: "DepartmentBudgets",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CountryCode",
                table: "Employees",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobId",
                table: "Employees",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DepartmentId",
                table: "Jobs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_DepartmentId",
                table: "Managers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RecipientId",
                table: "Payments",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AttendanceEntries");

            migrationBuilder.DropTable(
                name: "DepartmentBudgets");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Ceos");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
