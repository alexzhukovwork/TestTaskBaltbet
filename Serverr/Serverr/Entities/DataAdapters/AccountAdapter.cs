using Server.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Serverr.Entities.DataAdapters
{
    public class AccountAdapter
    {
        private static readonly string CONNECTION_STRING = WebConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString;

        public static void WriteToDB(Account account)
        {
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Accounts(last_name, name, patronymicl, birthday, balance) " +
                    "VALUES(@last_name, @name, @patronymicl, @birthday, @balance); " +
                    "SELECT CAST(scope_identity() AS int)",
                    con
                );
                con.Open();

                cmd.Parameters.AddWithValue("@last_name", account.LastName);
                cmd.Parameters.AddWithValue("@name", account.FirstName);
                cmd.Parameters.AddWithValue("@patronymicl", account.Patronymicl);
                cmd.Parameters.AddWithValue("@birthday", account.Birthday);
                cmd.Parameters.AddWithValue("@balance", account.Balance);

                account.Id = (Int32)cmd.ExecuteScalar();
                con.Close();
            }
        }

        public static void UpdateDB(Account account)
        {
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Accounts " +
                    "SET last_name=@last_name," +
                    "name=@name," +
                    "patronymicl=@patronymicl," +
                    "birthday=@birthday," +
                    "balance=@balance " +
                    "WHERE id=@id",
                    con
                );

                con.Open();
                cmd.Parameters.AddWithValue("@last_name", account.LastName);
                cmd.Parameters.AddWithValue("@name", account.FirstName);
                cmd.Parameters.AddWithValue("@patronymicl", account.Patronymicl);
                cmd.Parameters.AddWithValue("@birthday", account.Birthday);
                cmd.Parameters.AddWithValue("@balance", account.Balance);
                cmd.Parameters.AddWithValue("@id", account.Id);
                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public static void ReadFromDB(Account account)
        {
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Accounts",
                    con
                );
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        account.Id = (int)reader.GetValue(0);
                        account.LastName = ((string)reader.GetValue(1)).Replace(" ", string.Empty);
                        account.FirstName = ((string)reader.GetValue(2)).Replace(" ", string.Empty);
                        account.Patronymicl = ((string)reader.GetValue(3)).Replace(" ", string.Empty);
                        account.Birthday = (DateTime)reader.GetValue(4);
                        account.Balance = Convert.ToSingle(reader.GetValue(5));
                    }
                }

                con.Close();
            }
        }
    }
}