using BaseProject.Models;
using ObserverPattern.WebApp.Models;

namespace ObserverPattern.WebApp.Observer
{
    public class UserObserverCreatedDiscount : IUserObserver
    {
        private readonly IServiceProvider serviceProvider;

        public UserObserverCreatedDiscount(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser user)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<UserObserverCreatedDiscount>>();



            using var scope = serviceProvider.CreateScope();
           
            var context = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            context.Discounts.Add(new Discount() { Rate = 10, UserId = user.Id });
            context.SaveChanges();

            logger.LogInformation($"Discount was created.");
        }
    }
}
