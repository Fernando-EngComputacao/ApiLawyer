## 💻 Sobre o projeto

LawyerAPI é um projeto no qual dados do site do Segundo Grau do Tribunal de Justiça da BA, dado um processo, 0809979-67.2015.8.05.0080, é raspado e obtidos em formato JSON. Esses dados são gravados na base de dados, MySQL. 

Para acesso direto e gestão destes dados uma API REST é utilizada com a atuação os verbos HTTP (CRUD), com controle de usuário.

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
- [x] Controle de Usuário

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
- [x] Autenticação de Usuário
- [x] Camada de Proteção de acesso à API


--- 

## :hammer: NuGet Dependências 
- [x] Microsoft.EntityFrameworkCore.Tools 6.0.14
- [x] Microsoft.EntityFrameworkCore 6.0.14
- [x] Pomelo.EntityFrameworkCore.MySql 6.0.2
- [x] Microsoft.EntityFrameworkCore.Proxies 6.0.14
- [x] Microsoft.AspNetCore.Mvc.NewtonsoftJson 6.0.14
- [x] Microsoft.AspNetCore.Authentication.JwtBearer 8.0.3
- [x] Microsoft.AspNetCore.Identity.EntityFrameWorkCore 6.0.14
- [x] System.IdentityModel.Tokens.ValidatingIssuerNameRegistry 4.5.1
- [x] AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.0
- [x] Microsoft.Extensions.Identity.Stores 6.0.14
- [x] Swashbuckle.AspNetCore.Annotations 6.0.1
- [x] Swashbuckle.AspNetCore.SwaggerGen 6.2.3
- [x] Swashbuckle.AspNetCore.SwaggerUI 6.5.2
- [X] System.IdentityModel.Tokens.Jwt 7.4.1
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

## :lock: Autenticação de Usuário
#### - Migrations
##### - Especifique qual o contexto que irá rodar para a base de dados _(faça o mesmo para LawyerDbContext)_:

- Para criar a base de dados do Usuário para autenticação, use:

```
  Add-Migration CreateUserTable -Context UsuarioDbContext
```

- Para atualizar a base de dados do Usuário também é necessário utilizar o contexto, use:

```
  Update-Database -Context UsuarioDbContext
```

### - Cadastro de Usuário
#### - Exemplo para cadastro de usuário _(a senha deve ter letra maiúscula, minúscula, número e caracter especial)_:
```
{
  "Username": "root",
  "Email": "root@mail.com",
  "DataNascimento": "2024-02-23T16:13:43.323Z",
  "Password": "@Senha123!",
  "RePassword": "@Senha123!"
}
```

### - Logar Usuário (Autenticação) -> Retorna um Token Bearer
#### - Exemplo de login de usuário, do cadastro acima.
```
{
  "Username": "root",
  "Password": "@Senha123!"
}
```

### - Swagger
#### - Pegue o Token obtido ao logar usuário _(passo anterior)_ e coloque no "[Autorize 🔓]" presente na página do Swagger _(canto superior direito)_.

---

## :star: Orientações Adicionais
#### - Orientações Adicionais
###### - Para criar as Migrations, use o comando abaixo:

    Add-Migration <NomeDaMigration> -Context <Name>DbContext

- Exemplo: 

    Add-Migration CriacaoTabelaUsuario -Context LawyerDbContext

###### - Após criada a Migratoion, use o comando abaixo para atualizar a base de dados:

    Update-Database -Context LawyerDbContext
        
###### - Outros comandos úteis para Migrations
 - List-Migrations: lista todas as migrações existentes.
 - Script-Migration: gera um script SQL para uma migração específica.
 - Remove-Migration: remove uma migração específica.
 - Update-Database -TargetMigration: aplica migrações até um ponto específico.


---

####  :sunglasses: Autor: Fernando Furtado (2024)
   - [LinkedIn](https://linkedin.com/in/furtadof/)
   - [GitHub](https://github.com/Fernando-EngComputacao)