using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using SistemaLoja.Lab12_ConexaoSQLServer;

namespace SistemaLoja
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LAB 12 - CONEXÃO SQL SERVER ===\n");

            var produtoRepo = new ProdutoRepository();
            var pedidoRepo = new PedidoRepository();

            bool continuar = true;

            while (continuar)
            {
                MostrarMenu();
                string opcao = Console.ReadLine();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            produtoRepo.ListarTodosProdutos();
                            break;

                        case "2":
                            InserirNovoProduto(produtoRepo);
                            break;

                        case "3":
                            AtualizarProdutoExistente(produtoRepo);
                            break;

                        case "4":
                            DeletarProdutoExistente(produtoRepo);
                            break;

                        case "5":
                            ListarPorCategoria(produtoRepo);
                            break;

                        case "6":
                            CriarNovoPedido(pedidoRepo);
                            break;

                        case "7":
                            ListarPedidosDeCliente(pedidoRepo);
                            break;

                        case "8":
                            DetalhesDoPedido(pedidoRepo);
                            break;

                        case "0":
                            continuar = false;
                            break;

                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"\n❌ Erro SQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Erro: {ex.Message}");
                }

                if (continuar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("\nPrograma finalizado!");
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║       MENU PRINCIPAL               ║");
            Console.WriteLine("╠════════════════════════════════════╣");
            Console.WriteLine("║  PRODUTOS                          ║");
            Console.WriteLine("║  1 - Listar todos os produtos      ║");
            Console.WriteLine("║  2 - Inserir novo produto          ║");
            Console.WriteLine("║  3 - Atualizar produto             ║");
            Console.WriteLine("║  4 - Deletar produto               ║");
            Console.WriteLine("║  5 - Listar por categoria          ║");
            Console.WriteLine("║                                    ║");
            Console.WriteLine("║  PEDIDOS                           ║");
            Console.WriteLine("║  6 - Criar novo pedido             ║");
            Console.WriteLine("║  7 - Listar pedidos de cliente     ║");
            Console.WriteLine("║  8 - Detalhes de um pedido         ║");
            Console.WriteLine("║                                    ║");
            Console.WriteLine("║  0 - Sair                          ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.Write("\nEscolha uma opção: ");
        }

        static void InserirNovoProduto(ProdutoRepository repo)
        {
            Console.WriteLine("\n=== INSERIR NOVO PRODUTO ===");

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Preço: ");
            decimal preco = decimal.Parse(Console.ReadLine());

            Console.Write("Estoque: ");
            int estoque = int.Parse(Console.ReadLine());

            Console.Write("Categoria ID: ");
            int categoriaId = int.Parse(Console.ReadLine());

            var produto = new Produto
            {
                Nome = nome,
                Preco = preco,
                Estoque = estoque,
                CategoriaId = categoriaId
            };

            repo.InserirProduto(produto);
        }

        static void AtualizarProdutoExistente(ProdutoRepository repo)
        {
            Console.WriteLine("\n=== ATUALIZAR PRODUTO ===");

            Console.Write("ID do produto: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine();

            Console.Write("Novo preço: ");
            decimal preco = decimal.Parse(Console.ReadLine());

            Console.Write("Novo estoque: ");
            int estoque = int.Parse(Console.ReadLine());

            Console.Write("Nova categoria ID: ");
            int categoriaId = int.Parse(Console.ReadLine());

            var produto = new Produto
            {
                Id = id,
                Nome = nome,
                Preco = preco,
                Estoque = estoque,
                CategoriaId = categoriaId
            };

            repo.AtualizarProduto(produto);
        }

        static void DeletarProdutoExistente(ProdutoRepository repo)
        {
            Console.WriteLine("\n=== DELETAR PRODUTO ===");

            Console.Write("ID do produto: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Tem certeza que deseja deletar? (s/n): ");
            if (Console.ReadLine().Trim().ToLower() == "s")
            {
                repo.DeletarProduto(id);
            }
        }

        static void ListarPorCategoria(ProdutoRepository repo)
        {
            Console.WriteLine("\n=== PRODUTOS POR CATEGORIA ===");

            Console.Write("Digite o ID da categoria: ");
            int categoriaId = int.Parse(Console.ReadLine());

            repo.ListarProdutosPorCategoria(categoriaId);
        }

        static void CriarNovoPedido(PedidoRepository repo)
        {
            Console.WriteLine("\n=== CRIAR NOVO PEDIDO ===");

            Console.Write("ID do cliente: ");
            int clienteId = int.Parse(Console.ReadLine());

            var itens = new List<PedidoItem>();
            decimal total = 0;

            while (true)
            {
                Console.Write("ID do produto (ou 0 para finalizar): ");
                int produtoId = int.Parse(Console.ReadLine());

                if (produtoId == 0)
                    break;

                Console.Write("Quantidade: ");
                int quantidade = int.Parse(Console.ReadLine());

                Console.Write("Preço unitário: ");
                decimal preco = decimal.Parse(Console.ReadLine());

                itens.Add(new PedidoItem
                {
                    ProdutoId = produtoId,
                    Quantidade = quantidade,
                    PrecoUnitario = preco
                });

                total += quantidade * preco;
            }

            var pedido = new Pedido
            {
                ClienteId = clienteId,
                DataPedido = DateTime.Now,
                ValorTotal = total
            };

            repo.CriarPedido(pedido, itens);
        }

        static void ListarPedidosDeCliente(PedidoRepository repo)
        {
            Console.WriteLine("\n=== PEDIDOS DO CLIENTE ===");

            Console.Write("ID do cliente: ");
            int clienteId = int.Parse(Console.ReadLine());

            repo.ListarPedidosCliente(clienteId);
        }

        static void DetalhesDoPedido(PedidoRepository repo)
        {
            Console.WriteLine("\n=== DETALHES DO PEDIDO ===");

            Console.Write("ID do pedido: ");
            int pedidoId = int.Parse(Console.ReadLine());

            repo.ObterDetalhesPedido(pedidoId);
        }
    }
}
