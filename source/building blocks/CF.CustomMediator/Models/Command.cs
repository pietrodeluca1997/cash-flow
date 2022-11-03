namespace CF.CustomMediator.Models
{
    public abstract class Command : RequestMessage
    {
        public ValidationResult ValidationResult { get; set; }

        public Command()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract bool IsValid();
    }
}
