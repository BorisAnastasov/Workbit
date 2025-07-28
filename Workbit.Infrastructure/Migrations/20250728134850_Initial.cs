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
                    CeoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
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
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Employees_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), 0, "9b7edd35-e9da-4c61-820a-37613483ffd7", new DateTime(1994, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.c.thomas@workbit.com", false, "Bob", "Thomas", false, null, "BOB.C.THOMAS@WORKBIT.COM", "BOB.C.THOMAS", "AQAAAAIAAYagAAAAEJmq8K5qi0d+GFP1lzPQb9ISlGSvE4k0+l9B9pRVFq7PIkL431cd1LGfTtlZGSmI3w==", "+1-555-833-7506", false, "9a5ac456-3af4-42d2-8ead-1729f1fb71ea", false, "bob.c.thomas" },
                    { new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), 0, "ac4b7eed-543b-4a60-b311-5d2cc98c9005", new DateTime(1991, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.d.young@workbit.com", false, "Emily", "Young", false, null, "EMILY.D.YOUNG@WORKBIT.COM", "EMILY.D.YOUNG", "AQAAAAIAAYagAAAAEHO7b3ZOp6DC0u9GdTuFyI8bUBguwlmcPbU5324S16flnxvez1DwplRSs9b2cUrzVQ==", "+1-555-900-5684", false, "ef420c90-46c2-4d94-a468-2f78c38b7f9c", false, "emily.d.young" },
                    { new Guid("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), 0, "c25bdc3f-315b-44b7-b753-246e95c1b55b", new DateTime(1978, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "carl.t.morgan@workbit.com", false, "Carl", "Morgan", false, null, "CARL.T.MORGAN@WORKBIT.COM", "CARL.T.MORGAN", "AQAAAAIAAYagAAAAEDX2dxkPd36jqHpKBa2ePuvpc3pHmu9z3D0Kiem/XH/xAaHa3cXngeKB/A8oktBgFQ==", "+1-555-528-6881", false, "5ba8bb2a-b1b9-48c2-8086-3c4e52cc8bb0", false, "carl.t.morgan" },
                    { new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), 0, "6292f197-bf15-4c18-830e-e42b48dd0c81", new DateTime(1993, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "dave.r.walker@workbit.com", false, "Dave", "Walker", false, null, "DAVE.R.WALKER@WORKBIT.COM", "DAVE.R.WALKER", "AQAAAAIAAYagAAAAENOBCQLMptWp0E49H3tIpkRGsVchqQjt6KQmxfN9yz6aaIWa0KO4KLMPG50mjf6p1Q==", "+1-555-735-3311", false, "d772792c-9a0c-4a67-94df-71da2e67d784", false, "dave.r.walker" },
                    { new Guid("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801"), 0, "589919a1-a1ab-415e-b2e8-a8d918de60f9", new DateTime(1980, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.m.lewis@workbit.com", false, "John", "Lewis", false, null, "JOHN.M.LEWIS@WORKBIT.COM", "JOHN.M.LEWIS", "AQAAAAIAAYagAAAAEAtpZ8InsMk83gt/FmdNXwUosX7nV/tvGtbrK4nVHG5FxHb8+EGDUFY+KfpZggUNBg==", "+1-555-214-6998", false, "b4ee9581-05c5-43dc-8ea2-7286670d5c60", false, "john.m.lewis" },
                    { new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), 0, "0089a000-30e6-4abb-8eee-5ae98f3d5ff4", new DateTime(1990, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "frank.h.scott@workbit.com", false, "Frank", "Scott", false, null, "FRANK.H.SCOTT@WORKBIT.COM", "FRANK.H.SCOTT", "AQAAAAIAAYagAAAAEJFsQQkdJ3m2go22Phh+GYlusdTwIgmvMQuReTCg9AtsYtdxbikCbzwWiR536NQF8A==", "+1-555-960-6850", false, "477d148d-dfed-482f-aa75-658ba69ccc11", false, "frank.h.scott" },
                    { new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), 0, "fde2b4bc-b392-456a-9e7a-854ec8564062", new DateTime(1997, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "harry.n.brooks@workbit.com", false, "Harry", "Brooks", false, null, "HARRY.N.BROOKS@WORKBIT.COM", "HARRY.N.BROOKS", "AQAAAAIAAYagAAAAEKYUYQIdCeTwME6nQvuHzSpBoWVtoVMMfImCiV8oI/yhGSKixCo9wsIKa43TB1lczA==", "+1-555-113-7310", false, "b989800f-538f-43a3-9246-793c9a938e44", false, "harry.n.brooks" },
                    { new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), 0, "13ef2b30-a95a-4a66-b6f7-d01567311b18", new DateTime(1992, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "claire.b.james@workbit.com", false, "Claire", "James", false, null, "CLAIRE.B.JAMES@WORKBIT.COM", "CLAIRE.B.JAMES", "AQAAAAIAAYagAAAAEIIdMPANa3nkcsQDiblZDbza59QIKipxBWsTGCzAFLI2MrSRcTpRczu3CbHnpfJjPQ==", "+1-555-441-6410", false, "d40eff75-310e-4123-bc10-e03a49746bf7", false, "claire.b.james" },
                    { new Guid("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), 0, "0c3c9255-f44a-477b-9991-9c55b36c505c", new DateTime(1985, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "nina.v.hughes@workbit.com", false, "Nina", "Hughes", false, null, "NINA.V.HUGHES@WORKBIT.COM", "NINA.V.HUGHES", "AQAAAAIAAYagAAAAEGVBzhc3ZOVXXmvvTdcYgNuzKsG78nRkBB4uMhqpL40eGz24DZA2HZexN1Ta/5x/ug==", "+1-555-679-4615", false, "6bd09f61-c719-4814-aee5-2ed88cd35368", false, "nina.v.hughes" },
                    { new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), 0, "4dbdb579-69a6-442a-ae10-fc673765723f", new DateTime(1996, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "grace.l.adams@workbit.com", false, "Grace", "Adams", false, null, "GRACE.L.ADAMS@WORKBIT.COM", "GRACE.L.ADAMS", "AQAAAAIAAYagAAAAEFUeYsvC2U96zQC0NCeXsLdg41dPf9cn4voEyDfyvf+FZC7qfunC+s/ClXA7JjavoQ==", "+1-555-616-5225", false, "96202423-884e-4d87-9c9e-13dbb4d8a2a9", false, "grace.l.adams" },
                    { new Guid("d5e7f9a2-0ac3-4b6d-8c64-6fd8e4c0c013"), 0, "a00ef335-d1bf-4da0-8c26-9daa76aeeae4", new DateTime(1994, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", false, "Admin", "Adminov", false, null, "ADMIN@GMAIL.COM", "ADMINADMIN", "AQAAAAIAAYagAAAAEC274G7ZVef/xePwvIfzWyLbFFuLkAx9su7EvLBYU+SkXV/b43N3ZBmM28im/e6k3g==", "+1-555-455-4853", false, "45e48525-252e-4828-85ae-f049be3c34ac", false, "adminadmin" },
                    { new Guid("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), 0, "ae2e4a5b-b631-4b4a-86fb-df5f715adc26", new DateTime(1982, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "lisa.r.anderson@workbit.com", false, "Lisa", "Anderson", false, null, "LISA.R.ANDERSON@WORKBIT.COM", "LISA.R.ANDERSON", "AQAAAAIAAYagAAAAEJvciTPnlYfvGmmN0sDO1Vbe3hHztzlXhzbpQ2yyvw/LEdEF5HKhXGuTz2ojrxpylg==", "+1-555-380-6971", false, "44545281-1a7c-4ac1-b09a-270857b02675", false, "lisa.r.anderson" },
                    { new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), 0, "8604d0df-9a3c-487e-88ae-d1cbfbbb421b", new DateTime(1995, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.k.watson@workbit.com", false, "Alice", "Watson", false, null, "ALICE.K.WATSON@WORKBIT.COM", "ALICE.K.WATSON", "AQAAAAIAAYagAAAAEKuCD9YLpFKNVcr1U08zsCQlGk2ZQGVy0m7Pi13YnVjU2izecGsf2/b4+SJUlMECtA==", "+1-555-736-9670", false, "a7012131-499d-4bd8-8c27-f0653caba574", false, "alice.k.watson" }
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
                columns: new[] { "Id", "Address", "CeoId", "ContactPhone", "Name" },
                values: new object[] { 1, "123 Business Blvd, New York, NY, USA", new Guid("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801"), "+1-555-0000", "Workbit Solutions Inc." });

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
                columns: new[] { "ApplicationUserId", "DepartmentId" },
                values: new object[,]
                {
                    { new Guid("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), 2 },
                    { new Guid("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), 3 },
                    { new Guid("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ApplicationUserId", "JobId", "Level" },
                values: new object[,]
                {
                    { new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), 1, 1 },
                    { new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), 3, 2 },
                    { new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), 2, 3 },
                    { new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), 3, 1 },
                    { new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), 4, 4 },
                    { new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), 2, 4 },
                    { new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), 4, 3 },
                    { new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), 1, 2 }
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
                name: "IX_DepartmentBudgets_DepartmentId",
                table: "DepartmentBudgets",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

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
                name: "AspNetUsers");
        }
    }
}
