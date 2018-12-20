using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace Server.Entities
{
    [DataContract]
    [KnownType(typeof(List<Bet>))]
    public class Bet
    {
        [DataMember]
        private int id;
        [DataMember]
        private int accountId;
        [DataMember]
        private DateTime dateTime;
        [DataMember]
        private float summary;
        [DataMember]
        private List<Outcome> outcomes;
        [DataMember]
        private bool result;
        [DataMember]
        private float coefficient;
        [DataMember]
        private float summaryWin;

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public int AccountId { get => accountId; set => accountId = value; }
        [DataMember]
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        [DataMember]
        public float Summary { get => summary; set => summary = value; }
        [DataMember]
        public List<Outcome> Outcomes { get => (outcomes == null ? new List<Outcome>() : outcomes); }

        public Bet(float summary)
        {
            this.summary = summary;
            outcomes = new List<Outcome>();
        }

        public Bet()
        {
            outcomes = new List<Outcome>();
        }

        [OperationContract]
        public void AddOutcome(Outcome outcome)
        {
            outcomes.Add(outcome);
        }

        [DataMember]
        public bool Result
        {
            get
            {
                result = true;

                if (outcomes != null)
                {
                    foreach (Outcome outcome in outcomes)
                    {
                        if (outcome.Result == false)
                            result = false;
                    }
                }


                return result;
            }

            set
            {
                result = value;
            }
        }

        [DataMember]
        public float Coefficient
        {
            get
            {
                coefficient = 1;

                if (outcomes != null)
                {
                    foreach (Outcome outcome in outcomes)
                    {
                        coefficient *= outcome.Coefficient;
                    }
                }
                return coefficient;
            }

            set
            {
                coefficient = value;
            }
        }

        public override string ToString()
        {
            string str = string.Empty;

            str += "Id: " + Id + "\n";
            str += "Дата ставки: " + DateTime + "\n";
            str += "Сумма: " + Summary + "\n";
            str += "Коэффициент: " + Coefficient + "\n";
            str += "Результат: " + Result + "\n";
            str += "Выигрыш: " + SummaryWin + "\n";

            if (outcomes.Count == 1)
            {
                str += "Исход: " + "\n"; 
            }
            else
            {
                str += "Исходы: " + "\n";
            }


            foreach (Outcome o in outcomes)
            {
                str += o.ToString();
            }

            return str;
        }

        [DataMember]
        public float SummaryWin { get => summary * Coefficient * (Result ? 1 : 0); set => summaryWin = value; }
    }
}