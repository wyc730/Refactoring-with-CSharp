namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Flights;

public class CargoItem : ICargoItem
{
    public string ItemType { get; set; }
    public int Quantity { get; set; }
    public void LogManifest()
    {
        // 在控制台輸出自訂的貨物清單
        Console.WriteLine($"Customized: {ToString()}");
    }

    public override string ToString() =>
      $"{Quantity} {ItemType}";
}
