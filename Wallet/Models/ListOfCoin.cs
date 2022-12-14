using System;
using System.Collections.Generic;

namespace Wallet
{
    public partial class ListOfCoin
    {
        public ListOfCoin()
        {
            Histories = new HashSet<History>();
            Payments = new HashSet<Payment>();
        }

        public int IdListOfCoins { get; set; }
        public int IdUser { get; set; }
        public int IdCoins { get; set; }

        public virtual Coin IdCoinsNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
