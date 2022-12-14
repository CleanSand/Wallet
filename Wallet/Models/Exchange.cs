using System;
using System.Collections.Generic;

namespace Wallet
{
    public partial class Exchange
    {
        public int IdExchange { get; set; }
        public decimal Size { get; set; }
        public int IdGiveCurrency { get; set; }
        public int IdGetCurrency { get; set; }

        public virtual Currency IdGetCurrencyNavigation { get; set; } = null!;
        public virtual Currency IdGiveCurrencyNavigation { get; set; } = null!;
    }
}
