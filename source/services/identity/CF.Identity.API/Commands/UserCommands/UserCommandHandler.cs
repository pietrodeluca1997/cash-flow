using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;
using CF.CustomMediator.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace CF.Identity.API.Commands.UserCommands
{
    public class UserCommandHandler : IRequestMessageHandler<CreateNewUserCommand>,
                                      IRequestMessageHandler<DeleteUserCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;
        public UserCommandHandler(UserManager<IdentityUser> userManager,
                                  IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task Handle(CreateNewUserCommand requestMessage)
        {
            try
            {
                if (!requestMessage.IsValid())
                {
                    _mediator.FinishRequestBehaviour(requestMessage, ECommandResponseType.InvalidRequestData);
                    return;
                }

                IdentityUser newUser = new()
                {
                    UserName = requestMessage.Email,
                    Email = requestMessage.Email,
                    EmailConfirmed = true
                };

                IdentityResult identityResult = await _userManager.CreateAsync(newUser, requestMessage.Password);

                if (identityResult.Succeeded)
                {
                    UserCreatedEvent userCreatedEvent = new()
                    {
                        CPFNumber = requestMessage.CPFNumber,
                        Name = requestMessage.Name,
                        UserId = newUser.Id
                    };

                    await _mediator.Notify(userCreatedEvent);

                    _mediator.FinishRequestBehaviour(requestMessage, ECommandResponseType.Created);
                }
                else
                {
                    requestMessage.ValidationResult.AddError(new string[] { "E-mail already been taken" }, nameof(requestMessage.Email), requestMessage.Email);

                    _mediator.FinishRequestBehaviour(requestMessage, ECommandResponseType.BusinessRuleConflict);
                }
            }
            catch
            {
                _mediator.FinishRequestBehaviour(requestMessage, ECommandResponseType.InternalCommandError);
            }
        }

        public async Task Handle(DeleteUserCommand requestMessage)
        {
            if (!requestMessage.IsValid()) return;

            IdentityUser identityUser = await _userManager.FindByIdAsync(requestMessage.UserId.ToString());

            IdentityResult identityResult = await _userManager.DeleteAsync(identityUser);
        }
    }
}
