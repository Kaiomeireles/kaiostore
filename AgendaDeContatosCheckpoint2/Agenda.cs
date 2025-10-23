using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaDeContatos
{
    public class Agenda
    {
        private List<Contato> contatos = new List<Contato>();

        public void AdicionarContato(Contato contato)
        {
            contatos.Add(contato);
        }

        public void RemoverContato(string nome)
        {
            Contato contato = BuscarContato(nome);
            if (contato != null)
            {
                contatos.Remove(contato);
                Console.WriteLine("Contato removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Contato não encontrado.");
            }
        }

        public Contato BuscarContato(string nome)
        {
            return contatos.FirstOrDefault(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public void ListarContatos()
        {
            if (contatos.Count == 0)
            {
                Console.WriteLine("Nenhum contato cadastrado.");
            }
            else
            {
                foreach (var contato in contatos)
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine(contato);
                }
            }
        }
    }
}