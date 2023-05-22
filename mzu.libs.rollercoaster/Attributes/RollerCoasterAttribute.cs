namespace Mzu.Libs.Rollercoaster;

public class RollerCoasterAttribute : Attribute
{
    public int Interval { get; }
    public string Group { get; }
    public string Name { get; }

    public RollerCoasterAttribute(int interval, string group = "DefaultGroup1", string name = "DefaultName1")
    {
        Interval = interval;
        Group = group;
        Name = name;
    }
}
