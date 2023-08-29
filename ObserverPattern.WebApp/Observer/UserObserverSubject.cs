using BaseProject.Models;

namespace ObserverPattern.WebApp.Observer
{
    public class UserObserverSubject
    {
        private readonly List<IUserObserver> userObservers;

        public UserObserverSubject()
        {
            this.userObservers=new List<IUserObserver>();
        }

        public void RegisterObserver(IUserObserver userObserver)
        {
            this.userObservers.Add(userObserver);
        }

        public void RemoveObserver(IUserObserver userObserver)
        {
            this.userObservers.Add(userObserver);
        }

        public void NotifyObservers(AppUser user)
        {
            foreach (var userObserver in this.userObservers)
            {
                userObserver.UserCreated(user);
            }
        }
    }
}
