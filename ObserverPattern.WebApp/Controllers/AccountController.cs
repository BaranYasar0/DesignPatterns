using BaseProject.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ObserverPattern.WebApp.Events;
using ObserverPattern.WebApp.Models;
using ObserverPattern.WebApp.Observer;

namespace BaseProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserObserverSubject userObserverSubject;
        private readonly IMediator mediator;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, UserObserverSubject userObserverSubject, IMediator mediator)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userObserverSubject = userObserverSubject;
            this.mediator = mediator;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hasUser = await userManager.FindByEmailAsync(email);

            if (hasUser == null) return View();

            var signInResult = await signInManager.PasswordSignInAsync(hasUser, password, true, false);

            if(!signInResult.Succeeded) return View();

            return RedirectToAction(nameof(HomeController.Index), "home");
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateViewModel model)
        {
            AppUser user= new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                //userObserverSubject.NotifyObservers(user);
                await mediator.Publish(new UserCreatedEvent(){AppUser = user});
                
                ViewBag.message = "Üyelik işlemi gerçekleşti.";
                await signInManager.SignInAsync(user, true);
                return View();
            }
            else
            {
                ViewBag.message = result.Errors.ToList().First().Description;
                return View();
            }
            

        }
    }
}
