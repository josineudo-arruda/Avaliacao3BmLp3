namespace Avaliacao3Bimlp3.Models;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public bool Active { get; set; }
    
    public Product() {}
    public Product(int id, string name, double price, bool active)
    {
        Id = id;
        Name = name;
        Price = price;
        Active = active;
    }
}
