# Serviço de Ocorrências

## Sobre o Projeto

O Serviço de Ocorrências é um dos microsserviços do sistema de gerenciamento de ocorrências para o Corpo de Bombeiros. Sua principal responsabilidade é registrar, armazenar e disponibilizar informações sobre ocorrências atendidas pela corporação.

Este serviço foi desenvolvido utilizando arquitetura de microsserviços, permitindo integração com os demais serviços do sistema através de APIs REST.

## Tecnologias Utilizadas

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* Scalar
* REST API

## Funcionalidades

* Cadastro de ocorrências
* Consulta de ocorrências
* Atualização de ocorrências
* Exclusão de ocorrências
* Armazenamento de coordenadas geográficas (latitude e longitude)
* Integração com outros microsserviços através de endpoints REST

## Estrutura da Entidade

### Ocorrência

| Campo        | Tipo     |
| ------------ | -------- |
| Id           | Inteiro  |
| Tipo         | Texto    |
| Descricao    | Texto    |
| Latitude     | Double   |
| Longitude    | Double   |
| Status       | Texto    |
| DataAbertura | DateTime |

## Banco de Dados

O microsserviço utiliza SQLite como banco de dados local e Entity Framework Core para mapeamento e persistência dos dados.

Foi utilizada a abordagem Code First, com geração automática da estrutura do banco através de Migrations.

## Endpoints Disponíveis

### Listar ocorrências

```http
GET /api/Ocorrencia
```

### Buscar ocorrência por ID

```http
GET /api/Ocorrencia/{id}
```

### Cadastrar ocorrência

```http
POST /api/Ocorrencia
```

### Atualizar ocorrência

```http
PUT /api/Ocorrencia/{id}
```

### Remover ocorrência

```http
DELETE /api/Ocorrencia/{id}
```

## Integração com Outros Microsserviços

Este serviço foi projetado para fornecer informações ao microsserviço de Mapas. As coordenadas de latitude e longitude armazenadas em cada ocorrência permitem que o serviço de Mapas realize cálculos de rota e localização para atendimento das ocorrências.

Exemplo de fluxo:

1. Uma ocorrência é registrada.
2. O serviço de Ocorrências armazena as coordenadas geográficas.
3. O serviço de Mapas consulta a ocorrência.
4. A rota de atendimento é calculada utilizando os dados recebidos.

## Arquitetura

O projeto está organizado nas seguintes camadas:

* **Controllers:** exposição dos endpoints da API.
* **Servicos:** regras de negócio e operações da aplicação.
* **DTO:** entidades e modelos de dados.
* **DataContext:** comunicação com o banco de dados.
* **Migrations:** controle de versão da estrutura do banco.
