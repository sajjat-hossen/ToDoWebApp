using System.ComponentModel.DataAnnotations;

namespace ToDo.DomainLayer.CustomDataAnnotation
{
    public class ValidDueDateAttribute : ValidationAttribute
    {
        public ValidDueDateAttribute() 
        {
            ErrorMessage = "The Due Date cannot be in the past";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                DateTime dueDate = (DateTime)value;

                if (dueDate < DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage);
                }

            }

            return ValidationResult.Success;
        }

    }
}
