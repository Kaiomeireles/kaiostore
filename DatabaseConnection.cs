using Microsoft.Data.SqlClient;

namespace SistemaLoja.Lab12_ConexaoSQLServer
{
    public class DatabaseConnection
    {
        // Substitua os valores pelos dados reais do seu SQL Server
        private static string connectionString =
            "Server=localhost,1433;" +
            "Database=SistemaLojaDB;" +
            "User Id=sa;" +
            "Password=SuaSenhaSeguraAqui;" +
            "TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
