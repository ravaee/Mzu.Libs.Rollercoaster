namespace Mzu.Libs.Rollercoaster.Unittest;
public class RollerCoasterTests : IDisposable
{

    [Fact]
    public void TestMethodsInSameGroupRunSequentially()
    {

        //Arrenge
        RollerCoasterMethodExecutor.InitialRollerCoasterMethodExecutor(typeof(ExampleService).Assembly);
        RollerCoasterMethodExecutor.StartAllBackgroundService();

        Thread.Sleep(6000);

        //Act
        var hasException = ExampleService.LastException is not null;

        //Assert
        Assert.False(hasException);

    }

    [Fact]
    public void TestMethodsInDifferentGroupsRunInParallel()
    {
        //Arrenge
        RollerCoasterMethodExecutor.InitialRollerCoasterMethodExecutor(typeof(ExampleService).Assembly);
        RollerCoasterMethodExecutor.StartAllBackgroundService();

        Thread.Sleep(6000);

        //Act
        var hasException = ExampleService.LastException2 is not null;

        //Assert
        Assert.True(hasException);
    }

    public void Dispose()
    {
        ExampleService.LastException = null;
        ExampleService.LastException2 = null;

        ExampleService._semaphoreSlim1 = new(1, 1);
        ExampleService._semaphoreSlim2 = new(1, 1);
    }
}