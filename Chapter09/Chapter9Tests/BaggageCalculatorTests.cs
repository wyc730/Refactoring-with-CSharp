using Packt.CloudySkiesAir.Chapter9.Flight.Baggage;
using FluentAssertions;

namespace Chapter9Tests;

public class BaggageCalculatorTests {
    // 測試手提行李計算是否正確
    [Fact]
    public void CarryOnBaggageIsPricedCorrectly() {
        // Arrange - 初始化測試環境
        BaggageCalculator calculator = new();
        int carryOnBags = 2; // 手提行李數量
        int checkedBags = 0; // 托運行李數量
        int passengers = 1; // 乘客數量
        bool isHoliday = false; // 是否為假日

        // Act - 執行測試動作
        decimal result = calculator.CalculatePrice(checkedBags, carryOnBags, passengers, isHoliday);

        // Assert - 驗證結果
        result.Should().Be(60m); // 斷言結果應為 60
        result.Should().BePositive().And.BeInRange(50, 70); // 斷言結果應為正數且在 50 到 70 之間
    }

    // 測試第一個托運行李的價格是否正確
    [Fact]
    public void FirstCheckedBagShouldCostExpectedAmount() {
        // Arrange
        BaggageCalculator calculator = new();
        int carryOnBags = 0;
        int checkedBags = 1;
        int passengers = 1;
        bool isHoliday = false;

        // Act
        decimal result = calculator.CalculatePrice(checkedBags, carryOnBags, passengers, isHoliday);

        // Assert
        result.Should().NotBe(0); // 斷言結果不應為 0
        result.Should().BeGreaterThan(20); // 斷言結果應大於 20
        result.Should().Be(40); // 斷言結果應為 40
    }

    // 使用 InlineData 屬性提供多組測試資料，測試行李計算器是否能正確計算價格
    [Theory]
    [InlineData(0, 0, 1, false, 0)] // 測試無行李情況
    [InlineData(2, 3, 2, false, 190)] // 測試非假日多行李情況
    [InlineData(2, 1, 1, false, 100)] // 測試非假日標準行李情況
    [InlineData(2, 3, 2, true, 209)] // 測試假日多行李情況
    public void BaggageCalculatorCalculatesCorrectPrice(int carryOnBags, int checkedBags, int passengers, bool isHoliday, decimal expected) {
        // Arrange
        BaggageCalculator calculator = new();

        // Act
        decimal result = calculator.CalculatePrice(checkedBags, carryOnBags, passengers, isHoliday);

        // Assert
        result.Should().Be(expected); // 斷言計算結果應與預期相符
    }

}