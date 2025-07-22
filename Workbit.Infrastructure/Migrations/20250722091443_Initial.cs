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
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
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
                name: "Managers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficePhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
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
                        name: "FK_Companies_Managers_CeoId",
                        column: x => x.CeoId,
                        principalTable: "Managers",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentsManagers",
                columns: table => new
                {
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsManagers", x => new { x.ManagerId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_DepartmentsManagers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentsManagers_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckIn = table.Column<TimeSpan>(type: "time", nullable: true),
                    CheckOut = table.Column<TimeSpan>(type: "time", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfPayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Deduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryPayments_Employees_EmployeeId",
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
                    { new Guid("a1111111-0000-0000-0000-000000000001"), 0, "c8e226f9-c150-483a-9ac8-0a4c18ab152d", new DateTime(1980, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.m.lewis@workbit.com", false, "John", "Lewis", false, null, "JOHN.M.LEWIS@WORKBIT.COM", "JOHN.M.LEWIS", "AQAAAAIAAYagAAAAEIQ7pvzzu+KohgeiU/g7KXQG4ybj5F71V9+kkrCneYC+jinnwArKK3BoYmMIsZytgQ==", null, false, "9d6e951a-3572-4ac9-adb5-3177b22730eb", false, "john.m.lewis" },
                    { new Guid("a1111111-0000-0000-0000-000000000002"), 0, "9097c60e-a0b7-4dc2-956f-75760fd7be31", new DateTime(1982, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "lisa.r.anderson@workbit.com", false, "Lisa", "Anderson", false, null, "LISA.R.ANDERSON@WORKBIT.COM", "LISA.R.ANDERSON", "AQAAAAIAAYagAAAAEAbrhhJ1mLEjkStRsVAmWumDjTV+bm/20Spo5jF8saDxCYBtZBjxNl/zwCEp7y9ouA==", null, false, "2dc5a46d-da32-4fcb-964e-900c2d1b06c1", false, "lisa.r.anderson" },
                    { new Guid("a1111111-0000-0000-0000-000000000003"), 0, "cb32aee3-533e-41af-9990-8d86758c3807", new DateTime(1978, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "carl.t.morgan@workbit.com", false, "Carl", "Morgan", false, null, "CARL.T.MORGAN@WORKBIT.COM", "CARL.T.MORGAN", "AQAAAAIAAYagAAAAEIVhvJkvnn/dhQKVvPAjCi3qfu5pkG40CcQYiiE0WbBwMrmulv0W83AlMtcc3pyHIg==", null, false, "355ca936-3fc1-41ad-9c6f-44853a9f7835", false, "carl.t.morgan" },
                    { new Guid("a1111111-0000-0000-0000-000000000004"), 0, "e0ad2407-8413-455a-bb6f-d78e46ec4a0d", new DateTime(1985, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "nina.v.hughes@workbit.com", false, "Nina", "Hughes", false, null, "NINA.V.HUGHES@WORKBIT.COM", "NINA.V.HUGHES", "AQAAAAIAAYagAAAAEFIFZMkEvAWqDuVSFFNTtBmuBhIEaoNvOS9+3vnte1tMM7V9ezqoeTexRrwXDIDoFQ==", null, false, "7ad8c6eb-0067-4b68-ac87-08da05e40752", false, "nina.v.hughes" },
                    { new Guid("b2222222-0000-0000-0000-000000000001"), 0, "0501dde6-51b7-4e44-aeb6-88dbe6a85bd2", new DateTime(1995, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.k.watson@workbit.com", false, "Alice", "Watson", false, null, "ALICE.K.WATSON@WORKBIT.COM", "ALICE.K.WATSON", "AQAAAAIAAYagAAAAEG1TSH7XWgY5FF31EY6h+kpR4Ta23xY1QvB755wHhGba2Vfj8m/xXS+i644MxcNqdw==", null, false, "24cea4b7-8812-424a-9b79-dfbe8a6e6bb2", false, "alice.k.watson" },
                    { new Guid("b2222222-0000-0000-0000-000000000002"), 0, "a53e3146-0ab0-454b-8002-74f3db28384c", new DateTime(1994, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.c.thomas@workbit.com", false, "Bob", "Thomas", false, null, "BOB.C.THOMAS@WORKBIT.COM", "BOB.C.THOMAS", "AQAAAAIAAYagAAAAEGu7+22uiozvaG1msGUev5wbaAYOJn35Eb5mT89BFglX2k267WLJezwNor2t+WaKsA==", null, false, "b2b50cea-c55a-4ea9-b95f-807a7d9b954a", false, "bob.c.thomas" },
                    { new Guid("b2222222-0000-0000-0000-000000000003"), 0, "5f92ddfc-2a08-4050-b732-a9c6d444d49e", new DateTime(1992, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "claire.b.james@workbit.com", false, "Claire", "James", false, null, "CLAIRE.B.JAMES@WORKBIT.COM", "CLAIRE.B.JAMES", "AQAAAAIAAYagAAAAEO7lDxERAAi+BWl7ZdUW8Mp0L5w10/mWrtlhy2HuG92I5ZsbHbpEaGrIXY3tBTR20g==", null, false, "cb2d33bb-4556-4cb6-8f39-dddca22980a3", false, "claire.b.james" },
                    { new Guid("b2222222-0000-0000-0000-000000000004"), 0, "2fcc1130-5ac4-44ec-8a28-accbc1408758", new DateTime(1993, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "dave.r.walker@workbit.com", false, "Dave", "Walker", false, null, "DAVE.R.WALKER@WORKBIT.COM", "DAVE.R.WALKER", "AQAAAAIAAYagAAAAED++p+LNKNh25YRFGNCJWSTUTNFEvzqvYxkk5DDcvm1YQx9qNUFMbFMK4DCWiL0JYA==", null, false, "4e716858-6945-422e-97cc-232a7b3cc9db", false, "dave.r.walker" },
                    { new Guid("b2222222-0000-0000-0000-000000000005"), 0, "ed99e21d-6d9d-4bba-a941-09f5769db8ec", new DateTime(1991, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.d.young@workbit.com", false, "Emily", "Young", false, null, "EMILY.D.YOUNG@WORKBIT.COM", "EMILY.D.YOUNG", "AQAAAAIAAYagAAAAEOnbulCB1c8tW5qoMxWKccBmaFmIcBGP6z+9z3KBfgYN9xNB5/izKBARIwoRr9ZODQ==", null, false, "72cc3c1d-c3e7-4baf-ba52-c395c0d08f19", false, "emily.d.young" },
                    { new Guid("b2222222-0000-0000-0000-000000000006"), 0, "a56879eb-b89a-4a68-a1e6-ee6f08cdc59a", new DateTime(1990, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "frank.h.scott@workbit.com", false, "Frank", "Scott", false, null, "FRANK.H.SCOTT@WORKBIT.COM", "FRANK.H.SCOTT", "AQAAAAIAAYagAAAAEEX+9Hm115SkShfZSBLzQm4v2L7AOGDl3dzByIf2aPJ2VfBM0V9AFQ4ixqo3yXhGeA==", null, false, "293f7132-fe3c-49eb-8691-91ce88b09bdd", false, "frank.h.scott" },
                    { new Guid("b2222222-0000-0000-0000-000000000007"), 0, "c50ca3d8-a2c4-43df-82d4-5ff5aa9370fe", new DateTime(1996, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "grace.l.adams@workbit.com", false, "Grace", "Adams", false, null, "GRACE.L.ADAMS@WORKBIT.COM", "GRACE.L.ADAMS", "AQAAAAIAAYagAAAAEEer4oYCmf3GDMBqtYwRaU1sDTXD8ZM85r4WT0S1sPes9tEKzlsagHK6/ceuTQgKzg==", null, false, "c4fae092-ca90-4d01-9754-484bb0e56b0a", false, "grace.l.adams" },
                    { new Guid("b2222222-0000-0000-0000-000000000008"), 0, "bceaded9-9003-450d-bb73-a626ef456349", new DateTime(1997, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "harry.n.brooks@workbit.com", false, "Harry", "Brooks", false, null, "HARRY.N.BROOKS@WORKBIT.COM", "HARRY.N.BROOKS", "AQAAAAIAAYagAAAAEHDuY4WOYt9aRSJ2tDbs2IslSBDuci+ZB42CQ+EObsYW4EZLwTYC6swaFFOpX5MRJg==", null, false, "e067503b-0279-4a70-aab2-8783f7a45493", false, "harry.n.brooks" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Handles recruitment, payroll, and employee relations.", "Human Resources" },
                    { 2, "Manages infrastructure, development, and support.", "IT" },
                    { 3, "Responsible for budgeting, forecasting, and reporting.", "Finance" },
                    { 4, "Handles brand strategy and campaigns.", "Marketing" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "BaseSalary", "DepartmentId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 70000m, 2, "Develops and maintains applications.", "Software Engineer" },
                    { 2, 50000m, 1, "Manages hiring and employee support.", "HR Specialist" },
                    { 3, 60000m, 3, "Prepares financial reports and analysis.", "Financial Analyst" },
                    { 4, 45000m, 4, "Supports campaigns and communications.", "Marketing Coordinator" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ApplicationUserId", "IsCeo", "OfficePhone" },
                values: new object[,]
                {
                    { new Guid("a1111111-0000-0000-0000-000000000001"), true, "+1-555-1010" },
                    { new Guid("a1111111-0000-0000-0000-000000000002"), false, "+1-555-2020" },
                    { new Guid("a1111111-0000-0000-0000-000000000003"), false, "+1-555-3030" },
                    { new Guid("a1111111-0000-0000-0000-000000000004"), false, "+1-555-4040" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "CeoId", "ContactPhone", "Name" },
                values: new object[] { 1, "123 Business Blvd, New York, NY, USA", new Guid("a1111111-0000-0000-0000-000000000001"), "+1-555-0000", "Workbit Solutions Inc." });

            migrationBuilder.InsertData(
                table: "DepartmentsManagers",
                columns: new[] { "DepartmentId", "ManagerId" },
                values: new object[,]
                {
                    { 1, new Guid("a1111111-0000-0000-0000-000000000002") },
                    { 2, new Guid("a1111111-0000-0000-0000-000000000003") },
                    { 3, new Guid("a1111111-0000-0000-0000-000000000004") }
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
                table: "Attendances",
                columns: new[] { "Id", "CheckIn", "CheckOut", "Date", "EmployeeId", "Status" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2222222-0000-0000-0000-000000000001"), 0 },
                    { 2, new TimeSpan(0, 9, 30, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2222222-0000-0000-0000-000000000002"), 1 },
                    { 3, null, null, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2222222-0000-0000-0000-000000000003"), 2 },
                    { 4, new TimeSpan(0, 8, 45, 0, 0), new TimeSpan(0, 16, 30, 0, 0), new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2222222-0000-0000-0000-000000000004"), 0 },
                    { 5, null, null, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2222222-0000-0000-0000-000000000005"), 2 },
                    { 6, new TimeSpan(0, 9, 15, 0, 0), new TimeSpan(0, 17, 15, 0, 0), new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2222222-0000-0000-0000-000000000006"), 1 },
                    { 7, new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2222222-0000-0000-0000-000000000007"), 0 },
                    { 8, null, null, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2222222-0000-0000-0000-000000000008"), 2 }
                });

            migrationBuilder.InsertData(
                table: "SalaryPayments",
                columns: new[] { "Id", "Bonus", "DateOfPayment", "Deduction", "EmployeeId", "Notes", "Payment" },
                values: new object[,]
                {
                    { 1, 200m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m, new Guid("b2222222-0000-0000-0000-000000000001"), "Monthly salary + punctuality bonus", 2800m },
                    { 2, 300m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, new Guid("b2222222-0000-0000-0000-000000000002"), "Excellent performance", 5200m },
                    { 3, 150m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50m, new Guid("b2222222-0000-0000-0000-000000000003"), null, 4800m },
                    { 4, 500m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m, new Guid("b2222222-0000-0000-0000-000000000004"), "Late project delivery", 7000m },
                    { 5, 100m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, new Guid("b2222222-0000-0000-0000-000000000005"), null, 4200m },
                    { 6, 400m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m, new Guid("b2222222-0000-0000-0000-000000000006"), "Exceeded marketing targets", 6500m },
                    { 7, 0m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50m, new Guid("b2222222-0000-0000-0000-000000000007"), "One day unpaid leave", 3000m },
                    { 8, 350m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, new Guid("b2222222-0000-0000-0000-000000000008"), "On-time delivery and initiative", 6000m }
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
                name: "IX_Attendances_EmployeeId",
                table: "Attendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CeoId",
                table: "Companies",
                column: "CeoId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsManagers_DepartmentId",
                table: "DepartmentsManagers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobId",
                table: "Employees",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DepartmentId",
                table: "Jobs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayments_EmployeeId",
                table: "SalaryPayments",
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
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "DepartmentsManagers");

            migrationBuilder.DropTable(
                name: "SalaryPayments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
