using food_history_api.Models;

namespace food_history_api.DAOs.Mappers;

public class IngredientMapper : AbstractMapper<Ingredient>
{
    public IngredientMapper() : base(reader => {
        return new Ingredient(
            reader.GetInt32(reader.GetOrdinal("recipe_id")),
            reader.GetString(reader.GetOrdinal("name")),
            reader.GetInt32(reader.GetOrdinal("quantity")),
            reader.GetString(reader.GetOrdinal("uom")),
            reader.GetString(reader.GetOrdinal("notes"))
        );
    }){}
}