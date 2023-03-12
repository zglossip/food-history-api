using food_history_api.Models;
using food_history_api.DAOs.Interfaces;

using System.Data.SqlClient;
using Npgsql;

namespace food_history_api.DAOs;

public class InstructionDao : IInstructionDao {

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier;

    public InstructionDao(IDatabaseConnectionSupplier databaseConnectionSupplier)
    {
        _databaseConnectionSupplier = databaseConnectionSupplier;
    }

    public InstructionList Get(int recipeId)
    {
        List<string> instructionList = new List<string>();

        string sql = "SELECT TEXT " +
                     "FROM food_history.INSTRUCTION " +
                     "WHERE RECIPE_ID = @recipeId " +
                     "ORDER BY POSITION ASC";

        DaoUtil.Query((DaoUtil.GetInstructionListAction(instructionList)), 
                      _databaseConnectionSupplier.GetConnectionString(), 
                      sql, 
                      new List<NpgsqlParameter>{new NpgsqlParameter("@recipeId", recipeId)});

        return new InstructionList(recipeId, instructionList);
    }

    public void Delete(int recipeId)
    {
        string sql = "DELETE FROM food_history.INSTRUCTION " +
                     "WHERE RECIPE_ID = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), 
                        sql, 
                        new List<NpgsqlParameter>{new NpgsqlParameter("@recipeId", recipeId)});
    }

    public void Create(InstructionList instructionList)
    {
        string sql = "INSERT INTO food_history.INSTRUCTION (RECIPE_ID, POSITION, TEXT) " +
                     "VALUES(@recipeId, @position, @text)";

        int position = 0;

        instructionList.Instructions.ForEach(instruction => 
        {
            List<NpgsqlParameter> sqlParameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@recipeId", instructionList.RecipeId),
                new NpgsqlParameter("@position", position++),
                new NpgsqlParameter("@text", instruction)
            };

            DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(),
                            sql,
                            sqlParameters);
        });
    }
}