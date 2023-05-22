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
        interval: 3000,
        group: nameof(ExampleBackgroundServices),
        name: nameof(ExampleBackgroundServices.SetData))]
    public async Task SetData()
    {
        Console.WriteLine("Set Data ...");

        await _repository.SetData(StringGenerator.GenerateMeaningfulString(10));
    }


    [RollerCoaster(
        interval: 1000,
        group: nameof(ExampleBackgroundServices),
        name: nameof(ExampleBackgroundServices.GetAll))]
    public async Task GetAll()
    {
        var data = await _repository.GetData();

        Console.WriteLine($"Fetch Data => {string.Join(',', data)}");
    }

}
