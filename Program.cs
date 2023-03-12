using food_history_api.Services;
using food_history_api.Services.Interfaces;
using food_history_api.DAOs;
using food_history_api.DAOs.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//DI Setup
builder.Services.AddSingleton<IDatabaseConnectionSupplier, DatabaseConnectionSupplier>();

builder.Services.AddScoped<IIngredientDao, IngredientDao>();
builder.Services.AddScoped<IInstructionDao, InstructionDao>();
builder.Services.AddScoped<IRecipeDao, RecipeDao>();

builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IInstructionService, InstructionService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

// Add services to the container.

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
