## 💻 Sobre o projeto

LawyerAPI é um projeto no qual dados do site do Segundo Grau do Tribunal de Justiça da BA, dado um processo X, é raspado e obtidos em formato JSON. Esses dados são gravados na base de dados, MySQL. 

Para acesso direto e gestão destes dados uma API REST é utilizada com a atuação os verbos HTTP (CRUD).

Frente à isso, este projeto objetiva com o uso de Crawler e .NET, fazer o back-end desta aplicação.

---

## ⚙️ Funcionalidades

- [x] CRUD do Processo 
- [x] CRUD da Movimentação;
- [x] CRUD da Origem;
- [x] Crawler do Site Tribunal de Justiça do Estado da Bahia
- [x] Integração dos CRUDs com a base de dados
- [ ] Integração do Crawler com a base de dados

---

## :1st_place_medal: Envolvimento do projeto
- [x] Implementação de CRUD;
- [x] Documentação da API em Swagger;
- [x] Usabilidade do DbContext para escrita;
- [x] Implementação de relacionamento: 1-n e n-n;
- [x] Atuação de tipos de consultas com LINQ;
- [x] Utilização de Crawler

--- 

## :hammer: NuGet Dependências 
- [x] Microsoft.EntityFrameworkCore.Tools 6.0.14
- [x] Microsoft.EntityFrameworkCore 6.0.14
- [x] Pomelo.EntityFrameworkCore.MySql 6.0.2
- [x] Microsoft.EntityFrameworkCore.Proxies 6.0.14
- [x] Microsoft.AspNetCore.Mvc.NewtonsoftJson 6.0.14
- [x] Microsoft.Extensions.Identity.Stores 6.0.14
- [x] Microsoft.Identity.Client 4.50.0
- [x] System.IdentityModel.Tokens.ValidatingIssuerNameRegistry 4.5.1
- [x] AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.0
- [x] AutoMapper 12.0.0
- [x] Swashbuckle.AspNetCore 6.2.3
- [x] Swashbuckle.AspNetCore.Annotations 6.0.1
- [x] System.Text.RegularExpressions 4.3.1
  
---

## :bar_chart: Diagrama de Classe de Dados
####  - Diagrama de classe para banco de dados do projeto API-Lawyer, acesse-o [aqui](https://github.com/Fernando-EngComputacao/ApiLawyer/blob/dev/API-Lawyer/Assets/Diagram/Lawyer-dev.pdf).


---

## :star: Orientações Adicionais
#### Orientações Adicionais
###### - Para criar as Migratoions, use o comando abaixo:

    Add-Migration <NomeDaMigration>

Exemplo: 

    Add-Migration CriacaoTabelaUsuario

###### - Após criada a Migratoion, use o comando abaixo para atualizar a base de dados:

    Update-Database
        
###### - Outros comandos úteis para Migrations
 - List-Migrations: lista todas as migrações existentes.
 - Script-Migration: gera um script SQL para uma migração específica.
 - Remove-Migration: remove uma migração específica.
 - Update-Database -TargetMigration: aplica migrações até um ponto específico.


---

####  :sunglasses: Autor: Fernando Furtado (2024)
   - [LinkedIn](linkedin.com/in/furtadof/)
   - [GitHub](https://github.com/Fernando-EngComputacao)