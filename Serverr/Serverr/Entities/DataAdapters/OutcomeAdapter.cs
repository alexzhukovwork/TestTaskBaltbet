using Server.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Serverr.Entities.DataAdapters
{
    public class OutcomeAdapter
    {
        private static readonly string CONNECTION_STRING = WebConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString;

        public static void WriteToDB(List<Outcome> outcomes, Bet bet)
        {
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                
                con.Open();

                foreach (Outcome outcome in outcomes)
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Outcomes(bet_id, result, coefficient) " +
                        "VALUES(@bet_id, @result, @coefficient)",
                        con
                    );
                    cmd.Parameters.AddWithValue("@bet_id", bet.Id);
                    cmd.Parameters.AddWithValue("@result", outcome.Result);
                    cmd.Parameters.AddWithValue("@coefficient", outcome.Coefficient);
                    cmd.ExecuteNonQuery();
                }
                
                con.Close();
            }
        }

        public static List<Outcome> ReadFromDB(Bet bet)
        {
            List<Outcome> outcomes = new List<Outcome>();

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Outcomes WHERE bet_id = " + bet.Id,
                    con
                );
                con.Open();

                Outcome outcome;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        outcome = new Outcome();

                        outcome.Id = (int)reader.GetValue(0);
                        outcome.BetId = (int)reader.GetValue(1);
                        outcome.Result = (bool)reader.GetValue(2);
                        outcome.Coefficient = Convert.ToSingle(reader.GetValue(3));

                        outcomes.Add(outcome);
                    }
                }

                con.Close();
            }

            return outcomes;
        }
    }
}