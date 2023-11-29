﻿namespace ProductManagement.Entities;

public class Manufacturer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}