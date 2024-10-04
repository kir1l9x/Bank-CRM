using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Xunit;

namespace Lab1.Tests;

public class MyTests
{
    private readonly Train _train = new Train(1000000, 1000, 1);
    private readonly CommonRail _commonRail = new CommonRail(100);
    private readonly ForcedRail _forcedRail = new ForcedRail(100, 100000);

    [Fact]
    public void ShouldPassRoute_WhenRouteLimitAcceptable_ReturnsTrainPassResultSuccess()
    {
        var acceptableSpeedLimitPath = new Route(1000000);
        acceptableSpeedLimitPath.PartsOfPathway.Add(_forcedRail);
        acceptableSpeedLimitPath.PartsOfPathway.Add(_commonRail);

        var expectedResult = new PassingResult(true, 2);
        PassingResult actualResult = _train.TryPassWay(acceptableSpeedLimitPath);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void ShouldNotPassRoute_WhenRouteLimitUnacceptable_ReturnsTrainPassResultFail()
    {
        var unacceptableSpeedLimitPath = new Route(1);

        unacceptableSpeedLimitPath.PartsOfPathway.Add(_forcedRail);
        unacceptableSpeedLimitPath.PartsOfPathway.Add(_commonRail);

        var expectedResult = new PassingResult(false, 2);
        PassingResult actualResult = _train.TryPassWay(unacceptableSpeedLimitPath);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void ShouldPassStationAndRoute_WhenRouteAndStationLimitsAcceptable_ReturnsTrainPassResultSuccess()
    {
        var forcedRail = new ForcedRail(100, 20000);
        var station = new Station(800, 200, 100);
        var path = new Route(1000);
        path.PartsOfPathway.Add(forcedRail);
        path.PartsOfPathway.Add(_commonRail);
        path.PartsOfPathway.Add(station);
        path.PartsOfPathway.Add(_commonRail);

        var expectedResult = new PassingResult(true, 7);
        PassingResult actualResult = _train.TryPassWay(path);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void ShouldNotPassStation_WhenStationLimitsUnacceptable_ReturnsTrainPassResultFail()
    {
        var veryLightTrain = new Train(1000000, 1, 1);
        var forcedRail = new ForcedRail(100, 2000);
        var unacceptableSpeedLimitStation = new Station(1500, 200, 100);
        var path = new Route(1000);

        path.PartsOfPathway.Add(forcedRail);
        path.PartsOfPathway.Add(unacceptableSpeedLimitStation);
        path.PartsOfPathway.Add(_commonRail);

        var expectedResult = new PassingResult(false, 1);
        PassingResult actualResult = veryLightTrain.TryPassWay(path);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void ShouldPassAll_WhenForcedRailsAccelerateHigherThenLimitsAndDecelerateBackToLimitsBeforeChecking_ReturnsTrainPassResultSuccess()
    {
        var train = new Train(1000000, 100, 1);
        var accelerationForcedRail1 = new ForcedRail(100, 2000);
        var decelerationForcedRail1 = new ForcedRail(100, -1500);
        var accelerationForcedRail2 = new ForcedRail(100, 3000);
        var decelerationForcedRail2 = new ForcedRail(100, -2100);
        var station = new Station(30, 200, 100);

        var path = new Route(80);
        path.PartsOfPathway.Add(accelerationForcedRail1);
        path.PartsOfPathway.Add(_commonRail);
        path.PartsOfPathway.Add(decelerationForcedRail1);
        path.PartsOfPathway.Add(station);
        path.PartsOfPathway.Add(_commonRail);
        path.PartsOfPathway.Add(accelerationForcedRail2);
        path.PartsOfPathway.Add(_commonRail);
        path.PartsOfPathway.Add(decelerationForcedRail2);

        var expectedResult = new PassingResult(true, 15);
        PassingResult actualResult = train.TryPassWay(path);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void ShouldNotPass_WhenFirstForcedRailAppliesAccelerationAndSecondAppliesDoubleDeceleration_ReturnsTrainPassResultFail()
    {
        var accelerationForcedRail = new ForcedRail(100, 500);
        var doubleDecelerationForcedRail = new ForcedRail(100, -1000);

        var path = new Route(1000);
        path.PartsOfPathway.Add(accelerationForcedRail);
        path.PartsOfPathway.Add(doubleDecelerationForcedRail);

        var expectedResult = new PassingResult(false, 30);
        PassingResult actualResult = _train.TryPassWay(path);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void ShouldPassStationShouldNotPassRoute_WhenSpeedReachesLowerStationLimitHigherRouteLimitAndDoNotDecelerate_ReturnsTrainPassResultFail()
    {
        var train = new Train(1000000, 100, 1);
        var forcedRail = new ForcedRail(100, 2000);
        var station = new Station(2000, 200, 100);

        var path = new Route(15);
        path.PartsOfPathway.Add(forcedRail);
        path.PartsOfPathway.Add(_commonRail);
        path.PartsOfPathway.Add(station);
        path.PartsOfPathway.Add(_commonRail);

        var expectedResult = new PassingResult(false, 7);
        PassingResult actualResult = train.TryPassWay(path);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void ShouldNotPassAndNotStart_WhenStartIsCommonRailAndNoSpeedNoAcceleration_ReturnsTrainPassResultFail()
    {
        var path = new Route(1000);
        path.PartsOfPathway.Add(_commonRail);

        var expectedResult = new PassingResult(false, 0);
        PassingResult actualResult = _train.TryPassWay(path);

        Assert.Equal(expectedResult, actualResult);
    }
}