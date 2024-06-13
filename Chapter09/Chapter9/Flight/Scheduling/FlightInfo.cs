using Packt.CloudySkiesAir.Chapter9.Flight.Boarding;

namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling; 

public class FlightInfo
{

    private readonly Dictionary<string, Passenger> _bookedSeats = new();
    public Airport Departure { get; set; }
    public Airport Arrival { get; set; }

    public IReadOnlyDictionary<string, Passenger> CurrentBookings => _bookedSeats.AsReadOnly();

    // 分配座位給乘客
    public void AssignSeat(Passenger passenger, string seat)
    {
        _bookedSeats[seat.ToLower()] = passenger;
    }

    // 檢查座位是否可用
    public bool IsSeatAvailable(string seat)
    {
        return !_bookedSeats.ContainsKey(seat.ToLower());
    }

}
