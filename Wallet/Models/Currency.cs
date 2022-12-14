using System;
using System.Collections.Generic;

namespace Wallet
{
    public partial class Currency
    {
        public Currency()
        {
            Coins = new HashSet<Coin>();
            ExchangeIdGetCurrencyNavigations = new HashSet<Exchange>();
            ExchangeIdGiveCurrencyNavigations = new HashSet<Exchange>();
        }

        public int IdCurrency { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<Coin> Coins { get; set; }
        public virtual ICollection<Exchange> ExchangeIdGetCurrencyNavigations { get; set; }
        public virtual ICollection<Exchange> ExchangeIdGiveCurrencyNavigations { get; set; }
    }
}
