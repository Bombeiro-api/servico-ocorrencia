using Microsoft.EntityFrameworkCore;
using Ocorrencias;
using Ocorrencias.Infra;
using Ocorrencias.Servicos;
using Scalar.AspNetCore;
using Ocorrencias.Servicos;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

GeradorDeServicos.ServiceProvider = builder.Services.BuildServiceProvider();

builder.Services.AddScoped<ServOcorrencia>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();