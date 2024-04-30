﻿namespace OrderAPI.Models;

public class Product
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public int Serves { get; set; }
}
