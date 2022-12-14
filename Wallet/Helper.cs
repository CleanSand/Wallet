using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet
{
    public class Helper
    {
        private static WalletContext _context;
        public static User User { get; set; }
        public static WalletContext GetContext()
        {
            if (_context == null)
                _context = new WalletContext();
            return _context;
        }

    }
}
