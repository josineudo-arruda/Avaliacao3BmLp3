using Avaliacao3Bimlp3.Models;
using Avaliacao3Bimlp3.Database;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Avaliacao3Bimlp3.Repositories;

class ProductRepository
{
    private DatabaseConfig databaseConfig;  
    public ProductRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    public List<Product> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        
        var products = connection.Query<Product>("SELECT * FROM Products").ToList();
        
        return products;
    }

    public Product Save(Product product)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        
        connection.Execute("INSERT INTO Products VALUES(@Id, @Name, @Price, @Active)",product);

        return product;
    }

    public void Enable(int id) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active = true WHERE id = @Id", new { Id = id });
    }

    public void Disable(int id) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active = false WHERE id = @Id", new { Id = id });
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Products WHERE id = @Id", new { Id = id });
    }

    public bool existsById(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        return Convert.ToBoolean(connection.ExecuteScalar("SELECT count(id) FROM Products WHERE id = @id;", new {id = id}));
    }

    public bool existsAnyProduct()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        return Convert.ToBoolean(connection.ExecuteScalar("SELECT count(*) FROM Products"));
    }

    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price > @initialPrice and price < @endPrice", new {initialPrice = initialPrice, endPrice = endPrice}).ToList();

        return products;
    }

    public List<Product> GetAllWithPriceHigherThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price > @price", new {price = price}).ToList();

        return products;
    }

    public List<Product> GetAllWithPriceLowerThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price < @price", new {price = price}).ToList();

        return products;
    }

    public double GetAveragePrice()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        double media = connection.ExecuteScalar<double>("SELECT AVG(price) FROM Products");

        return media;
    }
}