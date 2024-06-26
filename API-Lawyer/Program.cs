using API_Lawyer.Assets.Client;
using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Services;
using API_Lawyer.Assets.Services.Validators;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionStringLawyer = builder.Configuration.GetConnectionString(("LawyerConnection"));
builder.Services.AddDbContext<LawyerContext>(
    opts =>
    {
        opts.UseMySql(connectionStringLawyer, ServerVersion.AutoDetect(connectionStringLawyer));
    }
);

// To use AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CrawlerValidator>());
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<TransicaoValidator>());
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<OrigemValidator>());
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<MovimentacaoValidator>());
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<ProcessoValidator>());

builder.Services.AddHttpClient();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddTransient<CrawlerClient>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "API-Lawyer", Version = "v1" });
});

// To use Service
builder.Services.AddScoped<OrigemService>();
builder.Services.AddScoped<MovimentacaoService>();
builder.Services.AddScoped<ProcessoService>();
builder.Services.AddScoped<TransicaoService>();
builder.Services.AddScoped<CrawlerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
