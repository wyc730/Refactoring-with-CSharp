using Packt.CloudySkiesAir.Chapter9.Flight.Boarding;

namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling {
    /// <summary>
    /// 重新撰寫的航班清單生成器
    /// </summary>
    public class RewrittenManifestGenerator
    {
        /// <summary>
        /// 建立航班清單
        /// </summary>
        /// <param name="flight">航班資訊</param>
        /// <returns>航班清單</returns>
        public FlightManifest Build(FlightInfo flight)
        {

            IReadOnlyDictionary<string, Passenger> bookings = flight.CurrentBookings;

            return new FlightManifest()
            {
                Departure = flight.Departure,
                Arrival = flight.Arrival,
                PassengerCount = bookings.Count(),
                BookedSeats = bookings.Keys.OrderBy(k => k).ToArray(),
                Passengers = bookings.Values
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToArray()
            };
        }
    }

}