using Server.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Server
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
        Bet GetBetById(int id);

        [OperationContract]
        List<Bet> GetBetsByAccount(int id);

        [OperationContract]
        Account UpdateAccount();
    }
}
