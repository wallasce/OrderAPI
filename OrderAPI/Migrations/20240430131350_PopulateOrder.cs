using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO orders(CPF, `Table`, State, CurrentOrderBalance, Date) " +
                   "VALUES('111.222.333-44', 5, 0, 15.50, now());");
            mb.Sql("INSERT INTO orders(CPF, `Table`, State, CurrentOrderBalance, Date) " +
                   "VALUES('111.222.333-44', 7, 0, 0, now());");
            mb.Sql("INSERT INTO orders(CPF, `Table`, State, CurrentOrderBalance, Date) " +
                   "VALUES('222.111.333-45', 2, 1, 150.50, now());");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM orders");
        }
    }
}
