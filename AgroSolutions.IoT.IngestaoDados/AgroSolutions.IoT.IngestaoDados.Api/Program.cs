using System.Text.Json.Serialization;
using AgroSolutions.IoT.IngestaoDados.Api.Configuration;
using AgroSolutions.IoT.IngestaoDados.Api.Middlewares;
using AgroSolutions.IoT.IngestaoDados.Application.Configuration;
using AgroSolutions.IoT.IngestaoDados.Infrastructure.Configuration;
using AgroSolutions.IoT.IngestaoDados.Infrastructure.Data;
using AgroSolutions.IoT.IngestaoDados.Infrastructure.Seed;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCustomHttpLogging();

// Adicionar configuracoes do banco de dados e servicos da infraestrutura
builder.Services.AddInfrastructureServices(builder.Configuration);

// Mensageria
builder.Services.AddMessaging(builder.Configuration);

// Adicionar servicos da camada de aplicação
builder.Services.AddApplicationServices();

// Controllers e Swagger
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithOptions();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Middleware de log
app.UseHttpLogging();

// Chamar o Seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();

    await DbInitializer.SeedAsync(context);
}

// Middleware do Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Middlewares HTTP
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();