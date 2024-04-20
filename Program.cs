using ImobiliariaApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar o servi�o do DbContext
builder.Services.AddDbContext<ImobiliariaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar o CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Adicionar os controladores como um servi�o
builder.Services.AddControllers(); // Esta linha estava faltando

// Adicionar servi�os de API e Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o pipeline de solicita��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization(); // Dependendo da configura��o, voc� pode precisar desta linha

app.MapControllers(); // Esta linha est� correta e mapeia as rotas dos controladores

app.Run();
