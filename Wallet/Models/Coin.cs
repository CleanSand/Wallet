using System;
using System.Collections.Generic;

namespace Wallet
{
    public partial class Coin
    {
        public Coin()
        {
            ListOfCoins = new HashSet<ListOfCoin>();
        }

        public int IdCoins { get; set; }
        public decimal NumberOfCoins { get; set; }
        public int IdCurrency { get; set; }

        public virtual Currency IdCurrencyNavigation { get; set; } = null!;
        public virtual ICollection<ListOfCoin> ListOfCoins { get; set; }
    }
}
