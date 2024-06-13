namespace Packt.CloudySkiesAir.Chapter9.Flight.Boarding;

public class BoardingProcessor {
  // 當前登機組別
  public int CurrentBoardingGroup { get; set; } = 2;
  // 登機狀態
  public BoardingStatus Status { get; set; }
  // 優先登機的組別
  private int[] _priorityLaneGroups = new[] { 1, 2 };

  // 默認構造函數
  public BoardingProcessor() {
  }

  // 帶參數的構造函數，設置登機狀態和當前登機組別
  public BoardingProcessor(BoardingStatus status, int group) {
    CurrentBoardingGroup = group;
    Status = status;
  }

  // 顯示登機狀態
  public void DisplayBoardingStatus(List<Passenger> passengers, bool? hasBoarded = null) {
    // 根據是否已登機篩選乘客列表
    passengers = passengers.Where(p => !hasBoarded.HasValue ||
                                       p.HasBoarded == hasBoarded)
                           .ToList();

    // 顯示登機頭信息
    DisplayBoardingHeader();

    // 遍歷乘客列表，顯示每位乘客的登機信息
    foreach (Passenger passenger in passengers) {
      string statusMessage = passenger.HasBoarded
        ? "Onboard" // 已登機
        : BuildMessage(passenger); // 生成登機信息

      Console.WriteLine($"{passenger.FullName,-23} Group {passenger.BoardingGroup}: {statusMessage}");
    }
  }

  // 顯示登機頭信息的私有方法
  private void DisplayBoardingHeader() {
    switch (Status) {
      case BoardingStatus.NotStarted:
        Console.WriteLine("Boarding is closed and the plane has departed.");
        break;

      case BoardingStatus.Boarding:
        if (_priorityLaneGroups.Contains(CurrentBoardingGroup)) {
          Console.WriteLine($"Priority Boarding Group {CurrentBoardingGroup}");
        } else {
          Console.WriteLine($"Boarding Group {CurrentBoardingGroup}");
        }
        break;

      case BoardingStatus.PlaneDeparted:
        Console.WriteLine("Boarding is closed and the plane has departed.");
        break;

      default:
        Console.WriteLine($"Unknown Boarding Status: {Status}");
        break;
    }

    Console.WriteLine();
  }

  // 根據乘客信息和登機狀態生成登機信息
  public string BuildMessage(Passenger passenger) {
    bool isMilitary = passenger.IsMilitary; // 是否為軍人
    bool needsHelp = passenger.NeedsHelp; // 是否需要幫助
    int group = passenger.BoardingGroup; // 乘客的登機組別

    return Status switch {
      BoardingStatus.PlaneDeparted => "Flight Departed",
      BoardingStatus.NotStarted => "Boarding Not Started",
      BoardingStatus.Boarding when isMilitary || needsHelp => "Board Now via Priority Lane",
      BoardingStatus.Boarding when CurrentBoardingGroup < group => "Please Wait",
      BoardingStatus.Boarding when _priorityLaneGroups.Contains(group) => "Board Now via Priority Lane",
      BoardingStatus.Boarding => "Board Now",
      _ => throw new NotSupportedException($"Unsupported Status {Status}"),
    };
  }
}

