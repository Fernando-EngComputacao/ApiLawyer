using API_Lawyer.Assets.Client;
using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Services;
using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Extensions.Exceptions;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddControllers()
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CrawlerValidator>());

builder.Services.AddHttpClient();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddTransient<CrawlerClient>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// To use Service
builder.Services.AddScoped<OrigemService>();
builder.Services.AddScoped<MovimentacaoService>();
builder.Services.AddScoped<ProcessoService>();
builder.Services.AddScoped<TransicaoService>();

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
