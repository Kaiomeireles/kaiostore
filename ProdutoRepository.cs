using Microsoft.Data.SqlClient;
using System;

namespace SistemaLoja.Lab12_ConexaoSQLServer
{
    public class ProdutoRepository
    {
        // EXERCÍCIO 1: Listar todos os produtos
        public void ListarTodosProdutos()
        {
            string sql = "SELECT * FROM Produtos";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, " +
                                          $"Preço: R${reader["Preco"]}, Estoque: {reader["Estoque"]}");
                    }
                }
            }
        }

        // EXERCÍCIO 2: Inserir novo produto
        public void InserirProduto(Produto produto)
        {
            string sql = "INSERT INTO Produtos (Nome, Preco, Estoque, CategoriaId) " +
                         "VALUES (@Nome, @Preco, @Estoque, @CategoriaId)";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@Estoque", produto.Estoque);
                    cmd.Parameters.AddWithValue("@CategoriaId", produto.CategoriaId);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("✅ Produto inserido com sucesso!");
                }
            }
        }

        // EXERCÍCIO 3: Atualizar produto
        public void AtualizarProduto(Produto produto)
        {
            string sql = "UPDATE Produtos SET " +
                         "Nome = @Nome, Preco = @Preco, Estoque = @Estoque, CategoriaId = @CategoriaId " +
                         "WHERE Id = @Id";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", produto.Id);
                    cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@Estoque", produto.Estoque);
                    cmd.Parameters.AddWithValue("@CategoriaId", produto.CategoriaId);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("✅ Produto atualizado com sucesso!");
                }
            }
        }

        // EXERCÍCIO 4: Deletar produto
        public void DeletarProduto(int id)
        {
            string sql = "DELETE FROM Produtos WHERE Id = @Id";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("✅ Produto deletado com sucesso!");
                }
            }
        }

        // EXERCÍCIO 5: Buscar produto por ID
        public Produto BuscarPorId(int id)
        {
            string sql = "SELECT * FROM Produtos WHERE Id = @Id";
            Produto produto = null;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            produto = new Produto
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nome = reader["Nome"].ToString(),
                                Preco = Convert.ToDecimal(reader["Preco"]),
                                Estoque = Convert.ToInt32(reader["Estoque"]),
                                CategoriaId = Convert.ToInt32(reader["CategoriaId"])
                            };
                        }
                    }
                }
            }

            return produto;
        }

        // EXERCÍCIO 6: Listar produtos por categoria
        public void ListarProdutosPorCategoria(int categoriaId)
        {
            string sql = @"SELECT p.*, c.Nome as NomeCategoria 
                           FROM Produtos p
                           INNER JOIN Categorias c ON p.CategoriaId = c.Id
                           WHERE p.CategoriaId = @CategoriaId";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoriaId", categoriaId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine($"Produtos da Categoria #{categoriaId}:\n");

                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, " +
                                              $"Preço: R${reader["Preco"]}, Estoque: {reader["Estoque"]}, Categoria: {reader["NomeCategoria"]}");
                        }
                    }
                }
            }
        }

        // DESAFIO 1: Buscar produtos com estoque baixo
        public void ListarProdutosEstoqueBaixo(int quantidadeMinima)
        {
            string sql = "SELECT * FROM Produtos WHERE Estoque < @Min";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Min", quantidadeMinima);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine($"Produtos com estoque abaixo de {quantidadeMinima}:\n");

                        while (reader.Read())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"⚠️ ID: {reader["Id"]}, Nome: {reader["Nome"]}, Estoque: {reader["Estoque"]}");
                            Console.ResetColor();
                        }
                    }
                }
            }
        }

        // DESAFIO 2: Buscar produtos por nome (LIKE)
        public void BuscarProdutosPorNome(string termoBusca)
        {
            string sql = "SELECT * FROM Produtos WHERE Nome LIKE @Busca";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Busca", "%" + termoBusca + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine($"Resultados para \"{termoBusca}\":\n");

                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, " +
                                              $"Preço: R${reader["Preco"]}, Estoque: {reader["Estoque"]}");
                        }
                    }
                }
            }
        }
    }
}
