namespace OrderAPI.Models;

public class Item
{
    public int ItemID { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public string Observation { get; set; }

    public enum States
    {
        PREPARING,
        READY,
        DELIVERED,
    }

    public States state { get; set; }
}
