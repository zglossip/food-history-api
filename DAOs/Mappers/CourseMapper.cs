namespace recipe_catalog_api.DAOs.Mappers;

public class CourseMapper : AbstractMapper<string>
{
    public CourseMapper() : base(reader =>
    {
        return reader.GetString(reader.GetOrdinal("text"));
    })
    { }
}