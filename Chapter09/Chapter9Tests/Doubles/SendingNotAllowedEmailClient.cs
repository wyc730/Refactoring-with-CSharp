using Packt.CloudySkiesAir.Chapter9.Flight.Scheduling;

namespace Chapter9Tests.Doubles;

// 這個類別是一個測試雙重，用於模擬一個不允許發送郵件的郵件客戶端
public class SendingNotAllowedEmailClient : IEmailClient {
    // 當嘗試發送郵件時，這個方法會被調用並且會導致測試失敗
    public bool SendMessage(string email, string message) {
        Assert.Fail("You should not have sent an E-Mail"); // 斷言失敗，提示不應該發送郵件
        return false; // 返回 false 表示郵件未發送
    }
}
