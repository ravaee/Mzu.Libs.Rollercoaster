namespace mzu.libs.rollercoaster.examples.webapi.Repository;

public class ExampleRepository : IExampleRepository
{
    private static List<string> Data = new();

    public List<string> GetData()
    {
        Thread.Sleep(3000);

        return Data;
    }

    public void SetData(string data)
    {
        Thread.Sleep(3000);

        Data.Add(data);
    }
}
