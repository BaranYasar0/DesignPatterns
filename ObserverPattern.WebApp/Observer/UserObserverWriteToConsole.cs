using BaseProject.Models;

namespace ObserverPattern.WebApp.Observer
{
    public class UserObserverWriteToConsole:IUserObserver
    {
        private readonly IServiceProvider serviceProvider;

        public UserObserverWriteToConsole(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser user)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<UserObserverWriteToConsole>>();

            logger.LogError($"User {user.UserName} created");
        }
    }
}
