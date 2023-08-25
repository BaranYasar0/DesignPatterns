using System.Security.Claims;
using BaseProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StrategyPattern.WebApp.Models;

namespace StrategyPattern.WebApp.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public SettingController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Settings settings = new();
            if (User.Claims.Where(x => x.Type == Settings.ClaimDatabaseType).FirstOrDefault() != null)
            {
                settings.DatabaseType = (EDatabaseType)int.Parse(User.Claims.First(x => x.Type == Settings.ClaimDatabaseType).Value);
            }

            else
            {
                settings.DatabaseType = settings.GetDefaultDatabaseType;
            }
            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var newClaim = new Claim(Settings.ClaimDatabaseType, databaseType.ToString());

            var claims = await userManager.GetClaimsAsync(user);

            var hasDatabaseTypeClaim = claims.FirstOrDefault(x => x.Type == Settings.ClaimDatabaseType);

            if (hasDatabaseTypeClaim != null)
            {
                await userManager.ReplaceClaimAsync(user, hasDatabaseTypeClaim, newClaim);
            }
            else
            {
                await userManager.AddClaimAsync(user, newClaim);
            }

            await _signInManager.SignOutAsync();

            var result = await HttpContext.AuthenticateAsync();

            await _signInManager.SignInAsync(user,result.Properties);
            return RedirectToAction("Index");
        }
    }
}
