# 🛍️ Ecommerce_API

API em desenvolvimento, que tem como objetivo se tornar um E-commerce, sendo possível cadastrar categorias de produtos, subcategorias e os produtos em si, além de um centro de distribuição, facilitando o controle e a gestão de uma loja E-commerce do zero, 
construída com ASP.NET Core e MySQL, com suporte completo via Docker.

---

## 🛠️ Tecnologias Utilizadas

* .NET 8.0
* Entity Framework Core 8.0.8
* MySQL 8.0
* Dapper 2.0.123
* AutoMapper 12.0.1
* Serilog 8.0.0
* Swagger (Swashbuckle) 8.1.1
  
---

## 📝 Pré-requisitos

Antes de rodar a aplicação, você precisa ter instalado:

* [Docker Desktop](https://www.docker.com/products/docker-desktop)
* (Opcional) Git, para clonar o repositório
* (Opcional) .NET SDK 8.0+, caso queira rodar localmente sem Docker

---

## 🚀 Como executar o projeto

### ✅ 1. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/ecommerceapi.git
cd ecommerceapi
```

### ✅ 2. Criar a imagem da aplicação

```bash
docker build -t ecommerceapi .
```

### ✅ 3. Subir os containers com Docker Compose

```bash
docker-compose up -d
```

---

## 🔄 Alternar string de conexão (appsettings.json) conforme ambiente

* **Produção ou execução em container Docker:**

```json
"LojaConnection": "server=db;database=ecommerceDb;user=root;password=root"
```

* **Desenvolvimento local (fora do Docker):**

```json
"LojaConnection": "server=localhost;database=ecommerceDb;user=root;password=root"
```
