namespace CF.CustomMediator.Models
{
    public class ValidationResult
    {
        public IList<ValidationFailure> ValidationFailures { get; set; }

        public ValidationResult()
        {
            ValidationFailures = new List<ValidationFailure>();
        }

        public void AddError(IList<string> errors, string propertyName, string valueAttempted)
        {
            ValidationFailure failure = new(errors, valueAttempted, propertyName);

            ValidationFailures.Add(failure);
        }
    }
}
