using Packt.CloudySkiesAir.Chapter9.Flight.Scheduling;

namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Search;

public class FlightSearch
{
    public Airport? Depart { get; set; }  // 出發機場
    public Airport? Arrive { get; set; }  // 到達機場
    public DateTime? MinArrive { get; set; }  // 最早到達時間
    public DateTime? MaxArrive { get; set; }  // 最晚到達時間
    public DateTime? MinDepart { get; set; }  // 最早出發時間
    public DateTime? MaxDepart { get; set; }  // 最晚出發時間
    public TimeSpan? MinLength { get; set; }  // 最短飛行時間
    public TimeSpan? MaxLength { get; set; }  // 最長飛行時間
}
