using Mzu.Libs.Rollercoaster.Unittest.Exceptions;
using System.Diagnostics;

namespace Mzu.Libs.Rollercoaster.Unittest;

internal class ExampleService
{
    public static SemaphoreSlim _semaphoreSlim1 = new(1, 1);
    public static SemaphoreSlim _semaphoreSlim2 = new(1, 1);


    public static ObjectLockException? LastException = null;
    public static ObjectLockException? LastException2 = null;



    [RollerCoaster(200, "group1", "Method1")]
    public void Method1()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _semaphoreSlim1.Wait();

            if (stopwatch.Elapsed > TimeSpan.FromMilliseconds(100))
            {
                throw new ObjectLockException();
            }

            Thread.Sleep(2000);

            _semaphoreSlim1.Release();

        }
        catch
        {
            LastException = new ObjectLockException();
        }
    }

    [RollerCoaster(200, "group1", "Method2")]
    public void Method2()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _semaphoreSlim1.Wait();

            if (stopwatch.Elapsed > TimeSpan.FromMilliseconds(100))
            {
                throw new ObjectLockException();
            }

            Thread.Sleep(2000);
            _semaphoreSlim1.Release();

        }
        catch
        {
            LastException = new ObjectLockException();
        }
    }

    [RollerCoaster(100, "group2", "Method3")]
    public void Method3()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _semaphoreSlim2.Wait(100);

            stopwatch.Stop();

            if (stopwatch.Elapsed > TimeSpan.FromMilliseconds(50))
            {
                throw new ObjectLockException();
            }

            Thread.Sleep(2000);
            _semaphoreSlim2.Release();

        }
        catch 
        {
            LastException2 = new ObjectLockException();
        }

    }

    [RollerCoaster(100, "group3", "Method4")]
    public void Method4()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _semaphoreSlim2.Wait(100);

            stopwatch.Stop();

            if (stopwatch.Elapsed > TimeSpan.FromMilliseconds(50))
            {
                throw new ObjectLockException();
            }

            Thread.Sleep(2000);

            _semaphoreSlim2.Release();
        }
        catch
        {
            LastException2 = new ObjectLockException();
        }

    }
}
