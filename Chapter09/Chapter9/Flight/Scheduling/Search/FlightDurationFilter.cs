namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Search;

public class FlightDurationFilter : FlightFilterBase
{
    public TimeSpan? MinDuration { get; set; }
    public TimeSpan? MaxDuration { get; set; }

    public override bool ShouldInclude(IFlightInfo flight)
    {
        // 檢查最小持續時間是否有值，並且航班的持續時間是否小於最小持續時間
        if (MinDuration.HasValue && flight.Duration < MinDuration)
        {
            return false;
        }
        // 檢查最大持續時間是否有值，並且航班的持續時間是否大於最大持續時間
        if (MaxDuration.HasValue && flight.Duration > MaxDuration)
        {
            return false;
        }
        return true;
    }
}
