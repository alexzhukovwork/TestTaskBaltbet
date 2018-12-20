using Server.Entities;
using Serverr.Entities.DataAdapters;
using System;
using System.Collections.Generic;

namespace Server
{

    public class Service1 : IService1
    {
        public Account UpdateAccount()
        {
            Account account = new Account();
            AccountAdapter.ReadFromDB(account);

            if (account.FirstName == null)
            {
                account.FirstName = "Alexey";
                account.LastName = "Zhukov";
                account.Patronymicl = "Igorevich";
                account.Balance = 10000;
                account.Birthday = new DateTime(1997, 6, 9);
                AccountAdapter.WriteToDB(account);
            }

            return account;
        }

        public bool CreateBet(Bet bet, Account account)
        {
            bool result = BetController.CreateBet(bet, account);

            if (result)
            {
                AccountAdapter.UpdateDB(account);
                BetAdapter.WriteToDB(bet, account);
                OutcomeAdapter.WriteToDB(bet.Outcomes, bet);
            }

            return result;
        }

        public bool WithdrawMoney(Account account, float value)
        {
            bool result = false;

            if (account.Balance >= value && value > 0)
            {
                result = true;
                account.WithdrawBalance(value);
                AccountAdapter.UpdateDB(account);
            }

            return result;
        }

        public bool DepositMoney(Account account, float value)
        {
            bool result = false;

            if (value > 0)
            {
                result = true;
                account.DepositeBalance(value);
                AccountAdapter.UpdateDB(account);
            }

            return result;
        }

        public Bet GetBetById(int id)
        {
            Bet bet = new Bet();
            BetAdapter.ReadFromDBById(bet, id.ToString());
            bet.Outcomes = OutcomeAdapter.ReadFromDB(bet);

            if (bet.Outcomes.Count == 0)
                bet = null;

            return bet;
        }

        public List<Bet> GetBetsByAccount(int id)
        {
            List<Bet> bets = new List<Bet>();
            BetAdapter.ReadListFromDBByField(bets, "account_id", id.ToString());

            for (int i = 0; i < bets.Count; i++) {
                bets[i].Outcomes = OutcomeAdapter.ReadFromDB(bets[i]);
            }

            return bets;
        }
    }
}
