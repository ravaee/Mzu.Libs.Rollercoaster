using Mzu.Libs.Rollercoaster.Configurations;

namespace Mzu.Libs.Rollercoaster;

public class RollerCoasterAttribute : Attribute
{
    public int Interval { get; }
    public string? Group { get; }
    public string? Name { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="interval">The default value is 0 and it means the Interval would running
    /// on the DefaultInterval time you already configured in Startup</param>
    /// <param name="group"></param>
    /// <param name="name"></param>
    public RollerCoasterAttribute(
        int interval = 0,
        string group = "DefaultGroup1",
        string name = null!)
    {
        Interval = interval == 0 ? Session.ConfiguredOptions.DefaultInterval : interval;
        Group = group;
        Name = name;
    }
}


