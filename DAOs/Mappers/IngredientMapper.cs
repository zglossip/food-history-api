using food_history_api.Models;

namespace food_history_api.DAOs.Mappers;

public class IngredientMapper : AbstractMapper<Ingredient>
{
    public IngredientMapper() : base(reader => {
        string? notes = null;
        string? uom = null;

        if(!reader.IsDBNull(reader.GetOrdinal("notes")))
        {
            notes = reader.GetString(reader.GetOrdinal("notes"));
        }

        if(!reader.IsDBNull(reader.GetOrdinal("uom")))
        {
            uom = reader.GetString(reader.GetOrdinal("uom"));
        }

        return new Ingredient(
            reader.GetString(reader.GetOrdinal("name")),
            reader.GetDouble(reader.GetOrdinal("quantity")),
            uom,
            notes
        );
    }){}
}