namespace NinjaSoft.Web.Models
{
    public class Contributor : BaseModel
    {
   
        public string Cid { get; set; }
        public string OrgName { get; set; }
        public double Total { get; set; }
        public double Pacs { get; set; }
        public double Indivs { get; set; }
        public string LogoUrl { get; set; }
        public string Cycle { get; set; }
        public string Origin { get; set; }
        public string Source { get; set; }
        public string Notice { get; set; }
    }
}