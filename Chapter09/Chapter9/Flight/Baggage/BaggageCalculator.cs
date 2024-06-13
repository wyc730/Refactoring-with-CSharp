namespace Packt.CloudySkiesAir.Chapter9.Flight.Baggage;

public class BaggageCalculator {
  // 定義攜帶行李的費用
  private const decimal CarryOnFee = 30M;
  // 定義第一件託運行李的費用
  private const decimal FirstBagFee = 40M;
  // 定義額外託運行李的費用
  private const decimal ExtraBagFee = 50M;

  // 定義節假日額外費用的百分比
  public decimal HolidayFeePercent { get; set; } = 0.1M;

  // 計算行李總費用的方法
  public decimal CalculatePrice(int bags, int carryOn,
    int passengers, bool isHoliday) {

    decimal total = 0;

    // 計算攜帶行李的費用
    if (carryOn > 0) {
      decimal fee = carryOn * CarryOnFee;
      Console.WriteLine($"Carry-on: {fee}");
      total += fee;
    }

    // 計算託運行李的費用
    if (bags > 0) {
      decimal bagFee = ApplyCheckedBagFee(bags, passengers);
      Console.WriteLine($"Checked: {bagFee}");
      total += bagFee;
    }

    // 如果是節假日，則計算節假日額外費用
    if (isHoliday) {
      decimal holidayFee = total * HolidayFeePercent;
      Console.WriteLine("Holiday Fee: " + holidayFee);

      total += holidayFee;
    }

    return total;
  }

  // 計算託運行李費用的私有方法
  private static decimal ApplyCheckedBagFee(int bags,
    int passengers) {
    if (bags <= passengers) {
      // 如果每位乘客的託運行李不超過一件，則按第一件託運行李的費用計算
      decimal firstBagFee = bags * FirstBagFee;
      return firstBagFee;
    } else {
      // 如果託運行李超過乘客數，則額外行李按額外託運行李的費用計算
      decimal firstBagFee = passengers * FirstBagFee;
      decimal extraBagFee = (bags - passengers) * ExtraBagFee;
      decimal checkedFee = firstBagFee + extraBagFee;
      return checkedFee;
    }
  }
}

