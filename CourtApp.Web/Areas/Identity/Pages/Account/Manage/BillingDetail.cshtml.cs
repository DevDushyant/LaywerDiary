using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CourtApp.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class BillingDetailModel : PageModel
    {

        private readonly ILogger<BillingDetailModel> _logger;

        public BillingDetailModel(ILogger<BillingDetailModel> _logger)
        {
            this._logger = _logger;
        }


        [BindProperty]
        public BillingDetailInputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class BillingDetailInputModel
        {
            [Display(Name = "Bank Name")]
            public string BankName { get; set; }

            [Display(Name = "Account Number")]
            public string AccountNo { get; set; }

            [Display(Name = "Branch Name")]
            public string Branch { get; set; }

            [Display(Name = "IFSC Code")]
            public string IfscCode { get; set; }

            [Display(Name = "Pan Number")]
            public string PanNumber { get; set; }

            [Display(Name = "GST No")]
            public string GstNo { get; set; }
        }
        public void OnGet()
        {
        }
    }
}
