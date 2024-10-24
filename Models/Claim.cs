using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CMC_SYSTEM.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }

        [Required(ErrorMessage = "Lecturer Name is required")]
        public string LecturerName { get; set; }

        [Required(ErrorMessage = "Hours Worked is required")]
        [Range(0, 100, ErrorMessage = "Hours worked must be between 0 and 100.")]
        public decimal HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly Rate is required")]
        [Range(0.01, 1000, ErrorMessage = "Hourly rate must be between 0.01 and 1000.")]
        public decimal HourlyRate { get; set; }

        // Condition: If HoursWorked exceeds 40, AdditionalNotes must be provided.
        [AdditionalNotesRequired(ErrorMessage = "Additional Notes are required if hours worked exceed 40.")]
        public string AdditionalNotes { get; set; }

        public HttpPostedFileBase SupportingDocument { get; set; } // File upload for supporting documents
    }

    // Custom validation attribute for conditional additional notes
    public class AdditionalNotesRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var claim = (Claim)validationContext.ObjectInstance;

            if (claim.HoursWorked > 40 && string.IsNullOrWhiteSpace(claim.AdditionalNotes))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
