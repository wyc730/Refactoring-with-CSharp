using Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Search;

namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling
{
    /// <summary>
    /// 航班排程器
    /// </summary>
    public class FlightScheduler
    {
        private readonly List<IFlightInfo> _flights = new();

        /// <summary>
        /// 排程航班
        /// </summary>
        /// <param name="flight">要排程的航班</param>
        public void ScheduleFlight(IFlightInfo flight)
        {
            _flights.Add(flight);

            for (int i = 0; i < 10000; i++)
                Console.WriteLine($"Scheduled Flight {flight}");
        }

        /// <summary>
        /// 移除航班
        /// </summary>
        /// <param name="flight">要移除的航班</param>
        public void RemoveFlight(IFlightInfo flight)
        {
            _flights.Remove(flight);
        }

        /// <summary>
        /// 取得所有航班
        /// </summary>
        /// <returns>所有航班的集合</returns>
        public IEnumerable<IFlightInfo> GetAllFlights()
        {
            return _flights.AsReadOnly();
        }

        /// <summary>
        /// 根據搜尋條件進行航班搜尋
        /// </summary>
        /// <param name="s">航班搜尋條件</param>
        /// <returns>符合搜尋條件的航班集合</returns>
        public IEnumerable<IFlightInfo> Search(FlightSearch s)
        {
            IEnumerable<IFlightInfo> results = _flights;

            if (s.Depart != null)
            {
                results = results.Where(f => f.Departure.Location == s.Depart);
            }

            if (s.Arrive != null)
            {
                results = results.Where(f => f.Arrival.Location == s.Arrive);
            }

            if (s.MinDepart != null)
            {
                results = results.Where(f => f.Departure.Time >= s.MinDepart);
            }

            if (s.MaxDepart != null)
            {
                results = results.Where(f => f.Departure.Time <= s.MaxDepart);
            }

            if (s.MinArrive != null)
            {
                results = results.Where(f => f.Arrival.Time >= s.MinArrive);
            }

            if (s.MaxArrive != null)
            {
                results = results.Where(f => f.Arrival.Time <= s.MaxArrive);
            }

            if (s.MinLength != null)
            {
                results = results.Where(f => f.Duration >= s.MinLength);
            }

            if (s.MaxLength != null)
            {
                results = results.Where(f => f.Duration <= s.MaxLength);
            }

            return results;
        }

        /// <summary>
        /// 根據搜尋規則進行航班搜尋
        /// </summary>
        /// <param name="rules">航班搜尋規則</param>
        /// <returns>符合搜尋規則的航班集合</returns>
        public List<IFlightInfo> Search(List<FlightFilterBase> rules) =>
            _flights.Where(f => rules.All(r => r.ShouldInclude(f)))
                    .ToList();
    }
}
