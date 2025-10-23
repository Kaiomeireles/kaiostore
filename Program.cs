using System;
using System.Collections.Generic;

namespace ProjetoPedido
{
    class Program
    {
        static ProdutoRepository produtoRepo = new ProdutoRepository();
        static PedidoRepository pedidoRepo = new PedidoRepository();
        static List<Cliente> clientes = new List<Cliente>();
        static int idCliente = 1;
        static int idProduto = 1;
        static int idPedido = 1;

        static void Main(string[] args)
        {
            int opcao;

            do
            {
                Console.WriteLine("\n=== MENU PRINCIPAL ===");
                Console.WriteLine("1. Cadastrar cliente");
                Console.WriteLine("2. Cadastrar produto");
                Console.WriteLine("3. Realizar pedido");
                Console.WriteLine("4. Visualizar pedidos");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        CadastrarCliente();
                        break;
                    case 2:
                        CadastrarProduto();
                        break;
                    case 3:
                        RealizarPedido();
                        break;
                    case 4:
                        VisualizarPedidos();
                        break;
                    case 5:
                        Console.WriteLine("Encerrando o programa...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcao != 5);
        }

        static void CadastrarCliente()
        {
            Console.WriteLine("\n=== Cadastro de Cliente ===");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();

            var cliente = new Cliente(idCliente++, nome, email, telefone);
            clientes.Add(cliente);

            Console.WriteLine("Cliente cadastrado com sucesso!");
        }

        static void CadastrarProduto()
        {
            Console.WriteLine("\n=== Cadastro de Produto ===");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Preço: ");
            decimal preco = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Categorias:");
            foreach (var cat in Enum.GetValues(typeof(Categoria)))
            {
                Console.WriteLine($"{(int)cat} - {cat}");
            }
            Console.Write("Escolha uma categoria: ");
            Categoria categoria = (Categoria)int.Parse(Console.ReadLine());

            var produto = new Produto(idProduto++, nome, preco, categoria);
            produtoRepo.AdicionarProduto(produto);

            Console.WriteLine("Produto cadastrado com sucesso!");
        }

        static void RealizarPedido()
        {
            Console.WriteLine("\n=== Realizar Pedido ===");

            if (clientes.Count == 0 || produtoRepo.ObterTodos().Count == 0)
            {
                Console.WriteLine("É necessário cadastrar pelo menos um cliente e um produto antes.");
                return;
            }

            Console.WriteLine("Clientes disponíveis:");
            foreach (var cliente in clientes)
            {
                Console.WriteLine(cliente);
            }
            Console.Write("Digite o ID do cliente: ");
            int id = int.Parse(Console.ReadLine());
            var clienteSelecionado = clientes.Find(c => c.Id == id);

            if (clienteSelecionado == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            var pedido = new Pedido(idPedido++, clienteSelecionado);

            string continuar;
            do
            {
                Console.WriteLine("\nProdutos disponíveis:");
                foreach (var produto in produtoRepo.ObterTodos())
                {
                    Console.WriteLine(produto);
                }

                Console.Write("Digite o ID do produto: ");
                int idProduto = int.Parse(Console.ReadLine());
                var produtoSelecionado = produtoRepo.BuscarPorId(idProduto);

                if (produtoSelecionado == null)
                {
                    Console.WriteLine("Produto não encontrado.");
                    continue;
                }

                Console.Write("Quantidade: ");
                int qtd = int.Parse(Console.ReadLine());

                var item = new PedidoItem(produtoSelecionado, qtd);
                pedido.AdicionarItem(item);

                Console.Write("Deseja adicionar outro item? (s/n): ");
                continuar = Console.ReadLine().ToLower();

            } while (continuar == "s");

            pedidoRepo.AdicionarPedido(pedido);
            Console.WriteLine("Pedido realizado com sucesso!");
        }

        static void VisualizarPedidos()
        {
            Console.WriteLine("\n=== Lista de Pedidos ===");

            var pedidos = pedidoRepo.ObterTodos();
            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido cadastrado.");
                return;
            }

            foreach (var pedido in pedidos)
            {
                Console.WriteLine(pedido);
            }
        }
    }
}
