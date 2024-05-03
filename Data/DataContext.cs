using MySqlConnector;

namespace mvc;

public class DataContext : IDataContext
{
    private readonly MySqlConnection _sqlConnection;

    public DataContext(MySqlConnection mySqlConnection)
    {
        _sqlConnection = mySqlConnection;
    }

    public async Task<List<Producto>> ObtenProductosAsync()
    {
        await _sqlConnection.OpenAsync();

        List<Producto> products = new();
        using var command = new MySqlCommand(@"SELECT producto.id, producto.nombre, producto.precio, fabricante.nombre AS fabricante_nombre
            FROM fabricante INNER JOIN producto ON fabricante.id = producto.id_fabricante", _sqlConnection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()) 
        {
            Producto item = new()
            {
                ProductoId = Convert.ToInt32(reader["id"]),
                Nombre = reader["nombre"].ToString(),
                Precio = Convert.ToDecimal(reader["precio"]),
                Fabricante = reader["fabricante_nombre"].ToString()
            };
            products.Add(item);
        }
        return products;
    }
}