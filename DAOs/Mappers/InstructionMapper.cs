namespace recipe_catalog_api.DAOs.Mappers;

public class InstructionMapper : AbstractMapper<string>
{
    public InstructionMapper() : base(reader =>
    {
        return reader.GetString(reader.GetOrdinal("text"));
    })
    { }
}