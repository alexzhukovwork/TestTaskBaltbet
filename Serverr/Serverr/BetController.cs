using Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    class BetController
    {
        public static bool CreateBet(Bet bet, Account account)
        {
            bool result = false;

            if (account.Balance >= bet.Summary && bet.Summary > 0 && bet.Coefficient > 1)
            {
                result = true;
                bet.AccountId = account.Id;
                bet.DateTime = DateTime.Now;
                account.WithdrawBalance(bet.Summary);
                account.DepositeBalance(bet.SummaryWin);
            }

            return result;
        }
    }
}