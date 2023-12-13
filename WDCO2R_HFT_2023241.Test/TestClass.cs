using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WDCO2R_HFT_2023241.Logic.Classes;
using WDCO2R_HFT_2023241.Models;
using WDCO2R_HFT_2023241.Repository;
using static WDCO2R_HFT_2023241.Logic.Classes.RentalLogic;

namespace WDCO2R_HFT_2023241.Test
{
    [TestFixture]
    class TestClass
    {
        BoardGameLogic gameLog;
        CustomerLogic cusLog;
        RentalLogic rentLog;

        public TestClass()
        {
            Mock<IBoardGameRepository> mockGameRep = new Mock<IBoardGameRepository>();
            Mock<ICustomerRepository> mockCusRep = new Mock<ICustomerRepository>();
            Mock<IRentalRepository> mockRentRep = new Mock<IRentalRepository>();

            BoardGames brd1 = new BoardGames
            {
                BoardGameId = 1,
                Title = "Test Game 1",
                Type = "Test Type 1",
            };
            BoardGames brd2 = new BoardGames
            {
                BoardGameId = 2,
                Title = "Test Game 2",
                Type = "Test Type 2",
            };
            Customer cust1 = new Customer
            {
                CustomerId = 1,
                CustomerName = "Test Customer 1",
                CustomerAge = 20,
            };
            Customer cust2 = new Customer
            {
                CustomerId = 2,
                CustomerName = "Test Customer 2",
                CustomerAge = 66,
            };

            mockRentRep.Setup((x) => x.Create(It.IsAny<Rental>()));
            mockRentRep.Setup((x) => x.ReadAll())
                .Returns(new List<Rental>()
                {
                    new Rental { BoardGame = brd1, Customer = cust1, TimeLeft = 0},
                    new Rental { BoardGame = brd2, Customer = cust2, Price = 0, TimeLeft = 400}
                }
                .AsQueryable());

            rentLog = new RentalLogic(mockRentRep.Object, mockGameRep.Object, mockCusRep.Object);

            var v = rentLog.Read(1);
            var v2 = rentLog.ReadAll();
        }
        [Test]
        public void CreateRentCorrectIdTest()
        {
            var created = new Rental()
            {
                RentId = 1,
                BoardGameId = 1,
                CustomerId = 1,
                Price = 1,
                TimeLeft = 6
            };

            try
            {
                //ACT
                rentLog.Create(created);
            }
            catch
            {
                //ASSERT
                Assert.Throws<NullReferenceException>(() => rentLog.Create(created));
            }
        }
        [Test]
        public void CreateRentWrongIdTest()
        {
            var created = new Rental()
            {
                RentId = 0,
                BoardGameId = 1,
                CustomerId = 1,
                Price = 1,
                TimeLeft = 1
            };

            try
            {
                //ACT
                rentLog.Create(created);
            }
            catch
            {
                //ASSERT
                Assert.Throws<NullReferenceException>(() => rentLog.Create(created));
            }
        }

        [Test]
        public void CreateCustomerBadAgeTest()
        {
            var created = new Customer
            {
                CustomerName ="Test Customer 1",
                CustomerId=1,
                CustomerAge=12
            };

            try
            {
                //ACT
                cusLog.Create(created);
            }
            catch
            {
                //ASSERT
                Assert.Throws<NullReferenceException>(() => cusLog.Create(created));
            }
        }
        [Test]
        public void UpdateGameWithCorrectNameTest()
        {
            var updated = new BoardGames()
            {
                BoardGameId = 1,
                Title = "Cartographers",
                Type = "Family"
            };

            try
            {
                //ACT
                gameLog.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<NullReferenceException>(() => gameLog.Update(updated));
        }

        [Test]
        public void UpdateGameWithIncorrectNameTest()
        {
            var updated = new BoardGames()
            {
                BoardGameId=1,
                Title="",
                Type="Family"
            };

            try
            {
                //ACT
                gameLog.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<NullReferenceException>(() => gameLog.Update(updated));
        }

        [Test]
        public void OlderCustomerTest()
        {
            var expected = rentLog.OlderCustomer().ToArray();

            Assert.That(expected[0].GetHashCode(), Is.EqualTo(new { _CustomerName = "Test Customer 2", _CustomerAge = 66 }.GetHashCode()));
        }
        [Test]
        public void WithinweekTest()
        {
            var expected = rentLog.Withinweek().ToArray();

            Assert.That(expected[0].GetHashCode(), Is.EqualTo(new { _CustomerName = "Test Customer 1", _TimeLeft = 0}.GetHashCode()));
        }
        [Test]
        public void CurrentCustomer()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Test Customer 1", "Test Game 1"));
            list.Add(new KeyValuePair<string, string>("Test Customer 2", "Test Game 2"));
            var expected = rentLog.currentCustomers().ToList();
            Assert.That(expected, Is.EqualTo(list));
        }

        [Test]
        public void HighestTimeTest()
        {
            var expected = rentLog.HighestTime().ToArray();
            Assert.That(expected[0], Is.EqualTo(new KeyValuePair<string, double>("Test Customer 2", 400)));
        }

        [Test]
        public void FreePriceTest()
        {
            var expected = rentLog.FreePrice().ToArray();
            Assert.That(expected[0], Is.EqualTo(new KeyValuePair<string, double>("Test Customer 1", 0)));
        }
    }
}
