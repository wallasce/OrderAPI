using System.Collections.ObjectModel;

namespace OrderAPI.Models;

public class Order
{
    public int OrderId { get; set; }
    public long CPF { get; set; }
    public int Tabble { get; set; }
    
    public enum States
    {
        Open,
        Close,
    }
    public States State { get; set; }

    public ICollection<Item> Items { get; set; }

    public Order()
    {
        Items = new Collection<Item>();
    }

    public float CurrentOrderBalance { get; set; }
}
