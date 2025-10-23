using System;

namespace AgendaDeContatos
{
    public class Contato
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Contato(string nome, string telefone, string email)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}\nTelefone: {Telefone}\nEmail: {Email}";
        }
    }
}