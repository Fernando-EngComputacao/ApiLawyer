## 💻 Sobre o projeto

LawyerAPI é um projeto no qual dados do site do Segundo Grau do Tribunal de Justiça da BA, dado um processo X, é raspado e obtidos em formato JSON. Esses dados são gravados na base de dados, MySQL. 

Para acesso direto e gestão destes dados uma API REST é utilizada com a atuação os verbos HTTP (CRUD).

Frente à isso, este projeto objetiva com o uso de Crawler e .NET, fazer o back-end desta aplicação.

---

## ⚙️ Funcionalidades

- [x] CRUD do Processo 
- [x] CRUD da Movimentação;
- [x] CRUD da Origem;
- [x] CRUD da Transição;
- [x] Crawler do Site Tribunal de Justiça do Estado da Bahia
- [x] Integração dos CRUDs com a base de dados
- [x] Integração do Crawler com a base de dados

---

## :1st_place_medal: Envolvimento do projeto
- [x] Implementação de CRUD;
- [x] Documentação da API em Swagger;
- [x] Usabilidade do DbContext para escrita;
- [x] Implementação de relacionamento: 1-n e n-n;
- [x] Atuação de tipos de consultas com LINQ;
- [x] Utilização de Crawler
- [x] Validação Técnica de Recurso
- [x] Validação de Negócio


--- 

## :hammer: NuGet Dependências 
- [x] Microsoft.EntityFrameworkCore.Tools 6.0.14
- [x] Microsoft.EntityFrameworkCore 6.0.14
- [x] Pomelo.EntityFrameworkCore.MySql 6.0.2
- [x] Microsoft.EntityFrameworkCore.Proxies 6.0.14
- [x] Microsoft.AspNetCore.Mvc.NewtonsoftJson 6.0.14
- [x] Microsoft.Extensions.Identity.Stores 6.0.14
- [x] Swashbuckle.AspNetCore.SwaggerGen 6.2.3
- [x] Swashbuckle.AspNetCore.SwaggerUI 6.5.2
- [x] System.IdentityModel.Tokens.ValidatingIssuerNameRegistry 4.5.1
- [x] AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.0
- [x] Swashbuckle.AspNetCore.Annotations 6.0.1
- [x] System.Text.RegularExpressions 4.3.1
- [x] FluentValidation.AspNetCore 10.3.0
- [x] Microsoft.Identity.Client 4.50.0
- [x] Swashbuckle.AspNetCore 6.2.3
- [x] AutoMapper 12.0.0
- [x] FuzzySharp 2.0.2

  
---

## :bar_chart: Diagrama de Classe de Dados
####  - Diagrama de classe para banco de dados do projeto API-Lawyer, acesse-o [aqui](https://github.com/Fernando-EngComputacao/ApiLawyer/blob/dev/API-Lawyer/Assets/Diagrams/Lawyer-dev.pdf).


---

## :bookmark: Recursos Adicionais
### - Validações Adicionais de Negócio
 - *Validação Numero Processo:* validação para impedir duplicidade de mesmo processo na base de dados. Isto é, impede o registro duplo de um mesmo processo.
   - *Crawler:* ao rodar o GET ou POST, do Crawler, de um processo já cadastrado na base de dados, o sistema avisa a duplicidade e não cadastra o processo;
   - *Processo:* ao rodar o POST no campo do Processo, ocorre o mesmo cenário do caso descrito acima (Crawler).
   
    
 - Validação Origem: validação para impedir duplicidade de mesma origem na base de dados. Isto é, impede o registro duplo de uma mesma origem.
   - Crawler:  ao rodar o GET ou POST, do Crawler, de uma origem já cadastrada na base de dados, o sistema ignora a tentativa de cadastro e não reflete aviso;
   - Origem: ao rodar o POST de uma origem já cadastrada na base de dados, o sistema acusa a tentativa de cadastro e reflete aviso;
 



---

## :star: Orientações Adicionais
#### - Orientações Adicionais
###### - Para criar as Migrations, use o comando abaixo:

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
   - [LinkedIn](https://linkedin.com/in/furtadof/)
   - [GitHub](https://github.com/Fernando-EngComputacao)