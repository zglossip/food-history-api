namespace food_history_api.DAOs.Interfaces;

public interface IDatabaseConnectionSupplier {

    public string GetConnectionString();

}