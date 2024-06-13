using Packt.CloudySkiesAir.Chapter9.Flight.Scheduling;

namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Search;

public abstract class FlightFilterBase
{
    // 定義抽象方法 ShouldInclude，用於判斷是否應該包含該航班
    public abstract bool ShouldInclude(IFlightInfo flight);
}
