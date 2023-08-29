using BaseProject.Models;
using MediatR;

namespace ObserverPattern.WebApp.Events
{
    public class UserCreatedEvent:INotification
    {
        public AppUser AppUser { get; set; }

    }
}
