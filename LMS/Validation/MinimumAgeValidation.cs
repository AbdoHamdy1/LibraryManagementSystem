using System.ComponentModel.DataAnnotations;

namespace LMS.Validation
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                var age = DateTime.Today.Year - birthDate.Year;
                if (birthDate > DateTime.Today.AddYears(-age)) age--; // Adjust age if the birthday hasn't occurred yet this year

                if (age < _minimumAge)
                {
                    return new ValidationResult($"You must be at least {_minimumAge} years old.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
