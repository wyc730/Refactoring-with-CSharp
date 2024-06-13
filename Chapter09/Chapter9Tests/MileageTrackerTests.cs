using Packt.CloudySkiesAir.Chapter9;
using Shouldly;

namespace Chapter9Tests; 

public class MileageTrackerTests {
    [Fact]
    public void NewAccountShouldHaveStartingBalance() {
        // Arrange - ��l�ƴ�������
        int expectedMiles = 100; // �w�����_�l���{��

        // Act - ������հʧ@
        MileageTracker tracker = new(); // �Ыؤ@�ӷs�����{�l�ܾ����

        // Assert - ���ҵ��G
        tracker.Balance.ShouldBe(expectedMiles); // �_���l�ܾ����l�B���ӵ���w�����_�l���{��
    }

    [Fact]
    public void AddMileageShouldIncreaseBalance() {
        // Arrange - ��l�ƴ�������
        MileageTracker tracker = new(); // �Ыؤ@�ӷs�����{�l�ܾ����

        // Act - ������հʧ@
        tracker.AddMiles(50); // �V�l�ܾ��K�[ 50 ���{

        // Assert - ���ҵ��G
        tracker.Balance.ShouldBe(150); // �_���l�ܾ����l�B���ӼW�[�� 150
    }

    [Fact]
    public void RemoveMileageShouldDecreaseBalance() {
        // Arrange - ��l�ƴ�������
        MileageTracker tracker = new(); // �Ыؤ@�ӷs�����{�l�ܾ����
        tracker.AddMiles(900); // ���V�l�ܾ��K�[ 900 ���{

        // Act - ������հʧ@
        tracker.RedeemMiles(250); // �ϥ� 250 ���{

        // Assert - ���ҵ��G
        tracker.Balance.ShouldBe(750); // �_���l�ܾ����l�B���Ӵ�֨� 750
    }

    [Fact]
    public void RemoveMileageShouldPreventNegativeBalance() {
        // Arrange - ��l�ƴ�������
        MileageTracker tracker = new(); // �Ыؤ@�ӷs�����{�l�ܾ����
        int startingBalance = tracker.Balance; // ����_�l�l�B

        // Act - ������հʧ@
        tracker.RedeemMiles(2500); // ���ըϥ� 2500 ���{�A�W�L�_�l�l�B

        // Assert - ���ҵ��G
        tracker.Balance.ShouldBe(startingBalance); // �_���l�ܾ����l�B���ӫO�����ܡA�]�������\�t�l�B
    }

    [Theory]
    [InlineData(900, 250, 750)] // ���ղK�[ 900 ���{��ϥ� 250 ���{�����p
    [InlineData(0, 2500, 100)] // ���զb�_�l�l�B�U���ըϥ� 2500 ���{�����p
    public void RemoveMileageShouldResultInCorrectBalance(int addAmount, int redeemAmount, int expectedBalance) {
        // Arrange - ��l�ƴ�������
        MileageTracker tracker = new(); // �Ыؤ@�ӷs�����{�l�ܾ����
        tracker.AddMiles(addAmount); // �V�l�ܾ��K�[���w�����{��

        // Act - ������հʧ@
        tracker.RedeemMiles(redeemAmount); // �ϥΫ��w�����{��

        // Assert - ���ҵ��G
        tracker.Balance.ShouldBe(expectedBalance); // �_���l�ܾ����l�B���ӵ���w�����l�B
    }
}
