using Npgsql;

namespace recipe_catalog_api.DAOs.Mappers;

public abstract class AbstractMapper<T>
{
    public Func<NpgsqlDataReader, T> MapperFunction { get; }

    public AbstractMapper(Func<NpgsqlDataReader, T> mapperFunction)
    {
        MapperFunction = mapperFunction;
    }

    public T Invoke(NpgsqlDataReader reader) => MapperFunction.Invoke(reader);
}