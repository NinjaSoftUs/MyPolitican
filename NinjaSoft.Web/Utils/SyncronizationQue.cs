using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System.Web;
using NinjaSoft.Web.Models;

namespace NinjaSoft.Web.Utils
{
    public static class SyncronizationQue
    {
        public static ILogger Log { get; set; }
        private static string _baseDir;

        private static Dictionary<DateTime, object> CandCntribeQue { get; set; } = new Dictionary<DateTime, object>();
        private static Dictionary<DateTime, object> CandIndByIndQue { get; set; } = new Dictionary<DateTime, object>();
        private static Dictionary<DateTime, object> CandIndustryQue { get; set; } = new Dictionary<DateTime, object>();
        private static Dictionary<DateTime, object> CandSectorQue { get; set; } = new Dictionary<DateTime, object>();
        internal static Dictionary<DateTime, object> CandSummaryQue { get; set; } = new Dictionary<DateTime, object>();
        private static Dictionary<DateTime, object> CongCmteIndusQue { get; set; } = new Dictionary<DateTime, object>();
        public static Dictionary<DateTime, object> LegislatorsQue { get; set; } = new Dictionary<DateTime, object>();
        private static Dictionary<DateTime, object> GetOrgsQue { get; set; } = new Dictionary<DateTime, object>();
        private static Dictionary<DateTime, object> IndependentExpendQue { get; set; } = new Dictionary<DateTime, object>();
        private static Dictionary<DateTime, object> MemPfDprofileQue { get; set; } = new Dictionary<DateTime, object>();
        private static Dictionary<DateTime, object> OrgSummaryQue { get; set; } = new Dictionary<DateTime, object>();
        public static bool ContribuitorsQueIsWaiting { get; set; }
        public static bool LegislatorsQueIsWaiting { get; set; }


        static SyncronizationQue()
        {
            _baseDir =$"{AppDomain.CurrentDomain.BaseDirectory}\\App_Data\\ques";

            LegislatorsQue = loadQue<Politician>($"{_baseDir}\\LegislatorsQue.json");
            CandCntribeQue = loadQue<Contributor>($"{_baseDir}\\CandCntribeQue.json");

            //CandIndByIndQue = loadQue($"{_baseDir}\\CandIndByIndQue.json");
            //CandIndustryQue = loadQue($"{_baseDir}\\CandIndustryQue.json");
            //CandSectorQue = loadQue($"{_baseDir}\\CandSectorQue.json");
            //CandSummaryQue = loadQue($"{_baseDir}\\CandSummaryQue.json");
            //CongCmteIndusQue = loadQue($"{_baseDir}\\CongCmteIndusQue.json");

            //GetOrgsQue = loadQue($"{_baseDir}\\GetOrgsQue.json");
            //IndependentExpendQue = loadQue($"{_baseDir}\\IndependentExpendQue.json");
            //MemPfDprofileQue = loadQue($"{_baseDir}\\MemPfDprofileQue.json");
            //OrgSummaryQue = loadQue($"{_baseDir}\\OrgSummaryQue.json");
        }

        #region CandCntribeQue

        public static IEnumerable<string> GetCandCntribeQue()
        {
            var list = new List<string>();
            list.AddRange(CandCntribeQue.Values.Select(x=>x.ToString()));
            return list;
        }

        public static void AddToCandCntribeQue(object obj)
        {
            if (!CandCntribeQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                CandCntribeQue.Add(DateTime.UtcNow, obj);
                SaveQue(CandCntribeQue, $"{_baseDir}\\CandCntribeQue.json");
            }
        }

        public static void DeleteFromCandCntribeQue(object obj)
        {
            if (!CandCntribeQue.ContainsValue(obj))
            {
                var target = (from items in CandCntribeQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    CandCntribeQue.Remove(target.Key);
                    SaveQue(CandCntribeQue, $"{_baseDir}\\CandCntribeQue.json");
                }
            }
        }

        #endregion CandCntribeQue

        #region CandIndByIndQue

        public static void AddToCandIndByIndQue(object obj)
        {
            if (!CandIndByIndQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                CandIndByIndQue.Add(DateTime.UtcNow, obj);
                SaveQue(CandIndByIndQue, $"{_baseDir}\\CandIndByIndQue.json");
            }
        }

        public static void DeleteFromCandIndByIndQue(object obj)
        {
            if (!CandIndByIndQue.ContainsValue(obj))
            {
                var target = (from items in CandIndByIndQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    CandIndByIndQue.Remove(target.Key);
                    SaveQue(CandIndByIndQue, $"{_baseDir}\\CandIndByIndQue.json");
                }
            }
        }

        #endregion CandIndByIndQue

        #region CandIndustryQue

        public static void AddToCandIndustryQue(object obj)
        {
            if (!CandIndustryQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                CandIndustryQue.Add(DateTime.UtcNow, obj);
                SaveQue(CandIndustryQue, $"{_baseDir}\\CandIndustryQue.json");
            }
        }

