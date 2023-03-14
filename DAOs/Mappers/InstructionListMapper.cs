namespace food_history_api.DAOs.Mappers;

public class InstructionMapper : AbstractMapper<string>
{
    public InstructionMapper() : base(reader => {
        return reader.GetString(reader.GetOrdinal("text"));
    }){}
}