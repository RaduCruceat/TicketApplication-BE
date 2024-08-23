using TicketApplication.Data.Context;
using TicketApplication.Data.Repositories;
using TicketApplication.Services;
//using TicketApplication.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TicketApplication.Migrations;
using TicketApplication.Data.Context;
using TicketApplication.Data.Repositories;
using TicketApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<IGhiseuService, GhiseuService>();
//builder.Services.AddScoped<IBonService, BonService>();


builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<CreateGhiseuValidator>();

string? connectionString = builder.Configuration.GetConnectionString("SqlServer");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'SqlServer' not found.");
}

// Run migrations
MigrationCode.RunMigrations(connectionString);

// Register repositories
//builder.Services.AddScoped<IGhiseuRepository>(sp =>
//{
//    return new GhiseuRepository(connectionString);
//});
//builder.Services.AddScoped<IBonRepository>(sp =>
//{
//    return new BonRepository(connectionString);
//});
//
// Register DbContext
builder.Services.AddDbContext<BonContext>(options =>
    options.UseSqlServer(connectionString));

// Register validators
//builder.Services.AddScoped<GhiseuIdValidator>();
//builder.Services.AddScoped<CreateGhiseuValidator>();
//builder.Services.AddScoped<UpdateGhiseuValidator>();
//builder.Services.AddScoped<DeleteGhiseuValidator>();
//builder.Services.AddScoped<BonIdValidator>();
//builder.Services.AddScoped<CreateBonValidator>();
//builder.Services.AddScoped<UpdateBonValidator>();
//builder.Services.AddScoped<DeleteBonValidator>();
//
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