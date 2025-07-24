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
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_Companies_AspNetUsers_CeoId",
                        column: x => x.CeoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    OfficePhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsCeo = table.Column<bool>(type: "bit", nullable: false)
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
                    JobId = table.Column<int>(type: "int", nullable: false),
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
                    { new Guid("a1111111-0000-0000-0000-000000000001"), 0, "59fe6724-f076-4b3a-b1c7-fc9a59b1d2fb", new DateTime(1980, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.m.lewis@workbit.com", false, "John", "Lewis", false, null, "JOHN.M.LEWIS@WORKBIT.COM", "JOHN.M.LEWIS", "AQAAAAIAAYagAAAAEDvN2GpdjMoESnhnXXulQ7phY40rnl+9xZPOBmwmt1TvyVbSOiRiu6RFGeHQH1SokQ==", null, false, "3140876e-92e0-41b0-b7a1-843785db7448", false, "john.m.lewis" },
                    { new Guid("a1111111-0000-0000-0000-000000000002"), 0, "235647cc-a312-4c73-af9b-3fc342f01981", new DateTime(1982, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "lisa.r.anderson@workbit.com", false, "Lisa", "Anderson", false, null, "LISA.R.ANDERSON@WORKBIT.COM", "LISA.R.ANDERSON", "AQAAAAIAAYagAAAAEAFlRcIcDHI6Q9wo3OeRrjtu3sCGxuhL8SWdLilD6pgR20WPQP01Xj6s7MMBZh+BXg==", null, false, "4b100e70-0c83-4b3b-ab6a-8850d2829d1f", false, "lisa.r.anderson" },
                    { new Guid("a1111111-0000-0000-0000-000000000003"), 0, "9f0ea8a7-6706-4103-8af7-ea14c1ebd6ed", new DateTime(1978, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "carl.t.morgan@workbit.com", false, "Carl", "Morgan", false, null, "CARL.T.MORGAN@WORKBIT.COM", "CARL.T.MORGAN", "AQAAAAIAAYagAAAAEA+xixyJXkjPLs6BBjyfhFTvf8H8+e6s6XtpJDARwOEqIudhAs3XnQfsfRmH8zaYGw==", null, false, "94afe5b2-d685-463d-ab40-145443581c5f", false, "carl.t.morgan" },
                    { new Guid("a1111111-0000-0000-0000-000000000004"), 0, "7e164757-f294-4873-b80b-e2415d9aa40d", new DateTime(1985, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "nina.v.hughes@workbit.com", false, "Nina", "Hughes", false, null, "NINA.V.HUGHES@WORKBIT.COM", "NINA.V.HUGHES", "AQAAAAIAAYagAAAAEIR2Zz07d4xsGfuTOize1lYuLxiwHJRYF/04ovqvhSCKWHSk/GeDmIF6lqX+MPRvvA==", null, false, "21a63f51-bfdd-425e-9373-01149599af5a", false, "nina.v.hughes" },
                    { new Guid("b2222222-0000-0000-0000-000000000001"), 0, "68386363-a0fc-49d7-8a8f-44e458fd6de6", new DateTime(1995, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.k.watson@workbit.com", false, "Alice", "Watson", false, null, "ALICE.K.WATSON@WORKBIT.COM", "ALICE.K.WATSON", "AQAAAAIAAYagAAAAEDVlCLNLaIvQAD727hnDpGm7uFlOcpqJ4hK4MCd9I4JiN3A7XheGTJ1HECYiyzoblQ==", null, false, "a526300a-f4aa-475a-a185-20355b84482b", false, "alice.k.watson" },
                    { new Guid("b2222222-0000-0000-0000-000000000002"), 0, "2f50d6fc-1866-47f3-ac87-d6f31a2f84b4", new DateTime(1994, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.c.thomas@workbit.com", false, "Bob", "Thomas", false, null, "BOB.C.THOMAS@WORKBIT.COM", "BOB.C.THOMAS", "AQAAAAIAAYagAAAAELbJ2zU9ohzCNOEIn5XHiF6U8tVHPNRly2iiNNiMvexP3s3D9p4fCMrIU0lLxrC8rQ==", null, false, "0588b0cc-d347-4872-8d95-869a0de6b66a", false, "bob.c.thomas" },
                    { new Guid("b2222222-0000-0000-0000-000000000003"), 0, "e34bacb7-188f-4225-ab12-7adb156c56ac", new DateTime(1992, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "claire.b.james@workbit.com", false, "Claire", "James", false, null, "CLAIRE.B.JAMES@WORKBIT.COM", "CLAIRE.B.JAMES", "AQAAAAIAAYagAAAAENMjG0e90IhJJaM/hBA46abq9zZbhR2mDcezMH1rzrLBbm9vMO4gtnIOTOrNj3IFkw==", null, false, "98de8148-4db1-4a0d-b984-964a6487e811", false, "claire.b.james" },
                    { new Guid("b2222222-0000-0000-0000-000000000004"), 0, "e5f6c6bb-151d-4b88-9d7e-7062cde33a10", new DateTime(1993, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "dave.r.walker@workbit.com", false, "Dave", "Walker", false, null, "DAVE.R.WALKER@WORKBIT.COM", "DAVE.R.WALKER", "AQAAAAIAAYagAAAAEAp/rCSbg1tNVeDCv7nkRITt4qZ7n7077LdkkvVVWhrOXC9ihOJV1HyC68fYQpiLaA==", null, false, "c0a90139-2d66-4741-9419-4095c7cf3a9f", false, "dave.r.walker" },
                    { new Guid("b2222222-0000-0000-0000-000000000005"), 0, "761587de-47e4-4b5a-a73c-0744afde8568", new DateTime(1991, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.d.young@workbit.com", false, "Emily", "Young", false, null, "EMILY.D.YOUNG@WORKBIT.COM", "EMILY.D.YOUNG", "AQAAAAIAAYagAAAAEE1q703JLi0gY0cly6hrd/XZoM9bMW1dveOBAd2z7m+tkbk0YYZq7SoYmIxgyk2zWg==", null, false, "11afc79f-7bfe-4356-9444-103e7980ffc5", false, "emily.d.young" },
                    { new Guid("b2222222-0000-0000-0000-000000000006"), 0, "9dca8e51-3841-4977-a1c2-ff11163c66f9", new DateTime(1990, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "frank.h.scott@workbit.com", false, "Frank", "Scott", false, null, "FRANK.H.SCOTT@WORKBIT.COM", "FRANK.H.SCOTT", "AQAAAAIAAYagAAAAEDc5JGHAYN7MwmgOKrNS1+K8PY1Ixi4ouX5pSxXwOJfGic5VUVZx6QnkHhVRUHRDwA==", null, false, "c1331569-810a-4915-850a-c4528c36a015", false, "frank.h.scott" },
                    { new Guid("b2222222-0000-0000-0000-000000000007"), 0, "7483fc9c-ce47-4793-9cce-86511cec7f60", new DateTime(1996, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "grace.l.adams@workbit.com", false, "Grace", "Adams", false, null, "GRACE.L.ADAMS@WORKBIT.COM", "GRACE.L.ADAMS", "AQAAAAIAAYagAAAAEBwSmfqOMRXXF50rgJclOdN4n4ie1lKenfsum28puwaw9fOXfNiM6+/zqFWFJSrqJA==", null, false, "036ac6ce-7acc-4cad-b083-7d1b66aa7966", false, "grace.l.adams" },
                    { new Guid("b2222222-0000-0000-0000-000000000008"), 0, "207d6214-9737-42e8-9b10-4bd6f482e5e2", new DateTime(1997, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "harry.n.brooks@workbit.com", false, "Harry", "Brooks", false, null, "HARRY.N.BROOKS@WORKBIT.COM", "HARRY.N.BROOKS", "AQAAAAIAAYagAAAAEDqojGa8nn876QhCsU+sInQCXPModce1ZpZrDA0wTZrfYEXL72KFeWnmvus5qgIH9w==", null, false, "016e33f4-4016-4407-a79d-655e25213649", false, "harry.n.brooks" }
                });

            migrationBuilder.InsertData(
                table: "AttendanceEntries",
                columns: new[] { "Id", "Timestamp", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 2, new DateTime(2025, 7, 14, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 3, new DateTime(2025, 7, 14, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 4, new DateTime(2025, 7, 14, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 5, new DateTime(2025, 7, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000003") },
                    { 6, new DateTime(2025, 7, 14, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000003") },
                    { 7, new DateTime(2025, 7, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000004") },
                    { 8, new DateTime(2025, 7, 14, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000004") },
                    { 9, new DateTime(2025, 7, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 10, new DateTime(2025, 7, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 11, new DateTime(2025, 7, 15, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 12, new DateTime(2025, 7, 15, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 13, new DateTime(2025, 7, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000003") },
                    { 14, new DateTime(2025, 7, 15, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000003") },
                    { 15, new DateTime(2025, 7, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000004") },
                    { 16, new DateTime(2025, 7, 15, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000004") },
                    { 17, new DateTime(2025, 7, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 18, new DateTime(2025, 7, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 19, new DateTime(2025, 7, 16, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 20, new DateTime(2025, 7, 16, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 21, new DateTime(2025, 7, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000004") },
                    { 22, new DateTime(2025, 7, 16, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000004") },
                    { 23, new DateTime(2025, 7, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 24, new DateTime(2025, 7, 17, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 25, new DateTime(2025, 7, 17, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 26, new DateTime(2025, 7, 17, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 27, new DateTime(2025, 7, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000003") },
                    { 28, new DateTime(2025, 7, 17, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000003") },
                    { 29, new DateTime(2025, 7, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000004") },
                    { 30, new DateTime(2025, 7, 17, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000004") },
                    { 31, new DateTime(2025, 7, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 32, new DateTime(2025, 7, 18, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000001") },
                    { 33, new DateTime(2025, 7, 18, 9, 45, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 34, new DateTime(2025, 7, 18, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000002") },
                    { 35, new DateTime(2025, 7, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000003") },
                    { 36, new DateTime(2025, 7, 18, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000003") },
                    { 37, new DateTime(2025, 7, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b2222222-0000-0000-0000-000000000004") },
                    { 38, new DateTime(2025, 7, 18, 16, 45, 0, 0, DateTimeKind.Unspecified), 1, new Guid("b2222222-0000-0000-0000-000000000004") }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "CeoId", "ContactPhone", "Name" },
                values: new object[] { 1, "123 Business Blvd, New York, NY, USA", new Guid("a1111111-0000-0000-0000-000000000001"), "+1-555-0000", "Workbit Solutions Inc." });

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
                columns: new[] { "ApplicationUserId", "DepartmentId", "IsCeo", "OfficePhone" },
                values: new object[,]
                {
                    { new Guid("a1111111-0000-0000-0000-000000000001"), 1, true, "+1-555-1010" },
                    { new Guid("a1111111-0000-0000-0000-000000000002"), 1, false, "+1-555-2020" },
                    { new Guid("a1111111-0000-0000-0000-000000000003"), 2, false, "+1-555-3030" },
                    { new Guid("a1111111-0000-0000-0000-000000000004"), 3, false, "+1-555-4040" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ApplicationUserId", "JobId", "Level" },
                values: new object[,]
                {
                    { new Guid("b2222222-0000-0000-0000-000000000001"), 1, 1 },
                    { new Guid("b2222222-0000-0000-0000-000000000002"), 1, 0 },
                    { new Guid("b2222222-0000-0000-0000-000000000003"), 2, 3 },
                    { new Guid("b2222222-0000-0000-0000-000000000004"), 2, 2 },
                    { new Guid("b2222222-0000-0000-0000-000000000005"), 3, 1 },
                    { new Guid("b2222222-0000-0000-0000-000000000006"), 3, 0 },
                    { new Guid("b2222222-0000-0000-0000-000000000007"), 4, 2 },
                    { new Guid("b2222222-0000-0000-0000-000000000008"), 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Bonus", "EmployeeId", "Notes", "PaymentDate", "Salary", "Taxes" },
                values: new object[,]
                {
                    { 1, 200m, new Guid("b2222222-0000-0000-0000-000000000001"), "Full salary + punctuality bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2800m, 100m },
                    { 2, 100m, new Guid("b2222222-0000-0000-0000-000000000002"), "Late arrivals detected, adjusted salary and reduced bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000m, 240m },
                    { 3, 100m, new Guid("b2222222-0000-0000-0000-000000000003"), "One absence day deducted", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4700m, 190m },
                    { 4, 400m, new Guid("b2222222-0000-0000-0000-000000000004"), "Early leaves adjusted, bonus for extra work on other days", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6900m, 280m },
                    { 5, 100m, new Guid("b2222222-0000-0000-0000-000000000005"), "Standard payout", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4200m, 150m },
                    { 6, 400m, new Guid("b2222222-0000-0000-0000-000000000006"), "Exceeded marketing targets", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6500m, 250m },
                    { 7, 0m, new Guid("b2222222-0000-0000-0000-000000000007"), "One day unpaid leave adjustment", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, 120m },
                    { 8, 350m, new Guid("b2222222-0000-0000-0000-000000000008"), "On-time delivery and initiative bonus", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000m, 200m }
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
                column: "CeoId");

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
                name: "AspNetUsers");
        }
    }
}
