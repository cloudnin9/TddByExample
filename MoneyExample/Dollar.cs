using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyExample
{
    public class Dollar : Money
    {

        public Dollar(int amount, string currency)
            : base(amount, currency)
        {            
        }

        public override Money Times(int p)
        {
            return new Dollar(amount * p, Currency);
        }
    }
}
