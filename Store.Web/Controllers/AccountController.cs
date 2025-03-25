using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Web.App;
using Store.Data.EF.Identity;
using Store.Web.Models;
using System;


namespace Store.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderService _orderService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, 
            IOrderRepository orderRepository, OrderService orderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _orderRepository = orderRepository;
            _orderService = orderService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            User user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.GetUserAsync(User);
                ChangeProfileViewModel model = new ChangeProfileViewModel
                {
                    Name = user.UserName,
                    cellPhone = user.PhoneNumber
                };
                return View(model);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Change(ChangeProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Зміна імені користувача, якщо вказано нове
            if (!string.IsNullOrEmpty(model.Name) && user.UserName != model.Name)
            {
                var userNameExists = await _userManager.FindByNameAsync(model.Name);
                if (userNameExists != null)
                {
                    ModelState.AddModelError("Name", "Це ім'я користувача вже зайняте");
                    return View("Index", model);
                }

                user.UserName = model.Name;
                var updateUserNameResult = await _userManager.UpdateAsync(user);
                if (!updateUserNameResult.Succeeded)
                {

                    return View("Index", model);
                }
            }

            // Зміна пароля, якщо вказано новий
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                Console.WriteLine(model.NewPassword);
                var changePasswordResult = await _userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    Console.WriteLine("Error");
                    ModelState.AddModelError("CurrentPassword", "Невірний пароль");
                    return View("Index", model);
                }
            }

            // Оновлюємо автентифікацію користувача
            await _signInManager.RefreshSignInAsync(user);

            TempData["SuccessMessage"] = "Дані успішно оновлено";
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                User user = UserTransformer.ToUser(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Login");
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                User user = UserTransformer.ToUser(model);

                var result = await _signInManager.PasswordSignInAsync(user ,model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Невірне ім'я або пароль");
                    return View(model);
                }
            }

            
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> OrderHistory()
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var orders = await _orderRepository.GetOrdersAsync(user.OrderIds.ToList());
            if (orders == null)
            {
                return View(new List<OrderViewModel>());
            }
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                orderViewModels.Add(await _orderService.MapAsync(order));
            }

            return View(orderViewModels);
        }
    }
}
