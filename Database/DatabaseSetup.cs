using Microsoft.Data.Sqlite;

namespace Avaliacao3Bimlp3.Database;

class DatabaseSetup 
{
    private DatabaseConfig databaseConfig;
    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig; 
        CreateTableProduct();
    }
    private void CreateTableProduct()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Products(
                id int not null primary key,
                name varchar(100) not null,
                price double not null,
                active bool not null
            );
        ";
        command.ExecuteNonQuery();
        connection.Close();
    }
}