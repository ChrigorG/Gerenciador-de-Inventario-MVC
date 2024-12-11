# Descrição do Projeto

Este projeto implementa regras de negócios essenciais, como o controle de permissões de acesso para os funcionários. Cada colaborador é atribuído a um grupo de permissões, o que define as áreas do sistema que ele pode acessar. Por exemplo, um funcionário pode ter permissão para cadastrar produtos ou realizar movimentações de estoque (entradas e saídas), dependendo das suas responsabilidades dentro da organização.

## Desenvolvimento

O projeto foi desenvolvido como uma demonstração prática utilizando:

- **Padrão MVC** e **arquitetura Clean Architecture**.
- **.NET 8** e **SQL Server** como base tecnológica.
- **Interfaces** e **injeção de dependência** para desacoplar e modularizar o código, facilitando a manutenção e testes.
- **Bootstrap 5** para a criação da interface.
- **Autenticação com Claims** para gerenciamento de usuários.
- Controle de acesso baseado em grupos de permissão, permitindo diferentes níveis de acesso, como:
  - Cadastro de produtos.
  - Movimentações de estoque (entradas e saídas).

Este projeto foi criado com foco no aprendizado e na prática de desenvolvimento de software com boas práticas e organização de código.


## Previw:

### Login
![image](https://github.com/user-attachments/assets/fea34688-27f8-4be1-8a78-05ae389dde96)

### Tela Inicial
![image](https://github.com/user-attachments/assets/7c035c63-7d5c-4db4-a236-67933dc10a62)

### Tela do cadastro de Funcionários
![image](https://github.com/user-attachments/assets/b56b7871-4150-42cd-9564-45362aabd5a2)


## Modo de uso

Ter instalado o SQLServer >= 15.0.4153

Realizar as migrations disponivel dentro do projeto "Gerenciador-de-Inventario-MVC" --> "Migrations.txt"

Executar o projeto
