using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ObserverPattern.WebApp.Events;
using ObserverPattern.WebApp.Observer;

namespace ObserverPattern.WebApp.EventHandlers
{
    public class CreatedUserWriteToConsoleEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<CreatedUserWriteToConsoleEventHandler> logger;

        public CreatedUserWriteToConsoleEventHandler(ILogger<CreatedUserWriteToConsoleEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogError($"User {notification.AppUser.UserName} created");

            return Task.CompletedTask;
        }
    }
}
