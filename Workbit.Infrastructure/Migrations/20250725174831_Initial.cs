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

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_Payments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), 0, "a5e9dbe5-0d0b-4632-822a-2d9c7568c82c", new DateTime(1994, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.c.thomas@workbit.com", false, "Bob", "Thomas", false, null, "BOB.C.THOMAS@WORKBIT.COM", "BOB.C.THOMAS", "AQAAAAIAAYagAAAAEDPseTyxM/voE3NccltkG2zkcIImCxOaa5fz0vhtVpZ53rMlSuibX5UoufmUmOwRrg==", "+1-555-705-4484", false, "f84e98a2-48f7-4a41-bb66-6f7eb3ebcad2", false, "bob.c.thomas" },
                    { new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), 0, "904b0f31-6e41-44d7-a932-ae8b90762f48", new DateTime(1991, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.d.young@workbit.com", false, "Emily", "Young", false, null, "EMILY.D.YOUNG@WORKBIT.COM", "EMILY.D.YOUNG", "AQAAAAIAAYagAAAAELX2QKT+Rb2KWrwSYvXL54PfARBtqZt9cyZUktbtoJqWfspzuoqHQcX2A9oytNafeQ==", "+1-555-757-8426", false, "c52fe920-1cd8-4d20-8c9c-0e8321ef37c0", false, "emily.d.young" },
                    { new Guid("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), 0, "7d679701-3472-4527-969d-a9684fcca14c", new DateTime(1978, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "carl.t.morgan@workbit.com", false, "Carl", "Morgan", false, null, "CARL.T.MORGAN@WORKBIT.COM", "CARL.T.MORGAN", "AQAAAAIAAYagAAAAEE9z7CAr+vRTZIhlUu0dgsnOkh0rskCpj8oMMQXZa+FSYgPNFvO8QJCDwmi4er4NCw==", "+1-555-287-9860", false, "654cbd59-a3d0-4c01-be8f-ed7ccdcca524", false, "carl.t.morgan" },
                    { new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), 0, "a355b8c5-40b2-481d-b8cd-0bc0e42c6cd5", new DateTime(1993, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "dave.r.walker@workbit.com", false, "Dave", "Walker", false, null, "DAVE.R.WALKER@WORKBIT.COM", "DAVE.R.WALKER", "AQAAAAIAAYagAAAAEHR7NZmcnZkvqzKtVnoZsuSL1fzuoPp9QGfcngw8ZAC+vuDsSGpBEYIpUSj3f3OXeg==", "+1-555-256-9354", false, "2541ae60-e73a-46ce-b05c-3e2463393c9c", false, "dave.r.walker" },
                    { new Guid("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801"), 0, "1511a0db-2354-4c78-911c-2a369fdccd2e", new DateTime(1980, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.m.lewis@workbit.com", false, "John", "Lewis", false, null, "JOHN.M.LEWIS@WORKBIT.COM", "JOHN.M.LEWIS", "AQAAAAIAAYagAAAAELlX+Z8XR4CxnVP5UcdxE9/DizRmzuN/7tPm4RGHnmTwa9V/dK4vUXDn4sRX2WdvBQ==", "+1-555-207-9626", false, "5a18e1c2-b4b2-40f6-88ae-3153fbe100f9", false, "john.m.lewis" },
                    { new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), 0, "768def20-391a-470e-be46-0a3fe69491b1", new DateTime(1990, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "frank.h.scott@workbit.com", false, "Frank", "Scott", false, null, "FRANK.H.SCOTT@WORKBIT.COM", "FRANK.H.SCOTT", "AQAAAAIAAYagAAAAEOrgGjN5TZP/tmukRoe2sWnjngo9gPGufEwzskIwzlHJ9qHf6iJwm8k80Xjnopzxsg==", "+1-555-895-1451", false, "da337793-0a5e-4289-b2d7-89596195d428", false, "frank.h.scott" },
                    { new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), 0, "801f883c-a866-4929-83f5-92f7b1cc2a47", new DateTime(1997, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "harry.n.brooks@workbit.com", false, "Harry", "Brooks", false, null, "HARRY.N.BROOKS@WORKBIT.COM", "HARRY.N.BROOKS", "AQAAAAIAAYagAAAAEBs+26oe+APdizwLURhVtWhiHAy1pDoHm3ygfBcYJlyfyRvbFc83PV1/hWAGJ/L55Q==", "+1-555-855-3517", false, "be883009-cbbf-4f7c-b3ae-33ff9a4db053", false, "harry.n.brooks" },
                    { new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), 0, "5c84259f-2882-4a57-a378-aa35f2036b98", new DateTime(1992, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "claire.b.james@workbit.com", false, "Claire", "James", false, null, "CLAIRE.B.JAMES@WORKBIT.COM", "CLAIRE.B.JAMES", "AQAAAAIAAYagAAAAELNWNXkxsrcYOOGBUd35aKYfKJ00as7asF8h07vxfoQcq2GqV54ulAO4boqX41k8SQ==", "+1-555-273-6799", false, "f84ea8d6-6647-4c0f-be7b-502679eb1f67", false, "claire.b.james" },
                    { new Guid("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), 0, "74ad748a-9bbe-472c-8bea-fa0e6f1eaf39", new DateTime(1985, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "nina.v.hughes@workbit.com", false, "Nina", "Hughes", false, null, "NINA.V.HUGHES@WORKBIT.COM", "NINA.V.HUGHES", "AQAAAAIAAYagAAAAEJ6hP42x/9dAYRCB2oBun8vCW2Goyfm3PgIJ3L1zlj41nyk3XmVaKELyZeIIpGmTdw==", "+1-555-802-5150", false, "8647de5b-2671-4f65-b6cb-a6d34f52d82c", false, "nina.v.hughes" },
                    { new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), 0, "cf662e10-9dd9-4ce1-8c51-cad515c73bca", new DateTime(1996, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "grace.l.adams@workbit.com", false, "Grace", "Adams", false, null, "GRACE.L.ADAMS@WORKBIT.COM", "GRACE.L.ADAMS", "AQAAAAIAAYagAAAAEH6JV23XbaGXoVqV6fDKjmXouh2q7GuQ97+1hVB2GA1T4lrLbN8UrSoBVYAjPJvEpQ==", "+1-555-688-3055", false, "c1dd8dc4-d6a0-4a0c-816e-f730fb25740d", false, "grace.l.adams" },
                    { new Guid("d5e7f9a2-0ac3-4b6d-8c64-6fd8e4c0c013"), 0, "94c3a977-a04e-40f4-a8b3-b2ebf31108f6", new DateTime(1994, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", false, "Admin", "Adminov", false, null, "ADMIN@GMAIL.COM", "ADMINADMIN", "AQAAAAIAAYagAAAAEO8ZGnb+1pKQpplAHqITmp7hPo8EZmEjBCx0Fc55HI24W7YT5AH1sBIbAj0wEMUSaQ==", "+1-555-949-6831", false, "568e4590-c9a9-4793-a837-b42d25eaa254", false, "adminadmin" },
                    { new Guid("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), 0, "7aea0736-32a2-4bea-92b2-23253a711b28", new DateTime(1982, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "lisa.r.anderson@workbit.com", false, "Lisa", "Anderson", false, null, "LISA.R.ANDERSON@WORKBIT.COM", "LISA.R.ANDERSON", "AQAAAAIAAYagAAAAEEa0LC3AmmH/C+80K80euGWuIeE0vhMUkh1psyukoQcF1mfNmH7bMAc8LCuoBbe1rQ==", "+1-555-344-7064", false, "89728d99-2b88-4561-afc8-2b28e3096015", false, "lisa.r.anderson" },
                    { new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), 0, "fae898dd-78ae-48af-8bac-596fe4aa6eef", new DateTime(1995, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.k.watson@workbit.com", false, "Alice", "Watson", false, null, "ALICE.K.WATSON@WORKBIT.COM", "ALICE.K.WATSON", "AQAAAAIAAYagAAAAEJQmBodlaBTqLp/NAubN5mWUEP9XGP2rWQkR/IWZt7Z8DMytDgd8RvJ2BKEmLZW66w==", "+1-555-454-9052", false, "25b7f311-e83d-49a1-b70f-51ca153d6704", false, "alice.k.watson" }
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

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Bonus", "EmployeeId", "Notes", "PaymentDate", "Salary", "Taxes" },
                values: new object[,]
                {
                    { 1, 200m, new Guid("f92e7b0f-5123-40c8-9d28-8834a3c93005"), "Full salary + punctuality bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2800m, 100m },
                    { 2, 100m, new Guid("2b06417a-1460-4b10-8454-51069dfb2d06"), "Late arrivals detected, adjusted salary and reduced bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000m, 240m },
                    { 3, 100m, new Guid("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), "One absence day deducted", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4700m, 190m },
                    { 4, 400m, new Guid("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), "Early leaves adjusted, bonus for extra work on other days", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6900m, 280m },
                    { 5, 100m, new Guid("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), "Standard payout", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4200m, 150m },
                    { 6, 400m, new Guid("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), "Exceeded marketing targets", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6500m, 250m },
                    { 7, 0m, new Guid("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), "One day unpaid leave adjustment", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, 120m },
                    { 8, 350m, new Guid("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), "On-time delivery and initiative bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000m, 200m }
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
                name: "IX_Payments_EmployeeId",
                table: "Payments",
                column: "EmployeeId");
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
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Employees");

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
