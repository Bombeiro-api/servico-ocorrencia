# Serviço de Ocorrências

Microsserviço responsável pelo registro e gestão de ocorrências do corpo de bombeiros. Ao criar uma ocorrência, aciona automaticamente o serviço de mapa para calcular a rota e despachar a viatura mais próxima. Faz parte do [Sistema CAD Bombeiros](https://github.com/Bombeiro-api).

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

### Encerrar ocorrência

```http
PATCH /api/Ocorrencia/{id}/encerrar
```

Marca a ocorrência como `Encerrada` e libera automaticamente a viatura de volta ao status `DisponivelNaBase` no servico-veiculos.

### Atualizar ocorrência

```http
PUT /api/Ocorrencia/{id}
```

### Remover ocorrência

```http
DELETE /api/Ocorrencia/{id}
```

## Integração com Outros Microsserviços

Ao criar uma ocorrência, este serviço chama o **servico-mapa** que por sua vez consulta o **servico-veiculos** e despacha a viatura mais próxima. Os dados de despacho são preenchidos automaticamente na ocorrência.

```
POST /api/ocorrencia
  └─► servico-mapa POST /api/mapa/rota-mais-proxima
        └─► retorna corporação, viatura, distância e tempo estimado
```

Para o fluxo completo de despacho, consulte o [README da organização](https://github.com/Bombeiro-api).

## Arquitetura

* **Controllers** — exposição dos endpoints da API
* **Servicos** — regras de negócio e integração com servico-mapa
* **DTO** — modelos de dados e contratos com outros serviços
* **DataContext** — comunicação com o banco de dados
* **Migrations** — controle de versão da estrutura do banco
