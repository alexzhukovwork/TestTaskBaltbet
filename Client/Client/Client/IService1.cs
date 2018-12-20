using Server.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace Client
{
    [ServiceContract]
    interface IService1
    {
        [OperationContract]
        bool WithdrawMoney(Account account, float value);

        [OperationContract]
        bool DepositMoney(Account account, float value);

        [OperationContract]
        bool CreateBet(Bet bet, Account account);

        [OperationContract]
        Account UpdateAccount();

        [OperationContract]
        Bet GetBetById(int id);

        [OperationContract]
        List<Bet> GetBetsByAccount(int id);
    }
}
