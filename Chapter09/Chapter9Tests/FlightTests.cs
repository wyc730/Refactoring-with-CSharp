using Packt.CloudySkiesAir.Chapter9.Flight;
using Shouldly;

namespace Chapter9Tests; 

public class FlightTests {
    [Fact]
    public void GeneratedMessageShouldBeCorrect() {
        // Arrange - 初始化測試環境
        Flight flight = new(); // 創建一個 Flight 實例
        string id = "CSA1234"; // 定義航班 ID
        string status = "On Time"; // 定義航班狀態

        // Act - 執行測試動作
        string message = flight.BuildMessage(id, status); // 生成航班狀態消息

        // Assert - 驗證結果        
        message.ShouldBe("Flight CSA1234 is On Time"); // 斷言生成的消息應該符合預期格式
    }
}