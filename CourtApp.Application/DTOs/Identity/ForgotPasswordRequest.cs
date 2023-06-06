using System.ComponentModel.DataAnnotations;

namespace CourtApp.Application.DTOs.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}