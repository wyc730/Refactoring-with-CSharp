using Packt.CloudySkiesAir.Chapter9;
using Shouldly;

namespace Chapter9Tests; 

public class MileageTrackerTests {
    [Fact]
    public void NewAccountShouldHaveStartingBalance() {
        // Arrange - 初始化測試環境
        int expectedMiles = 100; // 預期的起始里程數

        // Act - 執行測試動作
        MileageTracker tracker = new(); // 創建一個新的里程追蹤器實例

        // Assert - 驗證結果
        tracker.Balance.ShouldBe(expectedMiles); // 斷言追蹤器的餘額應該等於預期的起始里程數
    }

    [Fact]
    public void AddMileageShouldIncreaseBalance() {
        // Arrange - 初始化測試環境
        MileageTracker tracker = new(); // 創建一個新的里程追蹤器實例

        // Act - 執行測試動作
        tracker.AddMiles(50); // 向追蹤器添加 50 里程

        // Assert - 驗證結果
        tracker.Balance.ShouldBe(150); // 斷言追蹤器的餘額應該增加到 150
    }

    [Fact]
    public void RemoveMileageShouldDecreaseBalance() {
        // Arrange - 初始化測試環境
        MileageTracker tracker = new(); // 創建一個新的里程追蹤器實例
        tracker.AddMiles(900); // 先向追蹤器添加 900 里程

        // Act - 執行測試動作
        tracker.RedeemMiles(250); // 使用 250 里程

        // Assert - 驗證結果
        tracker.Balance.ShouldBe(750); // 斷言追蹤器的餘額應該減少到 750
    }

    [Fact]
    public void RemoveMileageShouldPreventNegativeBalance() {
        // Arrange - 初始化測試環境
        MileageTracker tracker = new(); // 創建一個新的里程追蹤器實例
        int startingBalance = tracker.Balance; // 獲取起始餘額

        // Act - 執行測試動作
        tracker.RedeemMiles(2500); // 嘗試使用 2500 里程，超過起始餘額

        // Assert - 驗證結果
        tracker.Balance.ShouldBe(startingBalance); // 斷言追蹤器的餘額應該保持不變，因為不允許負餘額
    }

    [Theory]
    [InlineData(900, 250, 750)] // 測試添加 900 里程後使用 250 里程的情況
    [InlineData(0, 2500, 100)] // 測試在起始餘額下嘗試使用 2500 里程的情況
    public void RemoveMileageShouldResultInCorrectBalance(int addAmount, int redeemAmount, int expectedBalance) {
        // Arrange - 初始化測試環境
        MileageTracker tracker = new(); // 創建一個新的里程追蹤器實例
        tracker.AddMiles(addAmount); // 向追蹤器添加指定的里程數

        // Act - 執行測試動作
        tracker.RedeemMiles(redeemAmount); // 使用指定的里程數

        // Assert - 驗證結果
        tracker.Balance.ShouldBe(expectedBalance); // 斷言追蹤器的餘額應該等於預期的餘額
    }
}
