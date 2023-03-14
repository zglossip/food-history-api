namespace food_history_api.DAOs.Mappers;

public class CuisineMapper : AbstractMapper<string>
{
    public CuisineMapper() : base(reader => {
        return reader.GetString(reader.GetOrdinal("text"));
    }){}
}