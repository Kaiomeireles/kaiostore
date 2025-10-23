# Sistema de Loja - Checkpoint 2 (C#)

## Integrantes:
- Kaio Vinicius Meireles Alves - RM553282

## Descrição:
Este projeto simula um sistema de loja com funcionalidades de cadastro de produtos, clientes, categorias e pedidos. Ele segue os princípios da Programação Orientada a Objetos (POO) em C# e usa uma abordagem modular para a separação de responsabilidades.

## Funcionalidades:
- Cadastro e listagem de produtos
- Cadastro de clientes
- Registro de pedidos e itens de pedidos
- Repositórios para gerenciamento de dados
- Conexão simulada com banco de dados
- Menu interativo em console

## Estrutura do Projeto:
- `Produto.cs`: Classe que representa um produto.
- `Categoria.cs`: Classe que representa a categoria de um produto.
- `Cliente.cs`: Classe que representa um cliente.
- `Pedido.cs`: Classe que representa um pedido.
- `PedidoItem.cs`: Classe que representa um item dentro de um pedido.
- `ProdutoRepository.cs`: Gerencia a lista de produtos disponíveis.
- `PedidoRepository.cs`: Gerencia os pedidos realizados.
- `DatabaseConnection.cs`: Simula a conexão com o banco de dados.
- `Program.cs`: Classe principal com o menu interativo.
- `SistemaLoja.csproj`: Arquivo de projeto do C#.

## Como Executar:
1. Abrir o projeto no Visual Studio ou outro IDE C#.
2. Restaurar dependências (se necessário).
3. Executar o arquivo `Program.cs`.