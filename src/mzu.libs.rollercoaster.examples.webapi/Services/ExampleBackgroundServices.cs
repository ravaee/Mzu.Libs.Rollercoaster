using mzu.libs.rollercoaster.examples.webapi.Helpers;
using mzu.libs.rollercoaster.examples.webapi.Repository;
using Mzu.Libs.Rollercoaster;

namespace mzu.libs.rollercoaster.examples.WebApi.Services;

public class ExampleBackgroundServices
{

    private readonly IExampleRepository _repository;


    public ExampleBackgroundServices(IExampleRepository repository)
    {
        _repository = repository;
    }


    [RollerCoaster(
        interval: 1000,
        group: nameof(ExampleBackgroundServices),
        name: nameof(SetData))]
    public void SetData()
    {
        Console.WriteLine("Start setting Data ...");

        _repository.SetData(StringGenerator.GenerateMeaningfulString(10));

        Console.WriteLine("Data has Set");
        Console.WriteLine("-------------------------");
    }


    [RollerCoaster(
        interval: 1000,
        group: nameof(ExampleBackgroundServices),
        name: nameof(GetAll))]
    public void GetAll()
    {
        Console.WriteLine("Start getting Data ...");

        var data = _repository.GetData();

        Console.WriteLine($"Fetch Data => {string.Join(',', data)}");
        Console.WriteLine("-------------------------");
    }

}
