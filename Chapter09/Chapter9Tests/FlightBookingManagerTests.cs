using Chapter9Tests.Doubles;
using GitHub;
using Moq;
using NSubstitute;
using Packt.CloudySkiesAir.Chapter9.Flight.Boarding;
using Packt.CloudySkiesAir.Chapter9.Flight.Scheduling;
using Snapper;
using Snapper.Attributes;

namespace Chapter9Tests;

public class FlightBookingManagerTests {
    // 測試使用測試雙重對象預訂空航班應成功
    [Fact]
    public void BookingFlightShouldSucceedForEmptyFlightTestDouble() {
        // Arrange - 初始化測試環境
        TestEmailClient emailClient = new(); // 使用測試雙重的郵件客戶端
        FlightBookingManager manager = new(emailClient); // 創建預訂管理器實例
        Passenger passenger = GenerateTestPassenger(); // 生成測試乘客
        FlightInfo flight = GenerateEmptyFlight("Paris", "Toronto"); // 生成空航班信息

        // Act - 執行預訂動作
        bool booked = manager.BookFlight(passenger, flight, "2B");

        // Assert - 驗證預訂成功
        booked.ShouldBeTrue();
    }

    // 測試使用 Moq 預訂空航班應成功
    [Fact]
    public void BookingFlightShouldSucceedForEmptyFlight() {
        // Arrange
        Mock<IEmailClient> clientMock = new(); // 使用 Moq 創建郵件客戶端模擬對象
        IEmailClient emailClient = clientMock.Object;
        FlightBookingManager manager = new(emailClient);
        Passenger passenger = GenerateTestPassenger();
        FlightInfo flight = GenerateEmptyFlight("Hamburg", "Cairo");

        // Act
        bool booked = manager.BookFlight(passenger, flight, "2B");

        // Assert
        booked.ShouldBeTrue();
    }

    // 測試預訂航班後應發送郵件
    [Fact]
    public void BookingFlightShouldSendEmails() {
        // Arrange
        Mock<IEmailClient> mockClient = new(); // 使用 Moq 創建郵件客戶端模擬對象
        mockClient.Setup(c => c.SendMessage(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        IEmailClient emailClient = mockClient.Object;

        FlightBookingManager manager = new(emailClient);
        Passenger passenger = GenerateTestPassenger();
        FlightInfo flight = GenerateEmptyFlight("Sydney", "Los Angelas");

        // Act
        bool result = manager.BookFlight(passenger, flight, "22C");

        // Assert
        result.ShouldBeTrue();
        mockClient.Verify(c => c.SendMessage(passenger.Email, It.IsAny<string>()), Times.Once); // 驗證是否發送了一次郵件
        mockClient.VerifyNoOtherCalls(); // 驗證沒有其他調用
    }

    // 使用 NSubstitute 測試預訂航班後應發送郵件
    [Fact]
    public void BookingFlightShouldSendEmailsNSubstitute() {
        // Arrange
        IEmailClient emailClient = Substitute.For<IEmailClient>(); // 使用 NSubstitute 創建郵件客戶端模擬對象
        emailClient.SendMessage(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

        FlightBookingManager manager = new(emailClient);
        Passenger passenger = GenerateTestPassenger();
        FlightInfo flight = GenerateEmptyFlight("Sydney", "Los Angelas");

        // Act
        bool result = manager.BookFlight(passenger, flight, "22C");

        // Assert
        result.ShouldBeTrue();
        emailClient.Received().SendMessage(passenger.Email, Arg.Any<string>()); // 驗證是否發送了郵件
    }

    // 生成測試用乘客
    private static Passenger GenerateTestPassenger() {
        return new() {
            FirstName = "Dot",
            LastName = "Nette",
            Email = "noreply@packt.com"
        };
    }

    // 使用 Snapper 進行快照測試，驗證航班名單是否符合預期
    [Fact]
    [UpdateSnapshots]
    public void FlightManifestShouldMatchExpectations() {
        // Arrange
        FlightInfo flight = GenerateEmptyFlight("Alta", "Laos");
        Passenger p1 = new("Dot", "Netta");
        Passenger p2 = new("See", "Sharp");
        flight.AssignSeat(p1, "1A");
        flight.AssignSeat(p2, "1B");
        LegacyManifestGenerator generator = new();

        // Act
        FlightManifest manifest = generator.Build(flight);

        // Assert
        manifest.ShouldMatchSnapshot(); // 使用快照進行驗證
    }

    // 使用科學家模式進行實驗，比較兩種航班名單生成器的結果
    [Fact]
    public void FlightManifestExperimentWithScientist() {
        FlightInfo flight = GenerateEmptyFlight("Buenos Ares", "Laos");
        Passenger p1 = new("Dot", "Netta");
        Passenger p2 = new("See", "Sharp");

        Scientist.Science<FlightManifest>("build flight manifest", exp => {
            exp.Use(() => {
                LegacyManifestGenerator generator = new();
                return generator.Build(flight);
            });
            exp.Try(() => {
                RewrittenManifestGenerator generator = new();
                return generator.Build(flight);
            });
            exp.Compare((a, b) => a.Arrival == b.Arrival &&
                                  a.Departure == b.Departure &&
                                  a.PassengerCount == b.PassengerCount);
            exp.ThrowOnMismatches = true; // 當結果不匹配時拋出異常
        });
    }

    // 生成空航班信息
    private static FlightInfo GenerateEmptyFlight(string from, string to) {
        return new() {
            Departure = new Airport() { Name = from },
            Arrival = new Airport() { Name = to },
        };
    }
}
