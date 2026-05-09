namespace recipe_catalog_api.DAOs.Interfaces;

public interface IDatabaseConnectionSupplier
{

    public string GetConnectionString();

}