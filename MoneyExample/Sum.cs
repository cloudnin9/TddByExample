using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyExample
{
    public class Sum : ICurrencyExpression
    {
        private Sum sum;

        public Sum(ICurrencyExpression augend, ICurrencyExpression addend)
        {            
            this.Augend = augend;
            this.Addend = addend;
        }
        public ICurrencyExpression Augend { get; private set; }

        public ICurrencyExpression Addend { get; private set; }

        public Money Reduce(IBank bank, string to)
        {
            return new Money(Augend.Reduce(bank, to).amount + Addend.Reduce(bank, to).amount, to);
        }

        public ICurrencyExpression Plus(ICurrencyExpression addend)
        {
            return new Sum(this, addend);
        }

        public ICurrencyExpression Times(int multiplier)
        {
            return new Sum(Augend.Times(multiplier), Addend.Times(multiplier));
        }
    }
}
