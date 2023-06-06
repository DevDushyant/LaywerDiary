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

    }

}