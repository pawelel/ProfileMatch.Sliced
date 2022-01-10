﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Addedlanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamePl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionPl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamePl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionPl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamePl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionPl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "ClosedQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamePl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionPl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosedQuestions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitlePl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClosedQuestionId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionPl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerOptions_ClosedQuestions_ClosedQuestionId",
                        column: x => x.ClosedQuestionId,
                        principalTable: "ClosedQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                name: "UserCategories",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategories", x => new { x.ApplicationUserId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_UserCategories_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOpenAnswers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OpenQuestionId = table.Column<int>(type: "int", nullable: false),
                    IsDisplayed = table.Column<bool>(type: "bit", nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOpenAnswers", x => new { x.ApplicationUserId, x.OpenQuestionId });
                    table.ForeignKey(
                        name: "FK_UserOpenAnswers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOpenAnswers_OpenQuestions_OpenQuestionId",
                        column: x => x.OpenQuestionId,
                        principalTable: "OpenQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClosedAnswers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClosedQuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerOptionId = table.Column<int>(type: "int", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClosedAnswers", x => new { x.ApplicationUserId, x.ClosedQuestionId });
                    table.ForeignKey(
                        name: "FK_UserClosedAnswers_AnswerOptions_AnswerOptionId",
                        column: x => x.AnswerOptionId,
                        principalTable: "AnswerOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserClosedAnswers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClosedAnswers_ClosedQuestions_ClosedQuestionId",
                        column: x => x.ClosedQuestionId,
                        principalTable: "ClosedQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8c916fc5-5d08-4164-8594-7ac0e2b6e16a", "83256a0f-8959-4eb8-a15e-e9c74c782841", "Admin", "ADMIN" },
                    { "93877c5b-c988-4c83-b152-d0b17858f7c6", "dd434162-84c9-4f3f-acd0-aa028df9b1f4", "User", "USER" },
                    { "af138749-2fc8-4bcf-8492-fadb9e0d5415", "6d68df77-faee-4dab-bb84-4c445d4cc7a1", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DescriptionPl", "Name", "NamePl" },
                values: new object[,]
                {
                    { 1, null, null, "Programowanie", null },
                    { 2, null, null, "Sieci komputerowe", null },
                    { 3, null, null, "Obsługa komputera", null },
                    { 4, null, null, "Handel", null },
                    { 5, null, null, "Lingwistyka", null }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "DescriptionPl", "Name", "NamePl" },
                values: new object[,]
                {
                    { 1, null, null, "unassigned", null },
                    { 2, null, null, "IT", null },
                    { 3, null, null, "HR", null }
                });

            migrationBuilder.InsertData(
                table: "OpenQuestions",
                columns: new[] { "Id", "Description", "DescriptionPl", "Name", "NamePl" },
                values: new object[,]
                {
                    { 1, null, null, "Co jest dla mnie ważne w pracy?", null },
                    { 2, null, null, "Co jest ważne dla mnie osobiście?", null },
                    { 3, null, null, "Moje hobby", null },
                    { 4, null, null, "Moje inne umiejętności", null },
                    { 5, null, null, "Moje zainteresowania", null },
                    { 6, null, null, "Jakie są moje cele?", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "Gender", "IsActive", "JobTitle", "JobTitlePl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 0, "32022a5f-2c5f-48af-b481-d56ea13b3e84", new DateTime(2022, 1, 10, 14, 0, 52, 803, DateTimeKind.Local).AddTicks(7146), 1, "admin@admin.com", true, "Klark", null, false, null, null, "Kent", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEFovlfV3/F9v/Fgvsvs/PVZ+HBSBJjh/1KkxmoD1XsHbCnPgtGkhQ//PsrmezgJM/w==", null, false, null, "b9cd9603-6bb3-42c6-b03e-9f975a5f6d14", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "ClosedQuestions",
                columns: new[] { "Id", "CategoryId", "Description", "DescriptionPl", "IsActive", "Name", "NamePl" },
                values: new object[,]
                {
                    { 1, 1, "Jaka jest Twoja znajomość programowania w C#?", null, true, "C#", null },
                    { 2, 1, "Jaka jest Twoja znajomość programowania w C++?", null, true, "C++", null },
                    { 3, 1, "Jaka jest Twoja znajomość programowania w Pythonie?", null, true, "Python", null },
                    { 4, 2, "Jaka jest Twoja znajomość sieci komputerowych?", null, true, "Konfiguracja routera", null },
                    { 5, 2, "Jaka jest Twoja znajomość usługi Active Directory?", null, true, "Usługa Active Directory", null },
                    { 6, 3, "Jaka jest Twoja znajomość Hardware komputera?", null, true, "Hardware", null },
                    { 7, 3, "Jaka jest Twoja znajomość na temat instalacji systemu Windows?", null, false, "Instalacja systemu Windows", null },
                    { 8, 4, "Jaka jest Twoja znajomość obsługi programów magazynowych?", null, false, "Obsługa programu magazynowego", null }
                });

            migrationBuilder.InsertData(
                table: "AnswerOptions",
                columns: new[] { "Id", "ClosedQuestionId", "Description", "DescriptionPl", "Level" },
                values: new object[,]
                {
                    { 1, 1, "Nie znasz podstaw tego języka programowania", null, 1 },
                    { 2, 1, "Znasz podstawowe rzeczy związane z programowaniem w C#", null, 2 },
                    { 3, 1, "Potrafisz pisać proste kody w języku", null, 3 },
                    { 4, 1, "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)", null, 4 },
                    { 5, 1, "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw", null, 5 },
                    { 6, 2, "Nie znasz podstaw tego języka programowania", null, 1 },
                    { 7, 2, "Znasz podstawowe rzeczy związane z programowaniem w C++", null, 2 },
                    { 8, 2, "Potrafisz pisać proste kody w języku", null, 3 },
                    { 9, 2, "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)", null, 4 },
                    { 10, 2, "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw", null, 5 },
                    { 11, 3, "Nie znasz podstaw tego języka programowania", null, 1 },
                    { 12, 3, "Znasz podstawowe rzeczy związane z programowaniem w Pythonie", null, 2 },
                    { 13, 3, "Potrafisz pisać proste kody w języku", null, 3 },
                    { 14, 3, "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)", null, 4 },
                    { 15, 3, "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw", null, 5 },
                    { 16, 4, "Znasz podstawowe informacje na temat routera", null, 1 },
                    { 17, 4, "Potrafisz zalogować się do routera i swobodnie poruszasz się po interfejsie", null, 2 },
                    { 18, 4, "Potrafisz skonfigurować podstawowe ustawienia sieciowe w routerze", null, 3 },
                    { 19, 4, "Potrafisz skonfigurować router dla wielu urządzeń oraz zadbać o bezpieczeństwo w sieci", null, 4 },
                    { 20, 4, "Potrafisz skonfigurować router w systemie linux w trybie tekstowym", null, 5 },
                    { 21, 5, "Nie konfigurowałeś żadnej usługi Active Directory", null, 1 },
                    { 22, 5, "Instalowałeś usługę Active Directory, ale jej nie konfigurowałeś", null, 2 },
                    { 23, 5, "Potrafisz dodawać podstawowe usługi do domeny i zrobić prostą konfiguracje", null, 3 },
                    { 24, 5, "Łatwość sprawia ci surfowanie po ustawieniach sieciowych domeny, bez problemu radzisz sobie z tworzeniem domen i dodawaniem kont użytkowników lub grup", null, 4 },
                    { 25, 5, "Usługa AD jest dla ciebie chlebem powszednim i nie sprawia ci żadnych problemów", null, 5 },
                    { 26, 6, "Nigdy nie rozmontowywałeś komputera stacjonarnego lub laptopa", null, 1 },
                    { 27, 6, "Znasz podstawowe elementy składowe komputera", null, 2 },
                    { 28, 6, "Potrafisz zlokalizować i nazwać dany komponent komputera", null, 3 },
                    { 29, 6, "Radzisz sobie z montażem podzespołów komputerowych", null, 4 },
                    { 30, 6, "Bez problemu składasz od podstaw komputer i go uruchamiasz", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8c916fc5-5d08-4164-8594-7ac0e2b6e16a", "a96d7c75-47f4-409b-a4d1-03f93c105647" });

            migrationBuilder.InsertData(
                table: "UserClosedAnswers",
                columns: new[] { "ApplicationUserId", "ClosedQuestionId", "AnswerOptionId", "IsConfirmed", "LastModified" },
                values: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 1, 2, false, null });

            migrationBuilder.InsertData(
                table: "UserClosedAnswers",
                columns: new[] { "ApplicationUserId", "ClosedQuestionId", "AnswerOptionId", "IsConfirmed", "LastModified" },
                values: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 2, 4, false, null });

            migrationBuilder.InsertData(
                table: "UserClosedAnswers",
                columns: new[] { "ApplicationUserId", "ClosedQuestionId", "AnswerOptionId", "IsConfirmed", "LastModified" },
                values: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 3, 3, false, null });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_ClosedQuestionId",
                table: "AnswerOptions",
                column: "ClosedQuestionId");

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
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedQuestions_CategoryId",
                table: "ClosedQuestions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_CategoryId",
                table: "UserCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClosedAnswers_AnswerOptionId",
                table: "UserClosedAnswers",
                column: "AnswerOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClosedAnswers_ClosedQuestionId",
                table: "UserClosedAnswers",
                column: "ClosedQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOpenAnswers_OpenQuestionId",
                table: "UserOpenAnswers",
                column: "OpenQuestionId");
        }

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
                name: "UserCategories");

            migrationBuilder.DropTable(
                name: "UserClosedAnswers");

            migrationBuilder.DropTable(
                name: "UserOpenAnswers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AnswerOptions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OpenQuestions");

            migrationBuilder.DropTable(
                name: "ClosedQuestions");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
