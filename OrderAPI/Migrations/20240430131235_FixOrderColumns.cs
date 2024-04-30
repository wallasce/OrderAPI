using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tabble",
                table: "Orders",
                newName: "Table");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Orders",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Table",
                table: "Orders",
                newName: "Tabble");

            migrationBuilder.AlterColumn<long>(
                name: "CPF",
                table: "Orders",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
