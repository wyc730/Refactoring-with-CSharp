using Packt.CloudySkiesAir.Chapter9.Flight.Baggage;
using FluentAssertions;

namespace Chapter9Tests;

public class BaggageCalculatorTests {
    // ���դⴣ����p��O�_���T
    [Fact]
    public void CarryOnBaggageIsPricedCorrectly() {
        // Arrange - ��l�ƴ�������
        BaggageCalculator calculator = new();
        int carryOnBags = 2; // �ⴣ����ƶq
        int checkedBags = 0; // ���B����ƶq
        int passengers = 1; // ���ȼƶq
        bool isHoliday = false; // �O�_������

        // Act - ������հʧ@
        decimal result = calculator.CalculatePrice(checkedBags, carryOnBags, passengers, isHoliday);

        // Assert - ���ҵ��G
        result.Should().Be(60m); // �_�����G���� 60
        result.Should().BePositive().And.BeInRange(50, 70); // �_�����G�������ƥB�b 50 �� 70 ����
    }

    // ���ղĤ@�Ӧ��B���������O�_���T
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
        result.Should().NotBe(0); // �_�����G������ 0
        result.Should().BeGreaterThan(20); // �_�����G���j�� 20
        result.Should().Be(40); // �_�����G���� 40
    }

    // �ϥ� InlineData �ݩʴ��Ѧh�մ��ո�ơA���զ���p�⾹�O�_�ॿ�T�p�����
    [Theory]
    [InlineData(0, 0, 1, false, 0)] // ���յL������p
    [InlineData(2, 3, 2, false, 190)] // ���իD����h������p
    [InlineData(2, 1, 1, false, 100)] // ���իD����зǦ�����p
    [InlineData(2, 3, 2, true, 209)] // ���հ���h������p
    public void BaggageCalculatorCalculatesCorrectPrice(int carryOnBags, int checkedBags, int passengers, bool isHoliday, decimal expected) {
        // Arrange
        BaggageCalculator calculator = new();

        // Act
        decimal result = calculator.CalculatePrice(checkedBags, carryOnBags, passengers, isHoliday);

        // Assert
        result.Should().Be(expected); // �_���p�⵲�G���P�w���۲�
    }

}