# Projeto Banco Digital — FIAP 3ESPW 2026

## 1. Identificação

| Nome | RM |
|------|----|
| Camila Pedroza da Cunha | RM 558768 |
| Pedro Almeida Camacho | RM 556831 |

---

## 2. Produto Bancário Escolhido

**Empréstimo Pessoal**

Escolhemos o Empréstimo Pessoal por permitir a implementação de regras de negócio claras e objetivas, como taxa de juros anual, valor máximo liberado e prazo máximo em meses. Além disso, viabiliza a regra extra de dupla: o **cálculo de score de crédito**, que determina automaticamente a aprovação ou recusa da contratação com base no perfil do cliente.

---

## 3. Decisão de Modelagem de Filas

Por se tratar de uma solução acadêmica com foco nas regras de negócio e na camada de persistência, optamos por **simular o processamento de forma síncrona**, sem uso de filas ou mensageria. O endpoint `POST /api/contratacoes` realiza a análise de crédito e atualiza o status da contratação (`APROVADO` ou `RECUSADO`) diretamente na requisição, retornando `202 Accepted` com o resultado imediato.

---

## 4. Diagrama de Classes

![Diagrama de Classes](./Docs/diagrama-classes.png)

---

## 5. Como Rodar Localmente

### Pré-requisitos
- .NET 8.0 SDK instalado
- Acesso à rede da FIAP (VPN se necessário)
- Credenciais Oracle FIAP (RM + senha)

### Configurar a connection string

No arquivo `appsettings.json`, substitua com suas credenciais:

```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=RM558768;Password=SUASENHA;Data Source=oracle.fiap.com.br:1521/ORCL"
  }
}
```

### Aplicar migrations e criar as tabelas

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Rodar a API

```bash
dotnet run
```

Acesse o Swagger em: `http://localhost:5000/swagger`

---

## 6. Endpoints Disponíveis

### POST `/api/agencias` — Cadastrar Agência
**Request:**
```json
{
  "nome": "Agência Centro",
  "numero": "0001",
  "endereco": "Rua das Flores, 100 - São Paulo/SP"
}
```
**Response:** `201 Created`
```json
{
  "id": 1,
  "nome": "Agência Centro",
  "numero": "0001",
  "endereco": "Rua das Flores, 100 - São Paulo/SP"
}
```

---

### GET `/api/agencias/{id}` — Buscar Agência
**Response:** `200 OK`
```json
{
  "id": 1,
  "nome": "Agência Centro",
  "numero": "0001",
  "endereco": "Rua das Flores, 100 - São Paulo/SP"
}
```

---

### POST `/api/clientes/pf` — Cadastrar Pessoa Física
**Request:**
```json
{
  "nome": "João da Silva",
  "email": "joao@email.com",
  "cpf": "123.456.789-00",
  "dataNascimento": "1990-05-15",
  "agenciaId": 1
}
```
**Response:** `201 Created`

**Erro — CPF duplicado:** `409 Conflict`
```json
{ "erro": "CPF já cadastrado." }
```

**Erro — Agência inexistente:** `400 Bad Request`
```json
{ "erro": "Agência não encontrada." }
```

---

### POST `/api/clientes/pj` — Cadastrar Pessoa Jurídica
**Request:**
```json
{
  "nome": "Empresa Ltda",
  "email": "contato@empresa.com",
  "cnpj": "12.345.678/0001-99",
  "razaoSocial": "Empresa de Tecnologia Ltda",
  "agenciaId": 1
}
```
**Response:** `201 Created`

**Erro — CNPJ duplicado:** `409 Conflict`
```json
{ "erro": "CNPJ já cadastrado." }
```

---

### GET `/api/clientes/{id}` — Buscar Cliente por ID
**Response:** `200 OK`
```json
{
  "id": 1,
  "nome": "João da Silva",
  "email": "joao@email.com",
  "agenciaId": 1,
  "tipoCliente": "PF",
  "cpf": "123.456.789-00",
  "dataNascimento": "1990-05-15T00:00:00"
}
```

---

### POST `/api/contratacoes` — Solicitar Contratação
**Request:**
```json
{
  "clienteId": 1,
  "produtoId": 1,
  "observacao": "Solicito empréstimo para capital de giro"
}
```
**Response:** `202 Accepted` — Aprovado
```json
{
  "id": 1,
  "clienteId": 1,
  "produtoId": 1,
  "dataSolicitacao": "2026-05-06T10:00:00",
  "status": "APROVADO",
  "observacao": "Aprovado. Score de crédito: 500."
}
```
**Response:** `202 Accepted` — Recusado
```json
{
  "id": 2,
  "clienteId": 3,
  "produtoId": 1,
  "dataSolicitacao": "2026-05-06T10:05:00",
  "status": "RECUSADO",
  "observacao": "Recusado. Score insuficiente: 300. Mínimo: 500."
}
```
**Erro — Cliente inexistente:** `404 Not Found`
```json
{ "erro": "Cliente não encontrado." }
```

---

### GET `/api/contratacoes/{id}` — Consultar Status da Contratação
**Response:** `200 OK`
```json
{
  "id": 1,
  "clienteId": 1,
  "produtoId": 1,
  "dataSolicitacao": "2026-05-06T10:00:00",
  "status": "APROVADO",
  "observacao": "Aprovado. Score de crédito: 500."
}
```

---

## 7. Regra de Negócio Extra — Score de Crédito (Dupla)

O sistema calcula um score de crédito automaticamente no momento da contratação:

| Critério | Pontos |
|----------|--------|
| Score base | 300 |
| PF com idade ≥ 25 anos | +100 |
| PF com idade ≥ 40 anos | +100 adicional |
| PJ (qualquer) | +250 |
| Por contratação aprovada anterior | +50 |
| **Score mínimo para aprovação** | **500** |

---

## 8. Como Executar os Testes

As evidências de todos os testes realizados via Swagger estão documentadas no arquivo:

📄 `docs/Evidências de Testes CP02 - C#.pdf`

Os fluxos cobertos são:
- Cadastro de agência
- Cadastro de cliente PF e PJ
- CPF e CNPJ duplicado (retorno 409)
- Agência inexistente (retorno 400)
- Contratação aprovada (score suficiente)
- Contratação recusada (score insuficiente)
- Contratação para cliente inexistente (retorno 404)
- Consulta de status da contratação

---

## 9. Evidências

As evidências completas com prints do Swagger estão em:

📄 [`docs/Evidências de Testes CP02 - C#.pdf`](docs/Evidências%20de%20Testes%20CP02%20-%20C%23.pdf)
