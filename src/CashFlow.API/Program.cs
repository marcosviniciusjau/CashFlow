using CashFlow.API.Filters;
using CashFlow.API.Middleware;
using CashFlow.Infra;
using CashFlow.App;
using CashFlow.Infra.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApp();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await MigationDatabase();
app.Run();

async Task MigationDatabase()
{
    using var scope = app.Services.CreateScope();
    await DataBaseMigration.MigrateDatabase(scope.ServiceProvider);
}