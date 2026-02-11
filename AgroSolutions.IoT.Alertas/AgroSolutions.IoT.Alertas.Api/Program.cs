using AgroSolutions.IoT.Alertas.Api.Configuration;
using AgroSolutions.IoT.Alertas.Application.Configuration;
using AgroSolutions.IoT.Alertas.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Mensageria
builder.Services.AddMessaging(builder.Configuration);

// Adicionar servicos da camada de aplicação
builder.Services.AddApplicationServices();

// Adicionar configuracoes de servicos da infraestrutura
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();