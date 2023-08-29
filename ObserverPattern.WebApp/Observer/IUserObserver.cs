using BaseProject.Models;

namespace ObserverPattern.WebApp.Observer
{
    public interface IUserObserver
    {
        void UserCreated(AppUser user);

    }
}
