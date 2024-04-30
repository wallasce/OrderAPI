using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO products(CategoryID, Name, Description, Price, Serves) " + 
                "VALUES(1, 'Dadinho de Tapioca', 'Acompanha Geleia de Pimenta', 30, 2)");
            mb.Sql("INSERT INTO products(CategoryID, Name, Description, Price, Serves) " +
                "VALUES(2, 'Guarana Antártica Zero', 'Lata de 350ml', 5, 1)");
            mb.Sql("INSERT INTO products(CategoryID, Name, Description, Price, Serves) " +
                "VALUES(3, 'Cheesecake', 'Autentica torta gelada americana', 15, 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM products");
        }
    }
}
