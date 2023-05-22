namespace mzu.libs.rollercoaster.examples.webapi.Repository;

public interface IExampleRepository
{
    Task<List<string>> GetData();
    Task SetData(string data);
}
