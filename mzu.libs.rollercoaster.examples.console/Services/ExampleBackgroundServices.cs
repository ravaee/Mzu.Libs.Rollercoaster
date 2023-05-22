using Mzu.Libs.Rollercoaster;

namespace mzu.libs.rollercoaster.examples.console.Services;

public class ExampleBackgroundServices
{
    private static string Text = "Still Not Changed by Any background Service";

    [RollerCoaster(3000, "Group2")]
    public void MethodA()
    {

        Text = "A";
        Thread.Sleep(3000);
        Console.WriteLine($"Method A : Text is : [{Text}]");

    }

    [RollerCoaster(1000, "Group2")]
    public void MethodB()
    {

        Text = "B";
        Console.WriteLine($"Method B : Text is : [{Text}]");

    }

}
