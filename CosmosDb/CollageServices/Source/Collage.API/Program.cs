using Collage.BLL;
using Collage.Core.Interfaces;
using Collage.DAL;
using Collage.DAL.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CollegeCosmosDbContext>(o =>
            o.UseCosmos(builder.Configuration["CosmosDbConnectionStrings:accountEndPoint"],
                        builder.Configuration["CosmosDbConnectionStrings:accountKey"],
                        builder.Configuration["CosmosDbConnectionStrings:databaseName"])
            .EnableSensitiveDataLogging(true));

builder.Services.AddScoped<IProfessorsCosmosBll, ProfessorsCosmosBll>();
builder.Services.AddScoped<IProfessorsCosmosDal, ProfessorsCosmosDal>();


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
