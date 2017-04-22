namespace NinjaSoft.Web.Models
{
    public class Sector : BaseModel
    {
        
        public string Cid { get; set; }
        public string SectorName { get; set; }
        public string SectorId { get; set; }
        public double Indivs { get; set; }
        public double Pacs { get; set; }
        public double Total { get; set; }


    }
}
