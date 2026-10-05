using Microsoft.EntityFrameworkCore;
using RapidCMS.Api.Controllers;
using RapidCMS.Application.Abstractions;
using RapidCMS.Application.Documents;
using RapidCMS.Infrastructure.Persistence;
using RapidCMS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("RapidCms")
    ?? throw new InvalidOperationException(
        "Connection string 'RapidCms' was not configured.");

builder.Services.AddDbContext<RapidCmsDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IDocumentRepository, EfDocumentRepository>();

builder.Services.AddScoped<CreateDocumentHandler>();
builder.Services.AddScoped<DeleteDocumentHandler>();
builder.Services.AddScoped<GetDocumentByIdHandler>();
builder.Services.AddScoped<GetDocumentsHandler>();

builder.Services.AddScoped<AddPageToDocumentHandler>();
builder.Services.AddScoped<RemovePageFromDocumentHandler>();

builder.Services.AddScoped<AddAssetToDocumentHandler>();
builder.Services.AddScoped<RemoveAssetFromDocumentHandler>();

builder.Services.AddScoped<AddVariableToDocumentHandler>();
builder.Services.AddScoped<RemoveVariableFromDocumentHandler>();

builder.Services.AddScoped<AddPrototypeToDocumentHandler>();
builder.Services.AddScoped<RemovePrototypeFromDocumentHandler>();

builder.Services.AddScoped<AddComponentToDocumentHandler>();
builder.Services.AddScoped<RemoveComponentFromDocumentHandler>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
