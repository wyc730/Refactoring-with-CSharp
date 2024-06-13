namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Flights;

public interface ICargoItem
{
    string ItemType { get; } // 貨物類型
    int Quantity { get; } // 數量
    string ManifestText => $"{ItemType} {Quantity}"; // 貨物清單文字
    void LogManifest()
    {
        Console.WriteLine(ManifestText); // 輸出貨物清單
    }
}
