using Microsoft.EntityFrameworkCore;
using ProjetoBanco.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("OracleConnection"),
        o => o.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19)
    ));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();