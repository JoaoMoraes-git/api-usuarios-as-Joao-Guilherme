# API de Gerenciamento de Usuários


## Descrição
Esta API permite gerenciar usuários, podendo listar, adicionar, atualizar e remover no banco de dados.
O projeto foi feito com ASP.NET Core 9 e usa camadas separadas como, validação dos dados e clean arquiteture


## Tecnologias Utilizadas
- .NET 9.0
- ASP.NET Core Minimal APIs
- Entity Framework Core
- SQLite
- AutoMapper
- FluentValidation


## Padrões de Projeto Implementados
- Repository Pattern
- Service Pattern
- DTO Pattern
- Dependency Injection


## Como Executar o projeto


### Pré-requisitos
Antes de rodar o programa, é preciso ter instalar e configurar o seguinte:


#### 1. .Net SDK 9.0
- .NET SDK 9.0 ou superior instalado

Link de download: 
https://dotnet.microsoft.com/


#### 2. Entity Framework Core
- Instalar o EF Core via terminal

Comando de instalação:
dotnet tool install --global dotnet-ef


#### 3. Restaurar pacotes NuGet
- Restaure os pacotes de NuGet via terminal

Comando:
dotnet restore


### Passos
1. Clone o repositório

git clone https://github.com/usuario/repositorio.git

2. Execute as migrations 

dotnet ef migrations add InitialCreate
dotnet ef database update


3. Execute a aplicação

dotnet run


### Exemplos de requisições

- Post:

{
    "Nome": "Rodrigo",
    "Email": "rodrigo05@gmail.com",
    "Senha": "11102005",
    "DataNascimento": "2005-10-11",
    "Telefone": "(90) 95342-4245"
}

- Put

{
    "Nome": "João",
    "Email": "joaogit@gmail.com",
    "DataNascimento": "2007-04-20",
    "Telefone": "",
    "Ativo": true
}


## Estrutura do Projeto

```
api-usuarios/
├── Application/                           # Camada de aplicação: regras de negócio, DTOs, serviços e validações
|   ├── DTOs/                              # Modelos usados para entrada e saída de dados na API
|   |   ├── UsuarioCreateDto.cs            # DTO utilizado para criação de usuários (não expõe a senha no retorno)
|   |   ├── UsuarioReadDto.cs              # DTO retornado nas consultas; expõe apenas dados seguros
|   |   └── UsuarioUpdateDto.cs            # DTO utilizado para atualização de usuários
|   |
|   ├── Interfaces/                        # Interfaces que definem contratos para serviços e repositórios
|   |   ├── IUsuarioRepository.cs          # Contrato do repositório de usuários (persistência)
|   |   └── IUsuarioService.cs             # Contrato dos serviços de usuário (regras de negócio)
|   |
|   ├── Mapping/                           # Configurações do AutoMapper
|   |   └── UsuarioProfile.cs              # Mapeamento entre Entidade <-> DTOs
|   |
|   ├── Services/                          # Implementações das regras de negócio (camada de serviço)
|   |   └── UsuarioService.cs              # Serviço responsável pela lógica de criação, consulta e atualização de usuários
|   |
|   └── Validators/                        # Validações usando FluentValidation
|       ├── UsuarioCreateDtoValidator.cs   # Regras de validação para criação (nome, email, senha, idade etc.)
|       └── UsuarioUpdateDtoValidator.cs   # Regras de validação para atualização (exceto senha)
|
├── Domain/                                # Camada de domínio: contém entidades e regras fundamentais
|   └── Entities/                          # Entidades centrais do sistema
|       └── Usuario.cs                     # Entidade de usuário (modelo principal da aplicação)
|
├── Infrastructure/                        # Infraestrutura: persistência e comunicação com o banco de dados
|   ├── Persistence/                       # Configurações relacionadas ao Entity Framework Core
|   |   └── AppDbContext.cs                # Contexto do banco de dados (DbSet<Usuario>)
|   | 
|   ├── Repositories/                      # Implementações dos repositórios
|       └── UsuarioRepository.cs           # Implementação concreta do repositório de usuários
|
├── Migrations/                            # Arquivos gerados pelo EF Core que criam e atualizam o banco de dados
|   └── (gerado automáticamente)           # Criados automaticamente após rodar "dotnet ef migrations add ..."
|
├── Program.cs                             # Ponto de entrada da API; registra serviços, validações e endpoints
|
├── README.md                              # Documentação do projeto com instruções de uso
|
└── usuario.db                             # Banco de dados SQLite criado automaticamente ao rodar a aplicação
```


## Autor
João Guilherme Silva de Moraes
Código acadêmico: 2024009659
Curso: Analise e Desenvolvimento de Sistemas







