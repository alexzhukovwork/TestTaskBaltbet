using Server.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Serverr.Entities.DataAdapters
{
    public class BetAdapter
    {
        private static readonly string CONNECTION_STRING = WebConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString;

        public static void WriteToDB(Bet bet, Account account)
        {
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Bets(date, account_id, summary) " +
                    "VALUES(@date, @account_id, @summary); " +
                    "SELECT CAST(scope_identity() AS int)",
                    con
                );
                con.Open();

                cmd.Parameters.AddWithValue("@date", bet.DateTime);
                cmd.Parameters.AddWithValue("@account_id", account.Id);
                cmd.Parameters.AddWithValue("@summary", bet.Summary);
                bet.Id = (Int32)cmd.ExecuteScalar();

                con.Close();
            }
        }

        public static void ReadListFromDBByField(List<Bet> bets, string field, string value)
        {
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Bets WHERE " + field + " = " + value,
                    con
                );
                con.Open();

                Bet bet;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bet = new Bet();

                        bet.Id = (int)reader.GetValue(0);
                        bet.DateTime = (DateTime)reader.GetValue(1);
                        bet.AccountId = (int)reader.GetValue(2);
                        bet.Summary = Convert.ToSingle(reader.GetValue(3));

                        bets.Add(bet);
                    }
                }

                con.Close();
            }
        }

        public static void ReadFromDBById(Bet bet, string id)
        {
            List<Bet> bets = new List<Bet>();

            ReadListFromDBByField(bets, "id", id);

            if (bets.Count > 0)
            {
                bet.Id = bets[0].Id;
                bet.DateTime = bets[0].DateTime;
                bet.AccountId = bets[0].AccountId;
                bet.Summary = bets[0].Summary;
            }
        }
    }
}