        public static void DeleteCandIndustryQue(object obj)
        {
            if (!CandIndustryQue.ContainsValue(obj))
            {
                var target = (from items in CandIndustryQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    CandIndustryQue.Remove(target.Key);
                    SaveQue(CandIndustryQue, $"{_baseDir}\\CandIndustryQue.json");
                }
            }
        }

        #endregion CandIndustryQue

        #region CandSectorQue

        public static void AddToCandSectorQue(object obj)
        {
            if (!CandCntribeQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                CandCntribeQue.Add(DateTime.UtcNow, obj);
                SaveQue(CandCntribeQue, $"{_baseDir}\\CandCntribeQue.json");
            }
        }

        public static void DeleteCandSectorQue(object obj)
        {
            if (!CandCntribeQue.ContainsValue(obj))
            {
                var target = (from items in CandCntribeQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    CandCntribeQue.Remove(target.Key);
                    SaveQue(CandCntribeQue, $"{_baseDir}\\CandCntribeQue.json");
                }
            }
        }

        #endregion CandSectorQue

        #region CandSummaryQue

        public static void AddToCandSummaryQue(object obj)
        {
            if (!CandSummaryQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                CandSummaryQue.Add(DateTime.UtcNow, obj);
                SaveQue(CandSummaryQue, $"{_baseDir}\\CandSummaryQue.json");
            }
        }

        public static void DeleteFromCandSummaryQue(object obj)
        {
            if (!CandSummaryQue.ContainsValue(obj))
            {
                var target = (from items in CandSummaryQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    CandSummaryQue.Remove(target.Key);
                    SaveQue(CandSummaryQue, $"{_baseDir}\\CandSummaryQue.json");
                }
            }
        }

        #endregion CandSummaryQue

        #region CongCmteIndusQue

        public static void AddToCongCmteIndusQue(object obj)
        {
            if (!CongCmteIndusQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                CongCmteIndusQue.Add(DateTime.UtcNow, obj);
                SaveQue(CongCmteIndusQue, $"{_baseDir}\\CongCmteIndusQue.json");
            }
        }

        public static void DeleteCongCmteIndusQue(object obj)
        {
            if (!CongCmteIndusQue.ContainsValue(obj))
            {
                var target = (from items in CongCmteIndusQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    CongCmteIndusQue.Remove(target.Key);
                    SaveQue(CongCmteIndusQue, $"{_baseDir}\\CongCmteIndusQue.json");
                }
            }
        }

        #endregion CongCmteIndusQue

        #region LegislatorsQue

        public static void AddToLegislatorsQue(Politician politician)
        {
            if (LegislatorsQue == null)
            {
                LegislatorsQue = loadQue<Politician>($"{_baseDir}\\LegislatorsQue.json");
            }

            if (!LegislatorsQue.ContainsValue(politician))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                LegislatorsQue.Add(DateTime.UtcNow, politician);
                SaveQue(LegislatorsQue, $"{_baseDir}\\LegislatorsQue.json");
            }
        }

        public static Task<bool> LoadLegislatorsAsyc()
        {
            var task = new Task<bool>(r =>
            {
                LegislatorsQue =  loadQue<Politician>($"{_baseDir}\\LegislatorsQue.json");
                return true;
            },null);

            task.Start();

            return task;
        }

        public static Politician GetNextLegislator()
        {
            var target = (from items in LegislatorsQue
                          orderby items.Key descending
                          select items).FirstOrDefault();

            return target.Value as Politician;
        }

        //public  void AddToLegislatorsQue(object obj)
        //{
        //    if (!LegislatorsQue.ContainsValue(obj))
        //    {
        //        Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
        //        LegislatorsQue.Add(DateTime.UtcNow, obj);
        //        SaveQue(LegislatorsQue, $"{_baseDir}\\LegislatorsQue.json");
        //    }
        //}

        public static void RemoveLegislatorFromQue(Politician policician)
        {
            var target = (from items in LegislatorsQue
                          where items.Value == policician
                          select items).FirstOrDefault();

            if (target.Value != null)
            {
                LegislatorsQue.Remove(target.Key);
                SaveQue(LegislatorsQue, $"{_baseDir}\\LegislatorsQue.json");
            }
        }

