using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Server.Entities
{
    [DataContract]
    public class Outcome
    {
        [DataMember]
        private int id;
        [DataMember]
        private int betId;
        [DataMember]
        private bool result;
        [DataMember]
        private float coefficient;

        [DataMember]
        public float Coefficient { get => coefficient; set => coefficient = value; }
        [DataMember]
        public bool Result { get => result; set => result = value; }
        [DataMember]
        public int BetId { get => betId; set => betId = value; }
        [DataMember]
        public int Id { get => id; set => id = value; }

        public Outcome(int id, bool result, float coefficient)
        {
            this.id = id;
            this.result = result;
            this.coefficient = coefficient;
        }

        public Outcome()
        {

        }
    }
}