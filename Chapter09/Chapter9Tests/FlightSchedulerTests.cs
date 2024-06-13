using Bogus;
using Packt.CloudySkiesAir.Chapter9.Flight.Scheduling;
using Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Flights;
using Shouldly;
using System.Diagnostics;

namespace Chapter9Tests;

public class FlightSchedulerTests {
    // 使用 Bogus 庫生成假的機場和航班資訊
    private readonly Faker<Airport> _airportFaker;
    private readonly Faker<PassengerFlightInfo> _flightFaker;

    public FlightSchedulerTests() {
        // 初始化機場資訊生成器
        _airportFaker = new Faker<Airport>()
            .RuleFor(a => a.Country, f => f.Address.Country())
            .RuleFor(a => a.Name, f => f.Address.City())
            .RuleFor(a => a.Code, f => f.Random.String2(3));

        // 初始化航班資訊生成器
        _flightFaker = new Faker<PassengerFlightInfo>()
            .RuleFor(p => p.Id, f => f.Random.AlphaNumeric(length: 6))
            .RuleForType(typeof(int), f => f.Random.Number(min: 16, max: 480))
            .RuleForType(typeof(AirportEvent), f => new AirportEvent() {
                Location = _airportFaker.Generate(),
                Time = f.Date.Future()
            })
            .RuleForType(typeof(FlightStatus), f => f.PickRandom<FlightStatus>());
    }

    [Fact]
    public void ScheduleFlightShouldAddFlight() {
        // Arrange - 初始化測試環境
        FlightScheduler scheduler = new();
        PassengerFlightInfo flight = _flightFaker.Generate(); // 生成一個航班資訊

        // Act - 執行測試動作
        scheduler.ScheduleFlight(flight);

        // Assert - 驗證結果
        IEnumerable<IFlightInfo> result = scheduler.GetAllFlights();
        result.ShouldNotBeNull(); // 結果不應為 null
        result.Count().ShouldBe(1); // 應只有一個航班
        result.ShouldContain(flight); // 結果應包含剛剛添加的航班
    }

    [Fact]
    public void ScheduleFlightShouldAddFlightNoShouldly() {
        // 使用標準的 Xunit 斷言進行測試，不使用 Shouldly
        // Arrange
        FlightScheduler scheduler = new();
        PassengerFlightInfo flight = _flightFaker.Generate();

        // Act
        scheduler.ScheduleFlight(flight);

        // Assert
        var result = scheduler.GetAllFlights();
        Assert.NotNull(result); // 結果不應為 null
        Assert.Equal(1, result.Count()); // 應只有一個航班
        Assert.Contains(flight, result); // 結果應包含剛剛添加的航班
    }

    [Fact]
    public void ScheduleFlightShouldNotBeSlow() {
        // 測試添加航班的操作應該快於 100 毫秒
        // Arrange
        FlightScheduler scheduler = new();
        PassengerFlightInfo flight = _flightFaker.Generate();

        // Act
        Action testAction = () => scheduler.ScheduleFlight(flight);

        // Assert
        TimeSpan maxTime = TimeSpan.FromMilliseconds(100);
        Should.CompleteIn(testAction, maxTime); // 操作應在 100 毫秒內完成
    }

    [Fact]
    public void ScheduleFlightShouldNotBeSlowStopwatch() {
        // 使用 Stopwatch 測試添加航班的操作應該快於 100 毫秒
        // Arrange
        FlightScheduler scheduler = new();
        PassengerFlightInfo flight = _flightFaker.Generate();
        int maxTime = 100;
        Stopwatch stopwatch = new();

        // Act
        stopwatch.Start();
        scheduler.ScheduleFlight(flight);
        stopwatch.Stop();
        long milliSeconds = stopwatch.ElapsedMilliseconds;

        // Assert
        milliSeconds.ShouldBeLessThanOrEqualTo(maxTime); // 操作應在 100 毫秒內完成
    }

    [Fact]
    public void RemoveShouldRemoveFlight() {
        // 測試從調度器中移除航班
        // Arrange
        FlightScheduler scheduler = new();
        PassengerFlightInfo flight = _flightFaker.Generate();
        scheduler.ScheduleFlight(flight); // 先添加一個航班

        // Act
        scheduler.RemoveFlight(flight); // 然後移除該航班

        // Assert
        IEnumerable<IFlightInfo> result = scheduler.GetAllFlights();
        result.ShouldNotBeNull(); // 結果不應為 null
        result.ShouldNotContain(flight); // 結果不應包含被移除的航班
    }
}
