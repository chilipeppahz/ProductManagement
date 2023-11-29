namespace ProductManagement.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; }
}