using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Workbit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
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
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    { new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), 0, "6d634d23-4d3b-4c96-ba0b-5b18456c8824", new DateTime(1994, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.c.thomas@workbit.com", false, "Bob", "Thomas", false, null, "BOB.C.THOMAS@WORKBIT.COM", "BOB.C.THOMAS", "AQAAAAIAAYagAAAAEJV129+yGZq61165m3nvp3WV8JvX56ZJnfEf4ZN34QdmJMk0FxGBrc78+le5KfLoJg==", "+1-555-502-2540", false, "c4d424c3-7e1a-43b2-ac21-486e489ff579", false, "bob.c.thomas" },
                    { new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), 0, "e26a37e6-d5e4-4b57-8ff0-d795a790c33e", new DateTime(1991, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.d.young@workbit.com", false, "Emily", "Young", false, null, "EMILY.D.YOUNG@WORKBIT.COM", "EMILY.D.YOUNG", "AQAAAAIAAYagAAAAEDzcngHCU79+RmDtJwuBYCadimv31tpi5PlMHurOL+mVbs1GPBDD1syqDvzpiwYTWw==", "+1-555-270-7307", false, "aa6cfaee-5bf8-4dbf-8865-a69ef9aa963a", false, "emily.d.young" },
                    { new Guid("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), 0, "e573ed64-2145-4790-8394-ad0e9b3efb61", new DateTime(1978, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "carl.t.morgan@workbit.com", false, "Carl", "Morgan", false, null, "CARL.T.MORGAN@WORKBIT.COM", "CARL.T.MORGAN", "AQAAAAIAAYagAAAAELiqp4GeJwjOPh6HQ9Ak22uK5u7cBZiB0DQxrviItbHiz480OODzSLLb5GGRwgooGw==", "+1-555-172-7142", false, "5ca783ae-7467-4795-80fc-51e5074f52c4", false, "carl.t.morgan" },
                    { new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), 0, "def2076d-02e4-49df-bc87-d1c1c1167c8c", new DateTime(1993, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "dave.r.walker@workbit.com", false, "Dave", "Walker", false, null, "DAVE.R.WALKER@WORKBIT.COM", "DAVE.R.WALKER", "AQAAAAIAAYagAAAAEJLC8PezHfEZCCD60eQDeqjxRE06cWUPrIt2Mp69PPc0Fo5n4jzNFDq5j9L38ZPAHw==", "+1-555-247-7376", false, "4bff4d1c-9825-4582-b894-5faed91cab75", false, "dave.r.walker" },
                    { new Guid("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801"), 0, "76a0d65a-5be9-4429-b610-6391cd21f4ba", new DateTime(1980, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.m.lewis@workbit.com", false, "John", "Lewis", false, null, "JOHN.M.LEWIS@WORKBIT.COM", "JOHN.M.LEWIS", "AQAAAAIAAYagAAAAENb7pa+FOqgWhsoHBJ9k3jW4ObcJIRyWGYbFbnoqtPXj9/ZvMkBO/jooDK+p3nTnuA==", "+1-555-852-6673", false, "eaa0ab80-a88c-4b59-8ccb-cee339ef7949", false, "john.m.lewis" },
                    { new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), 0, "4f0d1bbc-2e3f-4176-ae39-24002a13eb8b", new DateTime(1990, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "frank.h.scott@workbit.com", false, "Frank", "Scott", false, null, "FRANK.H.SCOTT@WORKBIT.COM", "FRANK.H.SCOTT", "AQAAAAIAAYagAAAAEKGKwUNnjf5WQh1+qdleP/GmpUiZGTl2QHHOEaqaYFdSvOJLcpb6WL7qqkfNCXbjnw==", "+1-555-180-7726", false, "ed160b5d-adeb-47c1-bd16-91e0280f3de0", false, "frank.h.scott" },
                    { new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), 0, "848b5bcb-aa8b-4886-bf16-de1a969b8350", new DateTime(1997, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "harry.n.brooks@workbit.com", false, "Harry", "Brooks", false, null, "HARRY.N.BROOKS@WORKBIT.COM", "HARRY.N.BROOKS", "AQAAAAIAAYagAAAAEJh1xV1bVCiuGVMeIgcDmuhYZPsODo8rxyjoJ4rrLr91MrTUuDJwsCWYzdurl89b1A==", "+1-555-395-4204", false, "df2807e3-b2ff-4b64-9f84-50013ef739f9", false, "harry.n.brooks" },
                    { new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), 0, "9a9d3c83-242f-481c-8887-e6e19a5bca26", new DateTime(1992, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "claire.b.james@workbit.com", false, "Claire", "James", false, null, "CLAIRE.B.JAMES@WORKBIT.COM", "CLAIRE.B.JAMES", "AQAAAAIAAYagAAAAEOm0W3OdPtPgEUWK03+Nqa+agy9F9zuMZJxvAuVuytp7BPHDpGjIorztPREwRbgbYA==", "+1-555-905-8990", false, "3a316920-b763-4913-9eeb-fc8d67f7e9b8", false, "claire.b.james" },
                    { new Guid("b06c8a25-b13c-4d31-bb49-113aa0cc46b8"), 0, "b98b6faa-3694-4878-892e-1a0d4cac94c2", new DateTime(1984, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "julia.p.schmidt@workbit.com", false, "Julia", "Schmidt", false, null, "JULIA.P.SCHMIDT@WORKBIT.COM", "JULIA.P.SCHMIDT", "AQAAAAIAAYagAAAAEDSzr5Vkb2LsiiOhqqaNL4JQEQv7jB1IC1M/4khjETZaMgpKkmvZzDz0CVvY0H9yEg==", "+1-555-710-7404", false, "dc65b802-4453-4270-a0c5-2a69750fd15d", false, "julia.p.schmidt" },
                    { new Guid("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), 0, "9bf36d01-fea1-47e5-9b06-65c5c50fdbac", new DateTime(1985, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "nina.v.hughes@workbit.com", false, "Nina", "Hughes", false, null, "NINA.V.HUGHES@WORKBIT.COM", "NINA.V.HUGHES", "AQAAAAIAAYagAAAAEH/Y0cWGq4lOd81owfqYto4FETen6AykXr9ZEx6sP9gHFdGaFVjv8Y+uGtEZnofQCw==", "+1-555-700-9671", false, "c17a4c29-e23c-4d66-ab59-c45278dbe55d", false, "nina.v.hughes" },
                    { new Guid("b23ae748-2292-4712-8778-3eb591c2f001"), 0, "9ed08cce-643c-4ac7-b4ce-70aff8e120a9", new DateTime(1981, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "antoine.c.leblanc@workbit.com", false, "Antoine", "LeBlanc", false, null, "ANTOINE.C.LEBLANC@WORKBIT.COM", "ANTOINE.C.LEBLANC", "AQAAAAIAAYagAAAAEAxJPJFEBnz+muMY2EX0v/9mKy2W3b6HB47k/NePxofdwrTbuTno+M4EiSZPMRhPIQ==", "+1-555-409-7104", false, "62c3f4f8-9037-47f6-8794-b4c6daa5a663", false, "antoine.c.leblanc" },
                    { new Guid("befa88e8-83fc-4b64-b4ce-7de0b97b6e51"), 0, "7dee7b7d-3b5a-4a29-ae72-86e8e28c253d", new DateTime(1983, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.j.evans@workbit.com", false, "Michael", "Evans", false, null, "MICHAEL.J.EVANS@WORKBIT.COM", "MICHAEL.J.EVANS", "AQAAAAIAAYagAAAAEFj00PCSumXMcBy9YNUDdi7qkcWjrtFMTFyOYDRBxGV7vyWTg7c0sbwztnR+jG+uaQ==", "+1-555-863-8858", false, "9638a9fa-81f0-41a3-983c-70ebc314bd91", false, "michael.j.evans" },
                    { new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), 0, "74f36ec2-9c3c-4a00-97f8-6efed13bef76", new DateTime(1996, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "grace.l.adams@workbit.com", false, "Grace", "Adams", false, null, "GRACE.L.ADAMS@WORKBIT.COM", "GRACE.L.ADAMS", "AQAAAAIAAYagAAAAEA/3v5OHqRbYLZEK81T41cYMprMFDalixOJgdEgQ8F3WogT+/mhUTYqOvRF6/1KjJg==", "+1-555-988-2193", false, "40471f17-7f78-497c-8ff4-ef044037cb8f", false, "grace.l.adams" },
                    { new Guid("d5e7f9a2-0ac3-4b6d-8c64-6fd8e4c0c013"), 0, "878e2639-209d-412a-8236-8392fca468ac", new DateTime(1994, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", false, "Admin", "Adminov", false, null, "ADMIN@GMAIL.COM", "ADMINADMIN", "AQAAAAIAAYagAAAAEKl7P5xE3fXz4GP3SwcyNldLWDJEKu0SumVlvnkvy3syoao678DeYt+5RlD5vTtykg==", "+1-555-129-8676", false, "06b69925-c5c2-4cbc-88e2-035205af0c28", false, "adminadmin" },
                    { new Guid("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), 0, "3758f325-f772-448d-b988-a17488bcda11", new DateTime(1982, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "lisa.r.anderson@workbit.com", false, "Lisa", "Anderson", false, null, "LISA.R.ANDERSON@WORKBIT.COM", "LISA.R.ANDERSON", "AQAAAAIAAYagAAAAEI+uwBpH51P4sesQ7hXMUCrfnAFFAFJp8kJtBjoIv0F61FuF3H3Jml/H8YCjnpap1w==", "+1-555-195-6596", false, "4b4e8ddb-af11-4d58-8d28-7fb6cbaacf25", false, "lisa.r.anderson" },
                    { new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), 0, "e6aac8bb-5f68-478b-b6ad-dd2d1e96f1fb", new DateTime(1995, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.k.watson@workbit.com", false, "Alice", "Watson", false, null, "ALICE.K.WATSON@WORKBIT.COM", "ALICE.K.WATSON", "AQAAAAIAAYagAAAAEELoFcMYwGvgbtu22IZpTbZjEop7l/eOoK0d4CN6HtQpsMIAWCw8tf6Ns3c7491rDg==", "+1-555-482-9658", false, "01f17a2a-8c44-49c2-b925-8194a45da6e2", false, "alice.k.watson" }
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
                    { 1, new DateTime(2025, 7, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 2, new DateTime(2025, 7, 14, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 3, new DateTime(2025, 7, 14, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 4, new DateTime(2025, 7, 14, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 5, new DateTime(2025, 7, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 6, new DateTime(2025, 7, 14, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 7, new DateTime(2025, 7, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 8, new DateTime(2025, 7, 14, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 9, new DateTime(2025, 7, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 10, new DateTime(2025, 7, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 11, new DateTime(2025, 7, 15, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 12, new DateTime(2025, 7, 15, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 13, new DateTime(2025, 7, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 14, new DateTime(2025, 7, 15, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 15, new DateTime(2025, 7, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 16, new DateTime(2025, 7, 15, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 17, new DateTime(2025, 7, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 18, new DateTime(2025, 7, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 19, new DateTime(2025, 7, 16, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 20, new DateTime(2025, 7, 16, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 21, new DateTime(2025, 7, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 22, new DateTime(2025, 7, 16, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 23, new DateTime(2025, 7, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 24, new DateTime(2025, 7, 17, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 25, new DateTime(2025, 7, 17, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 26, new DateTime(2025, 7, 17, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 27, new DateTime(2025, 7, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 28, new DateTime(2025, 7, 17, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 29, new DateTime(2025, 7, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 30, new DateTime(2025, 7, 17, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 31, new DateTime(2025, 7, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 32, new DateTime(2025, 7, 18, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005") },
                    { 33, new DateTime(2025, 7, 18, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 34, new DateTime(2025, 7, 18, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06") },
                    { 35, new DateTime(2025, 7, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 36, new DateTime(2025, 7, 18, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07") },
                    { 37, new DateTime(2025, 7, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") },
                    { 38, new DateTime(2025, 7, 18, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08") }
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
                    { 1, 200m, "Full salary + punctuality bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), 2800m, 100m },
                    { 2, 100m, "Late arrivals detected, adjusted salary and reduced bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), 5000m, 240m },
                    { 3, 100m, "One absence day deducted", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), 4700m, 190m },
                    { 4, 400m, "Early leaves adjusted, bonus for extra work on other days", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), 6900m, 280m },
                    { 5, 100m, "Standard payout", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), 4200m, 150m },
                    { 6, 400m, "Exceeded marketing targets", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), 6500m, 250m },
                    { 7, 0m, "One day unpaid leave adjustment", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), 3000m, 120m },
                    { 8, 350m, "On-time delivery and initiative bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), 6000m, 200m }
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
                    { 4, 1, "Develops brand strategy, campaigns, and customer outreach initiatives.", "Marketing" }
                });

            migrationBuilder.InsertData(
                table: "DepartmentBudgets",
                columns: new[] { "Id", "BonusPool", "DateAllocated", "DepartmentId", "IsDistributed", "TotalBudget" },
                values: new object[,]
                {
                    { 1, 5000m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, 50000m },
                    { 2, 10000m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, 80000m },
                    { 3, 7000m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, 60000m }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "BaseSalary", "DepartmentId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 6000m, 2, "Develops and maintains enterprise-grade software applications.", "Software Engineer" },
                    { 2, 4500m, 1, "Manages employee onboarding, benefits, and HR compliance.", "HR Specialist" },
                    { 3, 5000m, 3, "Analyzes financial data and prepares budget reports for strategic decisions.", "Financial Analyst" },
                    { 4, 4000m, 4, "Supports marketing campaigns, communications, and brand development.", "Marketing Coordinator" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ApplicationUserId", "DepartmentId", "IBAN" },
                values: new object[,]
                {
                    { new Guid("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), 2, "DE89370400440532013000" },
                    { new Guid("b06c8a25-b13c-4d31-bb49-113aa0cc46b8"), 2, "DE21100100101234567893" },
                    { new Guid("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), 3, "FR1420041010050500013M02606" },
                    { new Guid("b23ae748-2292-4712-8778-3eb591c2f001"), 3, "FR7630006000011234567890189" },
                    { new Guid("befa88e8-83fc-4b64-b4ce-7de0b97b6e51"), 1, "GB82WEST12345698765432" },
                    { new Guid("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), 1, "GB29NWBK60161331926819" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ApplicationUserId", "CountryCode", "IBAN", "JobId", "Level" },
                values: new object[,]
                {
                    { new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), "IT", "IT60X0542811101000000123456", 1, 1 },
                    { new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), "NL", "NL91ABNA0417164300", 3, 2 },
                    { new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), "RO", "RO49AAAA1B31007593840000", 2, 3 },
                    { new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), "PL", "PL61109010140000071219812874", 3, 1 },
                    { new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), "IE", "IE29AIBK93115212345678", 4, 4 },
                    { new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), "ES", "ES9121000418450200051332", 2, 4 },
                    { new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), "SE", "SE3550000000054910000003", 4, 3 },
                    { new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), "BG", "BG80BNBG96611020345678", 1, 2 }
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
