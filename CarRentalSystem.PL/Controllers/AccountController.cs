using AutoMapper;
using CarRentalSystem.DAL.Models;
using CarRentalSystem.PL.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.PL.Controllers
{
    public class AccountController(IMapper _mapper, UserManager<AppUser> _userManager,SignInManager<AppUser> _signInManager) : Controller
    {
        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    ModelState.AddModelError(string.Empty, "Email is already exist");
                    return View(model);
                }
                var newUser = _mapper.Map<AppUser>(model);
                newUser.UserName = $"{newUser.FirstName}{Guid.NewGuid().GetHashCode().ToString("X8")}";
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

           
            return View(model);
        }
        #endregion

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if(flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);
                        if(result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View(model);
        }
        #endregion
    }
}
