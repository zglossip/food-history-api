namespace food_history_api.DAOs.Mappers;

public class TagMapper : AbstractMapper<string>
{
    public TagMapper() : base(reader => {
        return reader.GetString(reader.GetOrdinal("text"));
    }){}
}