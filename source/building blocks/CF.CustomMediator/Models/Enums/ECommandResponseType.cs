namespace CF.CustomMediator.Models.Enums
{
    public enum ECommandResponseType
    {
        Created,
        Updated,
        Deleted,
        InvalidRequestData,
        NotFound,
        BusinessRuleConflict,
        InternalCommandError
    }
}
