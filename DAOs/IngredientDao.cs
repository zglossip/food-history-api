using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.DAOs.Mappers;
using food_history_api.DAOs.Util;

using Npgsql;

namespace food_history_api.DAOs;

public class IngredientDao : IIngredientDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier;

    public IngredientDao(IDatabaseConnectionSupplier databaseConnectionSupplier)
    {
        _databaseConnectionSupplier = databaseConnectionSupplier;
    }
    
    public List<Ingredient> Get(int recipeId)
    {
        string sql = "SELECT NAME, QUANTITY, UOM, NOTES " +
                     "FROM food_history.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId";

        return DaoUtil.QueryForList( _databaseConnectionSupplier.GetConnectionString(), 
                      sql, 
                      new IngredientMapper(),
                      new List<NpgsqlParameter>{new NpgsqlParameter("@recipeId", recipeId)});
    }

    public void Delete(int recipeId)
    {
        string sql = "DELETE FROM food_history.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), 
                        sql, 
                        new List<NpgsqlParameter>{new NpgsqlParameter("@recipeId", recipeId)});
    }

    public void Create(List<Ingredient> ingredientList, int recipeId)
    {
        string sql = "INSERT INTO food_history.INGREDIENT (RECIPE_ID, NAME, QUANTITY, UOM, NOTES) " +
                     "VALUES(@recipeId, @name, @quantity, @uom, @notes)";


        ingredientList.ForEach(ingredient => 
        {
            List<NpgsqlParameter> sqlParameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@name", ingredient.Name),
                new NpgsqlParameter("@quantity", ingredient.Quantity),
                new NpgsqlParameter("@uom", ingredient.Uom == null ? DBNull.Value : ingredient.Uom),
                new NpgsqlParameter("@notes", ingredient.Notes == null ? DBNull.Value : ingredient.Notes)
            };

            DaoUtil.Create(_databaseConnectionSupplier.GetConnectionString(),
                            sql,
                            sqlParameters);
        });
    }

}