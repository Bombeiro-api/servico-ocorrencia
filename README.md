# Serviço de Ocorrências

## Sobre o Projeto

O Serviço de Ocorrências é um dos microsserviços do sistema de gerenciamento de ocorrências para o Corpo de Bombeiros. Sua principal responsabilidade é registrar, armazenar e disponibilizar informações sobre ocorrências atendidas pela corporação.

## Tecnologias Utilizadas

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* Scalar
* REST API

## Estrutura da Entidade

### Ocorrência

| Campo | Tipo | Descrição |
|---|---|---|
| Id | Inteiro | Identificador único |
| Tipo | Texto | Tipo da ocorrência |
| Descricao | Texto | Descrição do incidente |
| Latitude | Double | Coordenada geográfica |
| Longitude | Double | Coordenada geográfica |
| Status | Texto | Status atual (padrão: "Aberta") |
| DataAbertura | DateTime | Data e hora do registro |
| Distancia | Texto | Distância estimada da corporação até o local |
| TempoEstimaodo | Texto | Tempo estimado de deslocamento |
| CorporacaoId | Inteiro | ID da corporação despachada (vem do servico-veiculos) |
| NomeCorporacao | Texto | Nome da corporação despachada |
| ViaturaId | Inteiro | ID da viatura despachada (vem do servico-veiculos) |

## Endpoints

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

**Body:**
```json
{
  "tipo": "Incêndio",
  "descricao": "Incêndio em residência",
  "latitude": -28.7283,
  "longitude": -49.3015
}
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

Ao criar uma ocorrência, este serviço chama o **ServicoMapa** para obter a rota e a corporação mais próxima. O ServicoMapa por sua vez consulta o **servico-veiculos** para encontrar uma viatura disponível e despachá-la.

### Fluxo completo

```
POST /api/Ocorrencia
  └─► ServicoMapa POST /api/mapa/rota-mais-proxima
        ├─► servico-veiculos GET /api/corporacao  (busca corporações com viatura disponível)
        ├─► Google Maps Distance Matrix           (encontra a mais próxima)
        ├─► servico-veiculos PATCH /api/viatura/{id}/status  (despacha a viatura)
        └─► retorna corporação, viatura, distância e tempo estimado

A ocorrência é salva com os dados de despacho preenchidos automaticamente.
```

## Arquitetura

* **Controllers** — exposição dos endpoints da API
* **Servicos** — regras de negócio e integração com ServicoMapa
* **DTO** — modelos de dados e contratos com outros serviços
* **DataContext** — comunicação com o banco de dados
* **Migrations** — controle de versão da estrutura do banco
