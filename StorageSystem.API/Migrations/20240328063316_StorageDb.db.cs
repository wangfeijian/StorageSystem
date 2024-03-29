using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class StorageDbdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorageDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WbsNo = table.Column<string>(type: "TEXT", nullable: false),
                    StorageName = table.Column<string>(type: "TEXT", nullable: false),
                    MaterielDetail = table.Column<string>(type: "TEXT", nullable: false),
                    MaterielSN = table.Column<string>(type: "TEXT", nullable: false),
                    InStorageDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InFinish = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageOutDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WbsNo = table.Column<string>(type: "TEXT", nullable: false),
                    StorageName = table.Column<string>(type: "TEXT", nullable: false),
                    MaterielDetail = table.Column<string>(type: "TEXT", nullable: false),
                    MaterielSN = table.Column<string>(type: "TEXT", nullable: false),
                    InStorageDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InFinish = table.Column<bool>(type: "INTEGER", nullable: false),
                    OutStorageDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OutFinish = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageOutDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StorageName = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Account = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageDetails");

            migrationBuilder.DropTable(
                name: "StorageOutDetails");

            migrationBuilder.DropTable(
                name: "StorageStatuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
