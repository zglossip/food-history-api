using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.DAOs.Mappers;
using recipe_catalog_api.DAOs.Util;

using Npgsql;

namespace recipe_catalog_api.DAOs;

public class InstructionDao(IDatabaseConnectionSupplier databaseConnectionSupplier) : IInstructionDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier = databaseConnectionSupplier;

    public Task<List<string>> GetAsync(int recipeId)
    {
        string sql = "SELECT TEXT " +
                     "FROM recipe_catalog.INSTRUCTION " +
                     "WHERE RECIPE_ID = @recipeId " +
                     "ORDER BY POSITION ASC";

        return DaoUtil.QueryForListAsync(_databaseConnectionSupplier.GetConnectionString(),
                      sql,
                      new InstructionMapper(),
                      new List<NpgsqlParameter> { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public Task DeleteAsync(int recipeId)
    {
        string sql = "DELETE FROM recipe_catalog.INSTRUCTION " +
                     "WHERE RECIPE_ID = @recipeId";

        return DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(),
                        sql,
                        new List<NpgsqlParameter> { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public async Task CreateAsync(List<string> instructions, int recipeId)
    {
        string sql = "INSERT INTO recipe_catalog.INSTRUCTION (RECIPE_ID, POSITION, TEXT) " +
                     "VALUES(@recipeId, @position, @text)";

        int position = 0;

        foreach (string instruction in instructions)
        {
            List<NpgsqlParameter> sqlParameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@position", position++),
                new NpgsqlParameter("@text", instruction)
            };

            await DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(),
                            sql,
                            sqlParameters);
        }
    }
}