        public static void AddToContriuitorsQue(List<Contributor> contributorslist)
        {
            foreach (var contributor in contributorslist)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1)); //make sure date incroments so we have a uniqe key
                CandCntribeQue.Add(DateTime.UtcNow, contributor);
            }
            SaveQue(CandCntribeQue, $"{_baseDir}\\CandCntribeQue.json");
        }

        public static Contributor GetNextContributor()
        {
            var target = CandCntribeQue.Select(items => items).FirstOrDefault();
            return (Contributor) target.Value;
        }

        public static void RemoveContributorFromQue(Contributor contributor)
        {
            var target = CandCntribeQue.Select(items => items).FirstOrDefault();
            if (target.Value != null)
            {
                CandCntribeQue.Remove(target.Key);
                SaveQue(CandCntribeQue, $"{_baseDir}\\CandCntribeQue.json");
            }
        }

        public static void DeleteGetLegislatorsQue(object obj)
        {
            if (!LegislatorsQue.ContainsValue(obj))
            {
                var target = (from items in LegislatorsQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    LegislatorsQue.Remove(target.Key);
                    SaveQue(LegislatorsQue, $"{_baseDir}\\LegislatorsQue.json");
                }
            }
        }

        #endregion LegislatorsQue

        #region GetOrgsQue

        public static void AddToGetOrgsQue(object obj)
        {
            if (!GetOrgsQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                GetOrgsQue.Add(DateTime.UtcNow, obj);
                SaveQue(GetOrgsQue, $"{_baseDir}\\GetOrgsQue.json");
            }
        }

        public static void DeleteGetOrgsQue(object obj)
        {
            if (!GetOrgsQue.ContainsValue(obj))
            {
                var target = (from items in GetOrgsQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    GetOrgsQue.Remove(target.Key);
                    SaveQue(GetOrgsQue, $"{_baseDir}\\GetOrgsQue.json");
                }
            }
        }

        #endregion GetOrgsQue

        #region IndependentExpendQue

        public static void AddToIndependentExpendQue(object obj)
        {
            if (!IndependentExpendQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                IndependentExpendQue.Add(DateTime.UtcNow, obj);
                SaveQue(IndependentExpendQue, $"{_baseDir}\\IndependentExpendQue.json");
            }
        }

        public static void DeleteIndependentExpendQue(object obj)
        {
            if (!IndependentExpendQue.ContainsValue(obj))
            {
                var target = (from items in IndependentExpendQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    IndependentExpendQue.Remove(target.Key);
                    SaveQue(IndependentExpendQue, $"{_baseDir}\\IndependentExpendQue.json");
                }
            }
        }

        #endregion IndependentExpendQue

        #region MemPfDprofileQue

        public static void AddToMemPfDprofileQue(object obj)
        {
            if (!MemPfDprofileQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                MemPfDprofileQue.Add(DateTime.UtcNow, obj);
                SaveQue(MemPfDprofileQue, $"{_baseDir}\\MemPfDprofileQue.json");
            }
        }

        public static void DeleteMemPfDprofileQue(object obj)
        {
            if (!MemPfDprofileQue.ContainsValue(obj))
            {
                var target = (from items in MemPfDprofileQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    MemPfDprofileQue.Remove(target.Key);
                    SaveQue(MemPfDprofileQue, $"{_baseDir}\\MemPfDprofileQue.json");
                }
            }
        }

        #endregion MemPfDprofileQue

        #region OrgSummaryQue

        public static void AddToOrgSummaryQue(object obj)
        {
            if (!OrgSummaryQue.ContainsValue(obj))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//pausing for a split secon to get a uniqe time stamp
                OrgSummaryQue.Add(DateTime.UtcNow, obj);
                SaveQue(OrgSummaryQue, $"{_baseDir}\\OrgSummaryQue.json");
            }
        }

        public static void DeleteOrgSummaryQue(object obj)
        {
            if (!OrgSummaryQue.ContainsValue(obj))
            {
                var target = (from items in OrgSummaryQue
                              where items.Value == obj
                              select items).FirstOrDefault();

                if (target.Value != null)
                {
                    OrgSummaryQue.Remove(target.Key);
                    SaveQue(OrgSummaryQue, $"{_baseDir}\\OrgSummaryQue.json");
                }
            }
        }

        #endregion OrgSummaryQue

        #region privateMethods

        private static Dictionary<DateTime, object> loadQue<T>(string jsonFlie)
        {
            try
            {
                var fileInfo = new FileInfo(jsonFlie);
                if (fileInfo.Exists)
                {
                    var json = File.ReadAllText(fileInfo.FullName);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        JObject jObject = (JObject)JsonConvert.DeserializeObject(json);

                        var dic = new Dictionary<DateTime, object>();
                        foreach (var keyValuePair in jObject)
                        {
                            var item = JsonConvert.DeserializeObject<T>(keyValuePair.Value.ToString());
                            dic.Add(Convert.ToDateTime(keyValuePair.Key), item);
                        }

                        return dic;
                    }
                }
            }
            catch (Exception e)
            {
              // Log.LogError("someting went bad loading que from json file. The que is deleted!");
              // Log.LogError(e.Message);
              // Log.LogError(e.StackTrace);

                var fileInfo = new FileInfo(jsonFlie);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }

            return new Dictionary<DateTime, object>();
        }

        private static void SaveQue(object obj, string jsonFile)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var fileInfo = new FileInfo(jsonFile);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
                fileInfo = null;
                File.WriteAllText(jsonFile, json);
            }
            catch (Exception e)
            {
              //  Log.LogCritical(e.Message);
              //  Log.LogCritical(e.StackTrace);
            }
        }

        #endregion privateMethods
    }
}