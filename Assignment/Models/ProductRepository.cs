using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Assignment.Models;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    // Fetch all products asynchronously
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        using (var connection = Connection as SqlConnection) // Cast to SqlConnection
        {
            await connection.OpenAsync();  // Open the connection asynchronously
            return await connection.QueryAsync<Product>("SELECT * FROM Products");
        }
    }

    // Add a new product to the database asynchronously
    public async Task AddProductAsync(Product product)
    {
        using (var connection = Connection as SqlConnection) // Cast to SqlConnection
        {
            await connection.OpenAsync();  // Open the connection asynchronously
            var query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
            await connection.ExecuteAsync(query, new { product.Name, product.Price });
        }
    }

    // Fetch a product by its ID asynchronously
    public async Task<Product> GetProductByIdAsync(int id)
    {
        using (var connection = Connection as SqlConnection) // Cast to SqlConnection
        {
            await connection.OpenAsync();  // Open the connection asynchronously
            return await connection.QuerySingleOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
        }
    }

    // Update an existing product asynchronously
    public async Task UpdateProductAsync(Product product)
    {
        using (var connection = Connection as SqlConnection) // Cast to SqlConnection
        {
            await connection.OpenAsync();  // Open the connection asynchronously
            var query = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
            await connection.ExecuteAsync(query, product);
        }
    }

    // Delete a product by its ID asynchronously
    public async Task DeleteProductAsync(int id)
    {
        using (var connection = Connection as SqlConnection) // Cast to SqlConnection
        {
            await connection.OpenAsync();  // Open the connection asynchronously
            var query = "DELETE FROM Products WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}

