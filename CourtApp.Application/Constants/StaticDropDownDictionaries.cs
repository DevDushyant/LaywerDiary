using System;
using System.Collections.Generic;

namespace CourtApp.Application.Constants
{
    public class StaticDropDownDictionaries
    {
        public static Dictionary<string, string> ModuleDictionary()
        {
            var _Module = new Dictionary<string, string>
            {
                { "LWD", "Lawyer Diary" },
                { "HNT", "Head Note" }
            };
            return _Module;
        }
        public static Dictionary<string, string> CaseStatus()
        {
            var _CaseStatus = new Dictionary<string, string>
            {
                { "ADMU", "Admitted(Unready)" },
                { "PADM", "Pre-Admission" },
                { "ADMI", "Admitted" },
                { "PEDG", "Pedning" },
                { "DISP", "Dispose" }
            };
            return _CaseStatus;
        }

        public static Dictionary<string, string> MatterStatus()
        {
            var _MatterStatus = new Dictionary<string, string>
            {
                { "OPN", "Open" },
                { "CLS", "Close" },
                { "WIP", "Work in progress" }

            };
            return _MatterStatus;
        }
        public static Dictionary<string, string> CaseSearchBy()
        {
            var _CaseSearchBy = new Dictionary<string, string>
            {
                { "BCSE", "By Case" },
                { "BPTY", "By Party" }
            };
            return _CaseSearchBy;
        }
        public static Dictionary<string, string> FeeType()
        {
            var _CaseSearchBy = new Dictionary<string, string>
            {
                { "FULL", "FULL" },
                { "HALF", "HALF" }
            };
            return _CaseSearchBy;
        }

        public static Dictionary<int, string> FirstTitle()
        {
            var _TitleFirst = new Dictionary<int, string>
            {
                { 1, "Petitioner" },
                { 2, "Objector/Petitioner" },
                { 3, "Complainant" },
                { 4, "Accused" },
                { 5, "Applicant" }
            };
            return _TitleFirst;
        }
        public static Dictionary<int, string> SecoundTitle()
        {
            var _Title2nd = new Dictionary<int, string>
            {
                { 1, "Defendants" },
                { 2, "Accused" },
                { 3, "Non-Applicant" },
                { 4, "Respondent/Claimaint" },
                { 5, "Judgement Debtor" }

            };
            return _Title2nd;
        }
        public static Dictionary<int, int> Year()
        {
            var year = DateTime.Now.Year;
            var _year = new Dictionary<int, int>();
            for (int y = year; y >= 1800; y--)
            {
                _year.Add(y, y);
            };
            return _year;
        }

        public static Dictionary<string, string> VolumeDictionary()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "volume1", "Volume I" },
                { "volume2", "Volume II" },
                { "volume3", "Volume III" },
                { "volume4", "Volume IV" },
                { "volume5", "Volume V" },
                { "volume6", "Volume VI" },
                { "volume7", "Volume VII" },
                { "volume8", "Volume VIII" },
                { "volume9", "Volume IX" },
                { "volume10", "Volume X" },
                { "Rajasthan", "Rajasthan" },
                { "Center", "Centeral" }
            };
            return _volumeDictionary;
        }      

        public static Dictionary<string, string> AssentByDictionary()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "PREDENT", "Presedent" },
                { "GOVNER", "Governer" },
                { "HHRAJP", "H.H. RajPramukh" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<string, string> GazetteNature()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "ORDINA", "Ordinary" },
                { "EXORDI", "Extra Ordinary" },
                 { "WEEKLY", "Weekly" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<string, string> Rule_GSR_SO()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "GSR", "GSR" },
                { "SO", "SO" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<string, string> ManageType()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "NEW", "New" },
                { "AMD", "Amended" },
                { "RPD", "Repealed" }
            };
            return _volumeDictionary;
        }       

        public static Dictionary<string, string> ComeInforce()
        {
            var _comeinforceDictionary = new Dictionary<string, string>
            {
                { "ATONCE", "AtOnce" },
                { "NOSPVN", "No specific provision" },
                 { "ASTBNO", "As to be notified" } ,
                 { "WEFPIG", "W.E.F. published in gazette" },
                 { "WEFIEF", "W.E.F. Immediate effect" },
                { "ASMEND", "As Mentioned" }
            };
            return _comeinforceDictionary;
        }

        public static Dictionary<string, string> RuleSearchingKind()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "NEW", "New" },
                { "AMD", "Amended" },
                { "RPD", "Repealed" },
                { "ALL", "All" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<string, string> DataToolType()
        {
            var _RDictionary = new Dictionary<string, string>
            {
                { "EGZT", "E-Gazzet" },
                { "RGZT", "Rajasthan" }
            };
            return _RDictionary;
        }

        public static Dictionary<int, string> SubjectType()
        {
            var _RDictionary = new Dictionary<int, string>
            {
                { 1, "Lawyer Diary" },
                { 2, "Advocate" }
            };
            return _RDictionary;
        }

        public static Dictionary<int, string> GazzetType()
        {
            var _RDictionary = new Dictionary<int, string>
            {
                { 1, "Gazzet of India" },
                { 2, "Gazzet of Rajasthan" },
                { 3, "Gazzet of Uttar Pradesh" }
            };
            return _RDictionary;
        }



    }

}