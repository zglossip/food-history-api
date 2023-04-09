using food_history_api.DAOs.Interfaces;
using food_history_api.DAOs.Mappers;
using food_history_api.DAOs.Util;

using Npgsql;

namespace food_history_api.DAOs;

public class InstructionDao : IInstructionDao {

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier;

    public InstructionDao(IDatabaseConnectionSupplier databaseConnectionSupplier)
    {
        _databaseConnectionSupplier = databaseConnectionSupplier;
    }

    public List<string> Get(int recipeId)
    {
        string sql = "SELECT TEXT " +
                     "FROM food_history.INSTRUCTION " +
                     "WHERE RECIPE_ID = @recipeId " +
                     "ORDER BY POSITION ASC";

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), 
                      sql, 
                      new InstructionMapper(),
                      new List<NpgsqlParameter>{new NpgsqlParameter("@recipeId", recipeId)});
    }

    public void Delete(int recipeId)
    {
        string sql = "DELETE FROM food_history.INSTRUCTION " +
                     "WHERE RECIPE_ID = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), 
                        sql, 
                        new List<NpgsqlParameter>{new NpgsqlParameter("@recipeId", recipeId)});
    }

    public void Create(List<string> instructions, int recipeId)
    {
        string sql = "INSERT INTO food_history.INSTRUCTION (RECIPE_ID, POSITION, TEXT) " +
                     "VALUES(@recipeId, @position, @text)";

        int position = 0;

        instructions.ForEach(instruction => 
        {
            List<NpgsqlParameter> sqlParameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@position", position++),
                new NpgsqlParameter("@text", instruction)
            };

            DaoUtil.Create(_databaseConnectionSupplier.GetConnectionString(),
                            sql,
                            sqlParameters);
        });
    }
}