using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyExample
{
    public interface ICurrencyExpression
    {
        Money Reduce(IBank bank, string to);

        ICurrencyExpression Plus(ICurrencyExpression sixFranc);

        ICurrencyExpression Times(int multiplier);
    }
}
