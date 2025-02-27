using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.Client.Model
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public Guid CaseId { get; set; }

        #region Client Properties
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        //public Guid ReferalId { get; set; }
        //public SelectList Referals { get; set; }
        public string ReferalBy { get; set; }
        //public Guid AppearenceID { get; set; }
        public string Appearence { get; set; }
        public SelectList Appearences { get; set; } //it will be Title FIrst union title secound
                                                    //public Guid? OppositCounselId { get; set; }
                                                    //public SelectList OppositCounsels { get; set; }
                                                    //public ClientFeeViewModel FeeDetail { get; set; }

        #endregion
        public string ClientType { get; set; }
        public CorporateViewModel CorporateViewModel { get; set; } = null;

    }
}
