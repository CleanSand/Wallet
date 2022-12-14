using System;
using System.Collections.Generic;

namespace Wallet
{
    public partial class Payment
    {
        public int IdPayment { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime TimePayment { get; set; }
        public int? IdListOfCoins { get; set; }

        public virtual ListOfCoin? IdListOfCoinsNavigation { get; set; }
    }
}
