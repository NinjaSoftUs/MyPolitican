namespace NinjaSoft.Web.Models
{
    public class Summary: BaseModel
    {
        public string Cid { get; set; }
        public string FirstElected { get; set; }
        public string NextElection { get; set; }
        public double Total { get; set; }
        public double Spent { get; set; }
        public double CashOnHand { get; set; }
        public double Debt { get; set; }
        public string Origin { get; set; }
        public string Source { get; set; }
        public string Cycle { get; set; }
        public string SourceLastUpdated { get; set; }

    }
}
