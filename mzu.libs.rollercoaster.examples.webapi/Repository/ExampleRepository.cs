using System.Linq;

namespace mzu.libs.rollercoaster.examples.webapi.Repository;

public class ExampleRepository : IExampleRepository
{
    private static List<string> Data = new(); 

    public async Task<List<string>> GetData()
    {
        await Task.Delay(3000);

        return Data;
    }

    public async Task SetData(string data)
    {
        await Task.Delay(3000);

        Data.Add(data);
    }
}
