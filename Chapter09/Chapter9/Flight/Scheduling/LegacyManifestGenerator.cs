﻿namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling {
    public class LegacyManifestGenerator
    {
        /// <summary>
        /// 生成航班清單
        /// </summary>
        /// <param name="flight">航班資訊</param>
        /// <returns>航班清單</returns>
        public FlightManifest Build(FlightInfo flight)
        {

            // If you're reading this, this code is intentionally a little bad.
            // I had to keep the code simple enough to understand while also
            // illustrate a legacy implementation complex enough to consider rewriting
            // That said, I've seen worse in the workplace, as I imagine you have as well

            var bookings = flight.CurrentBookings;
            var bookedSeats = bookings.Keys.OrderBy(k => k).ToArray();
            var passengers = bookings.Values
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName);

            FlightManifest manifest = new();
            manifest.Arrival = new Airport()
            {
                Code = flight.Arrival.Code,
                Country = flight.Arrival.Country,
                Name = flight.Arrival.Name
            };
            manifest.Departure = new Airport()
            {
                Code = flight.Departure.Code,
                Country = flight.Departure.Country,
                Name = flight.Departure.Name
            };

            int passengerCount = 0;
            foreach (var passenger in bookedSeats)
            {
                passengerCount++;
            }

            manifest.PassengerCount = passengerCount;
            manifest.Passengers = passengers.ToList().ToArray();

            return manifest;
        }
    }

}