using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO items(ProductId, Quantity, State, OrderId) " +
                   "VALUES(1, 1, 2, 1)");
            mb.Sql("INSERT INTO items(ProductId, Quantity, Observation, State, OrderId) " +
                   "VALUES(2, 2, 'Com gelo e limão', 2, 1)");
            mb.Sql("INSERT INTO items(ProductId, Quantity, State, OrderId) " +
                   "VALUES(3, 2, 0, 1)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM items");
        }
    }
}
