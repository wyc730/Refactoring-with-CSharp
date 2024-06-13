using Packt.CloudySkiesAir.Chapter9.Flight.Boarding;

namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling {
    public record FlightManifest
    {
        public Passenger[] Passengers { get; internal set; } // 乘客陣列
        public string[] BookedSeats { get; internal set; } // 預訂的座位陣列
        public int PassengerCount { get; internal set; } // 乘客數量
        public Airport Arrival { get; internal set; } // 到達機場
        public Airport Departure { get; internal set; } // 出發機場
    }
}