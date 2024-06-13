namespace Packt.CloudySkiesAir.Chapter9.Flight.Scheduling.Flights;

public record FreightFlightInfo : FlightInfoBase
{
    public string CharterCompany { get; set; }
    public string Cargo { get; set; }

    // 覆寫 BuildFlightIdentifier 方法，加入貨物和包機公司資訊
    public override string BuildFlightIdentifier() =>
      base.BuildFlightIdentifier() +
      $" {Cargo} for {CharterCompany}";
}
