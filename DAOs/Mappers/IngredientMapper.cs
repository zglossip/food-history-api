using food_history_api.Models;

namespace food_history_api.DAOs.Mappers;

public class IngredientMapper : AbstractMapper<Ingredient>
{
    public IngredientMapper() : base(reader => {
        string? notes = null;

        if(!reader.IsDBNull(reader.GetOrdinal("notes")))
        {
            notes = reader.GetString(reader.GetOrdinal("notes"));
        }

        return new Ingredient(
            reader.GetString(reader.GetOrdinal("name")),
            reader.GetInt32(reader.GetOrdinal("quantity")),
            reader.GetString(reader.GetOrdinal("uom")),
            notes
        );
    }){}
}