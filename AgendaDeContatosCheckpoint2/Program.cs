using System;

namespace AgendaDeContatos
{
    class Program
    {
        static void Main(string[] args)
        {
            Agenda agenda = new Agenda();
            int opcao;

            do
            {
                Console.WriteLine("\nAGENDA DE CONTATOS");
                Console.WriteLine("1. Adicionar Contato");
                Console.WriteLine("2. Remover Contato");
                Console.WriteLine("3. Buscar Contato");
                Console.WriteLine("4. Listar Contatos");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");
                
                if (!int.TryParse(Console.ReadLine(), out opcao)) continue;

                switch (opcao)
                {
                    case 1:
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();
                        Console.Write("Telefone: ");
                        string telefone = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        agenda.AdicionarContato(new Contato(nome, telefone, email));
                        break;

                    case 2:
                        Console.Write("Nome do contato a remover: ");
                        string nomeRemover = Console.ReadLine();
                        agenda.RemoverContato(nomeRemover);
                        break;

                    case 3:
                        Console.Write("Nome do contato a buscar: ");
                        string nomeBuscar = Console.ReadLine();
                        Contato encontrado = agenda.BuscarContato(nomeBuscar);
                        Console.WriteLine(encontrado != null ? encontrado.ToString() : "Contato não encontrado.");
                        break;

                    case 4:
                        agenda.ListarContatos();
                        break;

                    case 5:
                        Console.WriteLine("Saindo da agenda...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (opcao != 5);
        }
    }
}