using Mzu.Libs.Rollercoaster.Configurations;

namespace Mzu.Libs.Rollercoaster;

public class RollerCoasterAttribute : Attribute
{
    public string? Group { get; }
    public string? Name { get; }
    public int DefaultInterval { get; }
    public int Interval
    {
        get
        {
            string? envIntervalStr = Environment.GetEnvironmentVariable(Name + "_Interval");
 
            if (!string.IsNullOrEmpty(envIntervalStr) && int.TryParse(envIntervalStr, out int envInterval))
            {
                return envInterval;
            }
 
            return DefaultInterval;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="interval">The default value is 0 and it means the Interval would running
    /// on the DefaultInterval time you already configured in Startup</param>
    /// <param name="group"></param>
    /// <param name="name"></param>
    public RollerCoasterAttribute(int interval = 0, string? group = null, string? name = null)
    {
        DefaultInterval = interval <= 0 ? Session.ConfiguredOptions.DefaultInterval: interval;
        Group = group;
        Name = name;
    }
}


