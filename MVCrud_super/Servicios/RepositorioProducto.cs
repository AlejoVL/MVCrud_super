using Dapper;
using Microsoft.Data.SqlClient;
using MVCrud_super.Models;

namespace MVCrud_super.Servicios
{
    public interface IRepositorioProducto
    {
        Task actualizar(ProductoViewModel producto);
        Task borrar(int id);
        Task crear(ProductoViewModel producto);
        Task<ProductoViewModel> obtenerPorId(int id);
        Task<IEnumerable<ProductoViewModel>> obtenerProductos();
    }

    public class RepositorioProducto: IRepositorioProducto 
    {
        private readonly string connectionString;
        public RepositorioProducto(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ProductoViewModel>> obtenerProductos()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<ProductoViewModel>(@"SELECT id_producto AS GsIdProducto, nom_producto AS GsNombre FROM tbl_producto");
        }

        public async Task<ProductoViewModel> obtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<ProductoViewModel>(
                    @"SELECT id_producto AS GsIdProducto, nom_producto AS GsNombre 
                    FROM tbl_producto 
                    WHERE id_producto = @id", new { id }
                );
        }

        public async Task crear(ProductoViewModel producto)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.QueryAsync("sp_crear_producto", new
            {                
                nom_producto = producto.GsNombre         
            }, commandType: System.Data.CommandType.StoredProcedure);

        }

        public async Task actualizar(ProductoViewModel producto)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("sp_actualizar_producto", new
            {
                id_producto = producto.GsIdProducto,
                nom_producto = producto.GsNombre                
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("sp_eliminar_producto",
                new { id_producto = id }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
