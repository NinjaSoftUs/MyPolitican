using System;
using System.Collections.Generic;

namespace NinjaSoft.Web.Models
{
    public class Politician : BaseModel
    {
    
        public string BioguideId { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Cid { get; set; }
        public string Comments { get; set; }
        public string CongressOffice { get; set; }
        public string ExitCode { get; set; }
        public string FacebookId { get; set; }
        public string Fax { get; set; }
        public string Feccandid { get; set; }
        public string FirstElected { get; set; }
        public string Firstlast { get; set; }
        public string Gender { get; set; }
        public string Lastname { get; set; }
        public string Office { get; set; }
        public string Party { get; set; }
        public string Phone { get; set; }
        public string TwitterId { get; set; }
        public string VotesmartId { get; set; }
        public string Website { get; set; }
        public string YoutubeUrl { get; set; }
        //public List<Contributor> Contributors { get; set; }
        //public List<Sector> Sectors { get; set; }
    }
}