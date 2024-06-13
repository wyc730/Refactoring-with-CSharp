namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Flights;

public abstract record FlightInfoBase : IFlightInfo
{
    public AirportEvent Arrival { get; set; }
    public AirportEvent Departure { get; set; }
    public TimeSpan Duration => Departure.Time - Arrival.Time;
    public string Id { get; set; }
    public FlightStatus Status { get; set; } = FlightStatus.OnTime;

    // 建立飛行識別碼的方法
    public virtual string BuildFlightIdentifier() =>
      $"{Id} {Departure.Location}-{Arrival.Location}";

    // 覆寫 ToString 方法，將飛行識別碼作為字串表示
    public sealed override string ToString() =>
      BuildFlightIdentifier();
}
