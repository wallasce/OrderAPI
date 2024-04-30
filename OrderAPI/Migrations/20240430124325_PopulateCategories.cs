using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO categories(Name) VALUES('Entradas');");
            mb.Sql("INSERT INTO categories(Name) VALUES('Bebidas');");
            mb.Sql("INSERT INTO categories(Name) VALUES('Sobremesa');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM categories");
        }
    }
}
