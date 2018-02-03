using System;
using System.Collections.Generic;

namespace XtendChallenge.Models
{
    public class Account
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public int AccountNumber { get; set; }
        public Decimal Balance { get; set; }
        public Facility Facility { get; set; }
        public DateTime AdminDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public Patient Patient { get; set; }
        public List<Insurance> Insurance { get; set; }
    }
}
