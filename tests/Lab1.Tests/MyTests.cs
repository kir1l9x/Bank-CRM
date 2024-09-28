using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Xunit;

namespace Lab1.Tests;

public class MyTests
{
    [Fact]
    public void Test1()
    {
        var train = new Train(1000000, 1000, 1);
        var forcedRail = new ForcedRail(100, 100000);
        var commonRail = new CommonRail(100);

        var path = new Route(1000000);
        path.PartsOfPathway.Add(forcedRail);
        path.PartsOfPathway.Add(commonRail);

        var expectedResult = new WayPassingResult(true, 2);

        Assert.Equal(expectedResult, train.TryPassWay(path));
    }

    [Fact]
    public void Test2()
    {
        var train = new Train(1000000, 1000, 1);
        var forcedRail = new ForcedRail(100, 100000);
        var commonRail = new CommonRail(100);
        var path = new Route(1);

        path.PartsOfPathway.Add(forcedRail);
        path.PartsOfPathway.Add(commonRail);

        var expectedResult = new WayPassingResult(false, 2);

        Assert.Equal(expectedResult, train.TryPassWay(path));
    }

    [Fact]
    public void Test3()
    {
        var train = new Train(1000000, 1000, 1);
        var forcedRail = new ForcedRail(100, 20000);
        var commonRail = new CommonRail(100);
        var station = new Station(800, 200, 100);
        var path = new Route(1000);
        path.PartsOfPathway.Add(forcedRail);
        path.PartsOfPathway.Add(commonRail);
        path.PartsOfPathway.Add(station);
        path.PartsOfPathway.Add(commonRail);

        var expectedResult = new WayPassingResult(true, 7);

        Assert.Equal(expectedResult, train.TryPassWay(path));
    }

    [Fact]
    public void Test4()
    {
        var train = new Train(1000000, 1, 1);
        var forcedRail = new ForcedRail(100, 2000);
        var station = new Station(1500, 200, 100);
        var commonRail = new CommonRail(100);

        var path = new Route(1000);
        path.PartsOfPathway.Add(forcedRail);
        path.PartsOfPathway.Add(station);
        path.PartsOfPathway.Add(commonRail);

        var expectedResult = new WayPassingResult(false, 1);

        Assert.Equal(expectedResult, train.TryPassWay(path));
    }

    [Fact]
    public void Test5()
    {
        var train = new Train(1000000, 100, 1);
        var forcedRail = new ForcedRail(100, 2000);
        var commonRail = new CommonRail(100);
        var station = new Station(2000, 200, 100);

        var path = new Route(15);
        path.PartsOfPathway.Add(forcedRail);
        path.PartsOfPathway.Add(commonRail);
        path.PartsOfPathway.Add(station);
        path.PartsOfPathway.Add(commonRail);

        var expectedResult =
            new WayPassingResult(false, 7);

        Assert.Equal(expectedResult, train.TryPassWay(path));
    }

    [Fact]
    public void Test6()
    {
        var train = new Train(1000000, 100, 1);
        var forcedRail1 = new ForcedRail(100, 2000);
        var forcedRail2 = new ForcedRail(100, -1500);
        var forcedRail3 = new ForcedRail(100, 3000);
        var forcedRail4 = new ForcedRail(100, -2100);
        var station = new Station(30, 200, 100);
        var commonRail = new CommonRail(100);

        var path = new Route(80);
        path.PartsOfPathway.Add(forcedRail1);
        path.PartsOfPathway.Add(commonRail);
        path.PartsOfPathway.Add(forcedRail2);
        path.PartsOfPathway.Add(station);
        path.PartsOfPathway.Add(commonRail);
        path.PartsOfPathway.Add(forcedRail3);
        path.PartsOfPathway.Add(commonRail);
        path.PartsOfPathway.Add(forcedRail4);

        var expectedResult = new WayPassingResult(true, 15);

        Assert.Equal(expectedResult, train.TryPassWay(path));
    }

    [Fact]
    public void Test7()
    {
        var train = new Train(1000000, 1000, 1);
        var commonRail = new CommonRail(100);

        var path = new Route(1000);
        path.PartsOfPathway.Add(commonRail);

        var expectedResult = new WayPassingResult(false, 0);

        Assert.Equal(expectedResult, train.TryPassWay(path));
    }

    [Fact]
    public void Test8()
    {
        var train = new Train(1000000, 100, 1);
        var forcedRail1 = new ForcedRail(100, 500);
        var forcedRail2 = new ForcedRail(100, -1000);

        var path = new Route(1000);
        path.PartsOfPathway.Add(forcedRail1);
        path.PartsOfPathway.Add(forcedRail2);

        var expectedResult = new WayPassingResult(false, 9);

        Assert.Equal(expectedResult, train.TryPassWay(path));
    }
}