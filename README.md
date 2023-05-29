# RollerCoaster Project

The RollerCoaster Project is a lightweight library that allows you to schedule and run methods at specific intervals using background services. It provides an easy way to execute methods periodically without blocking the main thread.

## Features

- Schedule methods to run at specified intervals
- Support for static and non-static methods
- Support for dependency injection within scheduled methods
- Thread-safe execution of methods within the same group
- Ability to name and manage background services

## Getting Started

To use the RollerCoaster Project in your own project, follow these steps:

1. Install the RollerCoaster NuGet package:

```dotnet add package Mzu.Libs.Rollercoaster```

2. Add the RollerCoaster attribute to the methods you want to schedule for periodic execution:
```csharp
[RollerCoaster(interval: 2000)]
public void MyMethod()
{
    // Method implementation
}
```


### Configure for Console apps with no ServiceProvider configured.
Easily just use the **ExecuteRollerCoasterMethods** method without any further configs. 
```csharp
RollerCoasterMethodExecutor.ExecuteRollerCoasterMethods();
```

### Configure for ASP.NET projects.
 
```csharp
builder.Services.RegisterRollerCoaster((options) =>
{
    options.ServiceProvider = builder.Services.BuildServiceProvider();
    options.DefaultInterval = 1000;
});
```

3. Run your application and observe the scheduled methods executing at the specified intervals.

##RollerCoasterAttribute

The RollerCoasterAttribute is used to mark methods for scheduled execution. It provides the following options:

interval: Specifies the interval in milliseconds at which the method should be executed.
group (optional): Specifies the group to which the background service belongs.
name (optional): Specifies a name for the background service.

### RollerCoasterExample
The RollerCoasterExample class is an example class that demonstrates the usage of the RollerCoaster Project. It contains methods marked with the RollerCoasterAttribute for scheduling execution.

```csharp
public class RollerCoasterExample
{
    private readonly IExampleRepository _exampleRepository;

    public RollerCoasterExample(IExampleRepository exampleRepository)
    {
        _exampleRepository = exampleRepository;
    }

    [RollerCoaster(2000, name: "SetDataMethod")]
    public void SetDataMethod()
    {
        // Method implementation
    }

    [RollerCoaster(3000, name: "GetDataMethod")]
    public void GetDataMethod()
    {
        // Method implementation
    }
}
```

##Contributing
Contributions to the RollerCoaster Project are welcome! If you find a bug, have an enhancement request, or want to contribute code, please open an issue or submit a pull request.

##License
The RollerCoaster Project is licensed under the MIT License. See the LICENSE file for more information.
