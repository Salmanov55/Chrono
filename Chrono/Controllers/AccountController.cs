using Chrono.Enums;
using Chrono.Models;
using Chrono.ViewModels.AccountVM;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace Chrono.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM,string returnUrl)
        {
            if (loginVM == null) return NotFound();
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Username or Email or Password invalid!");
                    return View();
                }
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync
                (user, loginVM.Password, loginVM.RememmberMe, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Account bloked !");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Email or Password invalid!");
                return View(loginVM);
            }

            await _signInManager.SignInAsync(user, true);

            if (returnUrl != null) return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationVM registrationVM)
        {

            if (!ModelState.IsValid) return View(registrationVM);
            AppUser appUser = new();
            appUser.FullName = registrationVM.FullName;
            appUser.UserName = registrationVM.UserName;
            appUser.Email = registrationVM.Email;
            IdentityResult result = await _userManager.CreateAsync(appUser, registrationVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registrationVM);
            }

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

            string? link = Url.Action(nameof(ConfirmEmail),
                "Account",
                new { userId = appUser.Id, token },
                Request.Scheme,
                Request.Host.ToString());

            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("qurban231293@gmail.com"));
            email.To.Add(MailboxAddress.Parse(appUser.Email));
            email.Subject = "Email Confirmation";
            string body = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/Template/Verify.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{link}}", link);
            body = body.Replace("{{Fullname}}", appUser.FullName);

            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("qurban231293@gmail.com", "olszimzdwkxyjwwz");
            smtp.Send(email);
            smtp.Disconnect(true);



            return RedirectToAction(nameof(VerifyEmail));

        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return NotFound();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();


            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction(nameof(SuccessfulRegistered));

        }

        public IActionResult SuccessfulRegistered()
        {
            return View();
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }


        public async Task<IActionResult> CreateRoles()
        {

            foreach (var item in Enum.GetValues(typeof(EnumRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
            }
            return Content("elave edildi");
        }
    }
}
