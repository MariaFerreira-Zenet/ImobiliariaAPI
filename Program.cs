using ImobiliariaApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar o serviço do DbContext
builder.Services.AddDbContext<ImobiliariaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar o CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Adicionar os controladores como um serviço
builder.Services.AddControllers(); // Esta linha estava faltando

// Adicionar serviços de API e Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization(); // Dependendo da configuração, você pode precisar desta linha

app.MapControllers(); // Esta linha está correta e mapeia as rotas dos controladores

app.Run();
