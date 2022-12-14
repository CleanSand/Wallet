using System;
using System.Collections.Generic;

namespace Wallet
{
    public partial class History
    {
        public int IdHistory { get; set; }
        public int IdListOfCoins { get; set; }
        public string Description { get; set; } = null!;

        public virtual ListOfCoin IdListOfCoinsNavigation { get; set; } = null!;
    }
}
