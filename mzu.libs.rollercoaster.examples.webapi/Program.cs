using Microsoft.AspNetCore.Mvc;
using mzu.libs.rollercoaster;
using mzu.libs.rollercoaster.examples.webapi.Repository;
using Mzu.Libs.Rollercoaster;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IExampleRepository, ExampleRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/services/StartAll", () =>
{
	try
	{
        RollerCoasterMethodExecutor.ExecuteRollerCoasterMethods(builder.Services.BuildServiceProvider());

        return Results.Ok("Services are running successfully.");
    }
	catch (Exception e)
	{
        return Results.Problem($"Some problem happened when running the Background Services. [Actual Error = {e.Message}]");
    }
    
})
.WithName("Start All Services")
.WithOpenApi();


app.MapGet("/services/Stop/{name}", ([FromRoute] string name) =>
{
    try
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        RollerCoasterMethodExecutor.StopBackgroundService(name);

        return Results.Ok($"Service [{name}] stopped successfully.");
    }
    catch (Exception e)
    {
        return Results.Problem($"Some problem happened when running the Background Services. [Actual Error = {e.Message}]");
    }

})
.WithName("Stop Service")
.WithOpenApi();

app.MapGet("/services/Start/{name}", ([FromRoute] string name) =>
{
    try
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        RollerCoasterMethodExecutor.StartBackgroundService(name);

        return Results.Ok($"Service [{name}] started successfully.");
    }
    catch (Exception e)
    {
        return Results.Problem($"Some problem happened when running the Background Services. [Actual Error = {e.Message}]");
    }

})
.WithName("Start Service")
.WithOpenApi();



app.Run();


