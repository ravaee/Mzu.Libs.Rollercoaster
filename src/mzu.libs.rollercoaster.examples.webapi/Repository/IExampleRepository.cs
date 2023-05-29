namespace mzu.libs.rollercoaster.examples.webapi.Repository;

public interface IExampleRepository
{
    List<string> GetData();
    void SetData(string data);
}
