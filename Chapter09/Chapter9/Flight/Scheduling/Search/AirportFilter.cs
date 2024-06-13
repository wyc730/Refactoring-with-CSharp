using Packt.CloudySkiesAir.Chapter9.Flight.Scheduling;

namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Search;

public class AirportFilter : FlightFilterBase
{
    public bool IsDeparture { get; set; }
    public Airport Airport { get; set; }

    // 判斷是否應該包含該航班
    public override bool ShouldInclude(IFlightInfo flight)
    {
        if (IsDeparture)
        {
            // 如果是出發航班，則判斷出發地是否與篩選的機場相同
            return flight.Departure.Location == Airport;
        }
        else
        {
            // 如果是到達航班，則判斷目的地是否與篩選的機場相同
            return flight.Arrival.Location == Airport;
        }
    }
}
