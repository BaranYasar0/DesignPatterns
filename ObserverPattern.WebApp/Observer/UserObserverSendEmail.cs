using System.Net.Mail;
using BaseProject.Models;

namespace ObserverPattern.WebApp.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser user)
        {
            var logger = serviceProvider.GetService<ILogger<UserObserverSendEmail>>();

            var mailMessage = new MailMessage();

            var smtpClient = new SmtpClient();
        }
    }
}
