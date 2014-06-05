using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyExample
{
    public class Money : ICurrencyExpression
    {

        internal string Currency { get; private set; }
        internal int amount;

        public Money(int amount, string currency)
        {
            this.amount = amount;
            this.Currency = currency;
        }
        
        public static Money Dollar(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money Franc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public override bool Equals(object obj)
        {
            return (obj as Money) != null &&
                Currency == ((Money)obj).Currency &&
                this.amount == ((Money)obj).amount;
        }
        public override string ToString()
        {
            return string.Format("{1} {0}", Currency, amount);
        }

        public override int GetHashCode()
        {
            int seed = 0x0B;
            return (seed * 7) + Currency.GetHashCode() + amount.GetHashCode();
        }

        public ICurrencyExpression Plus(ICurrencyExpression addend)
        {
            return new Sum (this,addend);
        }

        public Money Reduce(IBank bank, string to)
        {
            var rate = bank.GetRate(Currency, to);
            return new Money(amount / rate, to);
        }

        public ICurrencyExpression Times(int multiplier)
        {
            return new Money(amount * multiplier, Currency);
        }
    }
}
