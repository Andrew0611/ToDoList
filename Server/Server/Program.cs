using Microsoft.EntityFrameworkCore;
using Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
ConfigurationManager manager = builder.Configuration;
builder.Services.AddDbContext<MemorandumContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Memorandum"));
});

// CORS¬Fµ¦
string[] corsURL = manager.GetSection("CORS").Get<string[]>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("UserPage", al =>
    {
        al.AllowAnyHeader();
        al.AllowAnyMethod();
        al.AllowAnyOrigin();
    });
});

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
