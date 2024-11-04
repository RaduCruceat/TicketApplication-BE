using TicketApplication.Data.Context;
//using TicketApplication.Data.Repositories;
using TicketApplication.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TicketApplication.Validators.GhiseuValidators;
using TicketApplication.Validators.BonValidators;
using TicketApplication.Data.EFRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


builder.Services.AddScoped<IGhiseuService, GhiseuService>();
builder.Services.AddScoped<IBonService, BonService>();



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



// Register DbContext
builder.Services.AddDbContext<BonContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IGhiseuRepositoryEF, GhiseuRepositoryEF>();
builder.Services.AddScoped<IBonRepositoryEF, BonRepositoryEF>();

// Register validators
builder.Services.AddScoped<GhiseuIdValidator>();
builder.Services.AddScoped<AddGhiseuValidator>();
builder.Services.AddScoped<ActiveGhiseuValidator>();
builder.Services.AddScoped<DeleteGhiseuValidator>();
builder.Services.AddScoped<EditGhiseuValidator>();

builder.Services.AddScoped<BonIdValidator>();
builder.Services.AddScoped<AddBonValidator>();
builder.Services.AddScoped<EditBonStatusValidator>();

//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();