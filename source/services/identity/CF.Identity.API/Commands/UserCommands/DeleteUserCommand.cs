using CF.CustomMediator.Models;

namespace CF.Identity.API.Commands.UserCommands
{
    public class DeleteUserCommand : Command
    {
        public Guid UserId { get; set; }

        public DeleteUserCommand(Guid userId) : base()
        {
            UserId = userId;
        }

        public override bool IsValid()
        {
            return UserId != Guid.Empty;
        }
    }
}
