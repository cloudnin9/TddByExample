using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoneyExample.UnitTests
{
    [TestClass]
    public class Money_Test_Suite
    {
        [TestMethod]
        public void Test_Multiplecation_Of_Money()
        {
            Money five = Money.Dollar(5);

            Assert.AreEqual(Money.Dollar(10), five.Times(2));
            Assert.AreEqual(Money.Dollar(15), five.Times(3));                  
        }

        [TestMethod]
        public void Test_Equality_Of_Money()
        {
            Assert.AreEqual(Money.Dollar(15), Money.Dollar(15));            
            Assert.AreNotEqual(Money.Dollar(15), Money.Dollar(14));
            Assert.AreNotEqual(Money.Dollar(15), Money.Franc(15));            
        }

        [TestMethod]
        public void Test_Currencies() 
        {            
            Assert.AreEqual(new Money(15, "CHF"), Money.Franc(15));
            Assert.AreEqual(new Money(15, "USD"), Money.Dollar(15));
        }

        [TestMethod]
        public void Test_Addition_Of_Money()
        {                       
            var bank = new Bank();

            ICurrencyExpression sum = Money.Dollar(15).Plus(Money.Dollar(10));
            var reduced = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(25), reduced);
        }

        [TestMethod]
        public void Test_Sum_Returns_Money()
        {
            var bank = new Bank();
            var five = Money.Dollar(5);
            Sum sum = (Sum)five.Plus(five);            

            Assert.AreEqual(five, sum.Augend);
            Assert.AreEqual(five, sum.Addend);
        }

        [TestMethod]
        public void Test_Bank_Reduces_Sum()
        {
            var bank = new Bank();
            var five = Money.Dollar(5);
            ICurrencyExpression sum = five.Plus(five);
            Money reduced = bank.Reduce(sum, "USD");

            Assert.AreEqual(Money.Dollar(10), reduced);
        }

        [TestMethod]
        public void Test_Reduce_Money()
        {
            Bank bank = new Bank();
            Money result = bank.Reduce(Money.Dollar(2), "USD");
            Assert.AreEqual(Money.Dollar(2), result);
        }

        [TestMethod]
        public void Test_Reduce_Money_Different_Currency() 
        {
            Money tenFranc = Money.Franc(10);
            Bank bank = new Bank();
            
            bank.AddRate("CHF", "USD", 2);

            Money result = bank.Reduce(tenFranc, "USD");

            Assert.AreEqual(Money.Dollar(5), result);            
        }

        [TestMethod]
        public void Test_Identity_Rate()
        {
            Assert.AreEqual(1, new Bank().GetRate("USD", "USD"));
        }

        [TestMethod]
        public void Test_Tuple() 
        {
            Tuple<string, string> pair1 = Tuple.Create<string, string>("USD", "CHF");
            Tuple<string, string> pair2 = Tuple.Create<string, string>("CHF", "USD");

            Assert.AreNotEqual(pair1,pair2);
        }

        [TestMethod]
        public void Test_Money_Add_Different_Currencies()
        {
            var bank = new Bank();

            ICurrencyExpression tenDollars = Money.Dollar(10);
            ICurrencyExpression sixFranc = Money.Franc(6);

            ICurrencyExpression sum = tenDollars.Plus(sixFranc);
            bank.AddRate("CHF", "USD", 2);
            var result = sum.Reduce(bank, "USD");
            Assert.AreEqual(Money.Dollar(13), result);
        }

        [TestMethod]
        public void Test_Sum_Plus_Money_Add_Different_Currencies()
        {
            //Arrange
            var bank = new Bank();
            bank.AddRate("CHF", "USD", 2);

            ICurrencyExpression tenDollars = Money.Dollar(10);
            ICurrencyExpression sixFranc = Money.Franc(6);
            ICurrencyExpression eightFranc = Money.Franc(8);

            //Act
            ICurrencyExpression sum = tenDollars.Plus(sixFranc);
            sum = sum.Plus(eightFranc);                       
            var result = sum.Reduce(bank, "USD");

            //Assert
            Assert.AreEqual(Money.Dollar(17), result);
        }

        [TestMethod]
        public void Test_Times_Money_Add_Different_Currencies() 
        {
            //Arrange
            var bank = new Bank();
            bank.AddRate("CHF", "USD", 2);

            ICurrencyExpression tenDollars = Money.Dollar(10);
            ICurrencyExpression sixFranc = Money.Franc(6);
            
            //Act
            ICurrencyExpression sumThenTimes = tenDollars.Plus(sixFranc).Times(2);
            var result = sumThenTimes.Reduce(bank, "USD");
            
            //Assert
            Assert.AreEqual(Money.Dollar(26), result);
        }

    }
}

