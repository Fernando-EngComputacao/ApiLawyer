using API_Lawyer.Assets.Client;
using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Datas;
using API_Lawyer.Assets.Models.Usuario;
using API_Lawyer.Assets.Models.Usuario.secret;
using API_Lawyer.Assets.Security;
using API_Lawyer.Assets.Services;
using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionStringLawyer = builder.Configuration.GetConnectionString(("LawyerConnection"));
var connectionStringUser = builder.Configuration.GetConnectionString(("UserConnection"));

builder.Services.AddDbContext<LawyerDbContext>(
    opts =>
    {
        opts.UseMySql(connectionStringLawyer, ServerVersion.AutoDetect(connectionStringLawyer));
    }
);
builder.Services.AddDbContext<UsuarioDbContext>(
    opts =>
    {
        opts.UseMySql(connectionStringUser, ServerVersion.AutoDetect(connectionStringUser));
    }
);

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();


// To use AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// To add UserAuthorization (method created by myselg), UserAuthorization > ValidUser
builder.Services.AddSingleton<IAuthorizationHandler, AutorizacaoUsuario>();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CrawlerValidator>());
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<TransicaoValidator>());
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<OrigemValidator>());
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<MovimentacaoValidator>());
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<ProcessoValidator>());

//HTTPCLient
builder.Services.AddHttpClient();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Client
builder.Services.AddTransient<CrawlerClient>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "API-Lawyer", Version = "v1" });

    // Configuração do esquema de segurança Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // Adiciona o filtro personalizado para adicionar ícones de cadeado
    c.OperationFilter<SwaggerString>();
});

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AERTHJIHGFDER567YTFDY876GE44R5TT6YYY77U")),
        ValidateAudience = true,
        ValidateIssuer = true,
        ClockSkew = TimeSpan.Zero
    };
}
);

// To use Service
builder.Services.AddScoped<OrigemService>();
builder.Services.AddScoped<MovimentacaoService>();
builder.Services.AddScoped<ProcessoService>();
builder.Services.AddScoped<TransicaoService>();
builder.Services.AddScoped<CrawlerService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AutorizacaoService>();
builder.Services.AddScoped<TokenService>();

// To apply authorization
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("standard", policy =>
    {
        policy.AddRequirements(new UsuarioValido());
    });
    opt.AddPolicy("local", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddRequirements(new UsuarioValido());
        policy.RequireClaim("NívelDeAcesso", "Admin");
    });
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
