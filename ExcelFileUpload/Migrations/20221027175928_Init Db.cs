using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelFileUpload.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserID = table.Column<int>(type: "int", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserID = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ExcelFiles",
                columns: table => new
                {
                    ExcelFileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserID = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserID = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelFiles", x => x.ExcelFileID);
                    table.ForeignKey(
                        name: "FK_ExcelFiles_Users_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExcelFiles_Users_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "IsActive", "Title" },
                values: new object[] { 1, false, "Super Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "IsActive", "Title" },
                values: new object[] { 2, false, "Sub Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedByUserID", "CreatedDateTime", "Email", "FirstName", "ImageName", "IsActive", "LastName", "Password", "RoleID", "UpdatedByUserID", "UpdatedDateTime" },
                values: new object[] { 1, null, null, "super_admin@stoke.com", "Super", "user-default.png", true, "Admin", "s8scmb168Cftrf3LG8cFMjQRHk8LXGSAC9iYfOIBymK3f1Jx/wY6Tpt7jccy2MWd17vta8mAcP74Eg+BFzOQew==", 1, null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedByUserID", "CreatedDateTime", "Email", "FirstName", "ImageName", "IsActive", "LastName", "Password", "RoleID", "UpdatedByUserID", "UpdatedDateTime" },
                values: new object[] { 2, null, null, "sub_admin@stoke.com", "Sub", "user-default.png", true, "Admin", "s8scmb168Cftrf3LG8cFMjQRHk8LXGSAC9iYfOIBymK3f1Jx/wY6Tpt7jccy2MWd17vta8mAcP74Eg+BFzOQew==", 2, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ExcelFiles_CreatedByUserID",
                table: "ExcelFiles",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ExcelFiles_UpdatedByUserID",
                table: "ExcelFiles",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedByUserID",
                table: "Users",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelFiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
