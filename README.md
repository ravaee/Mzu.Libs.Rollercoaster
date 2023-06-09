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

### Overriding the Interval with Environment Variables

RollerCoaster allows the intervals defined for methods to be overridden at runtime using environment variables. This enables the adjustment of execution intervals in different environments without the need for code changes. 


Define an environment variable with a name matching the `Name` value given in the `RollerCoaster` attribute, appended with `_Interval`.

For example, if you have a method defined with the `RollerCoaster` attribute as follows:
   
   ```csharp
   [RollerCoaster(2000, "group1", "Method1")]
   public void Method1()
   {
       // method logic here
   }
   ```
The corresponding environment variable would be named Method1_Interval.

### Setting Environment Variables in ASP.NET Core

The value of the environment variable should represent the desired interval in milliseconds.
Setting Environment Variables in ASP.NET Core
You can set environment variables in ASP.NET Core applications by using the appsettings.json or appsettings.{Environment}.json files. Here's an example of how you could set the Method1_Interval variable in the appsettings.json file:

```json
{
  "Method1_Interval": "5000"
}
```

This will set the interval for Method1 to 5 seconds in all environments. If you want to set a different interval for a specific environment (like Development), you could do this in the appsettings.Development.json file:

```json
{
  "Method1_Interval": "10000"
}
```
This will set the interval for Method1 to 10 seconds in the Development environment.

### Setting Environment Variables in Docker
If you're running your application in a Docker container, you can set environment variables using the -e option with the docker run command:

```docker
docker run -e Method1_Interval=5000 -d my-aspnet-app
```

## Contributing
Contributions to the RollerCoaster Project are welcome! If you find a bug, have an enhancement request, or want to contribute code, please open an issue or submit a pull request.

## License
The RollerCoaster Project is licensed under the MIT License. See the LICENSE file for more information.
