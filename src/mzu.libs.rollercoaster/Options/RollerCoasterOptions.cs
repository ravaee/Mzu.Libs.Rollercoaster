namespace Mzu.Libs.Rollercoaster.Options;

/// <summary>
/// Option object to configure RollerCoaster 
/// </summary>
public class RollerCoasterOptions
{
    /// <summary>
    /// Intervals in Milisecond
    /// </summary>
    public int DefaultInterval { get; set; } = 1000;
    public IServiceProvider? ServiceProvider { get; set; }

}
