namespace CF.CustomMediator.Models
{
    public class ValidationFailure
    {
        public string PropertyName { get; set; }
        public string ValueAttempted { get; set; }
        public IList<string> Errors { get; set; }

        public ValidationFailure(IList<string> errors, string propertyName, string valueAttempted)
        {
            Errors = errors;
            PropertyName = propertyName;
            ValueAttempted = valueAttempted;
        }
    }
}
