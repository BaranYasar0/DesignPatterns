using BaseProject.Models;
using MediatR;
using ObserverPattern.WebApp.Models;

namespace ObserverPattern.WebApp.Events
{
    public class CreateDiscountEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly AppIdentityDbContext context;
        private readonly ILogger<CreateDiscountEventHandler> logger;

        public CreateDiscountEventHandler(AppIdentityDbContext context, ILogger<CreateDiscountEventHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            await context.Discounts.AddAsync(new Discount() { Rate = 10, UserId = notification.AppUser.Id });

            await context.SaveChangesAsync();

            logger.LogInformation($"Discount was created.");
        }
    }
}
