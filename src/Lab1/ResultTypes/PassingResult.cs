namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public abstract record PassingResult
{
    private PassingResult(double time)
    {
        Time = time;
    }

    public double Time { get; set; }

    public sealed record Success : PassingResult
    {
        public Success(double time) : base(time) { }
    }

    public sealed record SpeedLowerThenZero : PassingResult
    {
        public SpeedLowerThenZero(double time) : base(time) { }
    }

    public sealed record TooLargeForceToTrain : PassingResult
    {
        public TooLargeForceToTrain(double time) : base(time) { }
    }

    public sealed record TooHighSpeedForStation : PassingResult
    {
        public TooHighSpeedForStation(double time) : base(time) { }
    }

    public sealed record TooHighSpeedForRoute : PassingResult
    {
        public TooHighSpeedForRoute(double time) : base(time) { }
    }

    public sealed record SpeedIsNonPositiveOnCommonRail : PassingResult
    {
        public SpeedIsNonPositiveOnCommonRail(double time) : base(time) { }
    }
}