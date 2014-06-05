using System;
namespace MoneyExample
{
    public interface IBank
    {
        void AddRate(string from, string to, int rate);
        int GetRate(string from, string to);
        Money Reduce(ICurrencyExpression source, string to);
    }
}
