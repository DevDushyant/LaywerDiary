using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : BaseController<HomeController>
    {
        public IActionResult Index()
        {
            _notify.Information("Hi There!");
            var model = new DashboardViewModel
            {
                TotalCases = 120,
                DisposedCases = 45,
                PendingCases = 60,
                AssignedCases = 15,
                StatusSummaries = new List<CaseStatusSummary>
                {
                    new CaseStatusSummary { Status = "Pending", Count = 60 },
                    new CaseStatusSummary { Status = "Disposed", Count = 45 },
                    new CaseStatusSummary { Status = "Assigned", Count = 15 }
                },
                UpcomingHearings = new List<NextHearingItem>
                {
                    new NextHearingItem
                    {
                        CaseId = 101,
                        CaseTitle = "State vs Sharma",
                        HearingDate = DateTime.Today.AddDays(1),
                        CourtName = "District Court Bhopal",
                        OpponentName = "Rakesh Sharma"
                    },
                    new NextHearingItem
                    {
                        CaseId = 102,
                        CaseTitle = "Rajeev vs Govt",
                        HearingDate = DateTime.Today.AddDays(3),
                        CourtName = "High Court Indore",
                        OpponentName = "Govt. Advocate"
                    }
                },
                MonthlyCaseStatuses = new List<MonthlyCaseStatus>
                {
                        new MonthlyCaseStatus { Month = "Jan", Filed = 20, Disposed = 10 },
                        new MonthlyCaseStatus { Month = "Feb", Filed = 30, Disposed = 25 },
                        new MonthlyCaseStatus { Month = "Mar", Filed = 25, Disposed = 20 },
                        new MonthlyCaseStatus { Month = "Apr", Filed = 15, Disposed = 10 },
                        new MonthlyCaseStatus { Month = "May", Filed = 40, Disposed = 35 },
                        new MonthlyCaseStatus { Month = "Jun", Filed = 10, Disposed = 5 }
                }
            };
            return View(model);
        }
    }
}