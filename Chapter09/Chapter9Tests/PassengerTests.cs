using Bogus;
using Moq;
using Packt.CloudySkiesAir.Chapter9.Flight.Boarding;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter9Tests; 

public class PassengerTests {
    // 測試乘客的全名是否正確
    [Fact]
    public void PassengerFullNameShouldBeAccurate() {
        // Arrange - 初始化測試環境
        Passenger passenger = new() {
            FirstName = "Dot",
            LastName = "Nette",
        };

        // Act - 執行測試動作
        string name = passenger.FullName;

        // Assert - 驗證結果
        name.ShouldBe("Dot Nette"); // 斷言全名應為 "Dot Nette"
    }

    // 測試登機信息是否正確
    [Fact]
    public void BoardingMessageShouldBeAccurate() {
        // Arrange - 初始化測試環境
        Passenger passenger = new() {
            BoardingGroup = 7,
            FirstName = "Dot",
            LastName = "Nette",
            MailingCity = "Columbus",
            MailingState = "Ohio",
            MailingCountry = "United States",
            MailingPostalCode = "43081",
            Email = "noreply@packt.com",
            RewardsId = "CSA88121",
            RewardMiles = 360,
            IsMilitary = false,
            NeedsHelp = false,
        };
        BoardingProcessor boarding = new(BoardingStatus.Boarding, group:3);

        // Act - 執行測試動作
        string message = boarding.BuildMessage(passenger);

        // Assert - 驗證結果
        message.ShouldBe("Please Wait"); // 斷言登機信息應為 "Please Wait"
    }

    // 使用 Bogus 生成乘客資料，並測試登機信息是否正確
    [Fact]
    public void BoardingMessageShouldBeAccurateWithBogus() {
        Faker<Passenger> faker = BuildPersonFaker(); // 使用 Bogus 建立乘客資料生成器

        Passenger passenger = faker.Generate(); // 生成一個乘客資料
        passenger.BoardingGroup = 7;
        passenger.NeedsHelp = false;
        passenger.IsMilitary = false;

        BoardingProcessor boarding = new(BoardingStatus.Boarding, group: 3);

        // Act - 執行測試動作
        string message = boarding.BuildMessage(passenger);

        // Assert - 驗證結果
        message.ShouldBe("Please Wait"); // 斷言登機信息應為 "Please Wait"
    }

    // 建立一個用於生成乘客資料的 Bogus Faker
    private static Faker<Passenger> BuildPersonFaker() {
        // Arrange - 初始化測試環境
        Faker<Passenger> faker = new();
        faker.RuleFor(p => p.FirstName, f => f.Person.FirstName)
             .RuleFor(p => p.LastName, f => f.Person.LastName)
             .RuleFor(p => p.Email, f => f.Person.Email)
             .RuleFor(p => p.MailingCity, f => f.Address.City())
             .RuleFor(p => p.MailingCountry, f => f.Address.Country())
             .RuleFor(p => p.MailingState, f => f.Address.State())
             .RuleFor(p => p.MailingPostalCode, f => f.Address.ZipCode())
             .RuleFor(p => p.RewardsId, f => f.Rant.Review())
             .RuleFor(p => p.RewardMiles, f => f.Random.Number(int.MaxValue));
        return faker;
    }
}

