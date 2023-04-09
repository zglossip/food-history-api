using food_history_api.Models;

namespace food_history_api.DAOs.Mappers;

public class RecipeMapper : AbstractMapper<Recipe>
{
    public RecipeMapper() : base(reader => 
    {
        Uri? sourceUri = null;
        if (!reader.IsDBNull(reader.GetOrdinal("source")))
        {
            string source = reader.GetString(reader.GetOrdinal("source"));
            if (Uri.TryCreate(source, UriKind.Absolute, out Uri? result))
            {
                sourceUri = result;
            }
        }

        return new Recipe(
            reader.GetInt32(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("name")),
            reader.GetInt32(reader.GetOrdinal("serving_amount")),
            reader.GetString(reader.GetOrdinal("serving_name")),
            sourceUri
        );
    }){}
}