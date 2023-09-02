using Application.Middlewares;
using FradminDomain.SGBDs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var envVars = Environment.GetEnvironmentVariables()!;

builder.Services.AddSingleton<SgbdBase>(new PostgresConnection(
    envVars["DB_HOST"]?.ToString() ?? "localhost",
    envVars["DB_PORT"]?.ToString() ?? "5432",
    envVars["DB_USER"]?.ToString() ?? "postgres",
    envVars["DB_PASSWORD"]?.ToString() ?? "postgres",
    envVars["DB_NAME"]?.ToString() ?? "postgres"));

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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
