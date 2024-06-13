namespace Packt.CloudySkiesAir.Chapter9 {
  // 定義一個用於追蹤里程的類別
  public class MileageTracker {
    // 註冊獎勵里程數，設定為常數
    private const int SignUpBonus = 100;
    // 用於存儲當前里程餘額的屬性
    public int Balance { get; set; } = SignUpBonus;

    // 添加里程到當前餘額的方法
    public void AddMiles(int miles) {
      Balance += miles; // 將傳入的里程數加到餘額上
    }

    // 嘗試使用里程兌換獎勵的方法
    public bool RedeemMiles(int miles) {
      if (Balance >= miles) { // 檢查餘額是否足夠
        Balance -= miles; // 從餘額中扣除兌換的里程數
        return true; // 兌換成功，返回 true
      }
      return false; // 餘額不足，兌換失敗，返回 false
    }
  }
}
