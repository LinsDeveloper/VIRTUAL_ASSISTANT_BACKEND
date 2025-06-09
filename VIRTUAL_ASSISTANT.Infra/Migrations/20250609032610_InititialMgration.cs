using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace VIRTUAL_ASSISTANT.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InititialMgration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    SALT_KEY = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    PASSWORD = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    ADDRESS = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.USER_ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_USER_REMINDERS",
                columns: table => new
                {
                    REMINDER_ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    TITLE = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    MESSAGE = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    REMINDER_TIMER = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER_REMINDERS", x => x.REMINDER_ID);
                    table.ForeignKey(
                        name: "FK_TB_USER_REMINDERS_TB_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "TB_USER",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.NoAction);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USER_REMINDERS_USER_ID",
                table: "TB_USER_REMINDERS",
                column: "USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_USER_REMINDERS");

            migrationBuilder.DropTable(
                name: "TB_USER");
        }
    }
}
