using System;
using System.Collections.Generic;

namespace Wallet
{
    public partial class User
    {
        public User()
        {
            ListOfCoins = new HashSet<ListOfCoin>();
        }

        public int IdUser { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int WalletNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<ListOfCoin> ListOfCoins { get; set; }
    }
}
