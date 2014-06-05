using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyExample
{
    public class Bank : MoneyExample.IBank
    {
        IDictionary<Tuple<string, string>, int> rates = new Dictionary<Tuple<string, string>, int>();

        public Money Reduce(ICurrencyExpression source, string to)
        {
            return source.Reduce(this, to);
        }

        public void AddRate(string from, string to, int rate)
        {
            var rateKey = new Tuple<string, string>(from, to);
            var kvp = new KeyValuePair<Tuple<string, string>, int>(rateKey, rate);
            if (!rates.Contains(kvp))
            {                
                rates.Add(kvp);
            }            
        }

        public int GetRate(string from, string to)
        {
            if (from == to) return 1;
            var rateKey = new Tuple<string, string>(from, to);            
            if (rates.ContainsKey(rateKey))
            {
                var rate = 0;
                rates.TryGetValue(rateKey, out rate);
                return rate;
            }
            return 0;
        }
    }
}
