using System.Security.Policy;
using AutoMapper;
using CarRentalSystem.DAL.Models;
using CarRentalSystem.PL.DTO;
using CarRentalSystem.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IActionResult> Profile()
        {
            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                // If no user is logged in, redirect to SignIn
                return RedirectToAction("SignIn");
            }

            // Map AppUser to your Profile DTO (if you have one)
            var profileDto = _mapper.Map<ProfileDTO>(user);

            // Send it to the view
            return View(profileDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePersonalInfo(ProfileDTO model)
        {
            // First, check if the model state is valid
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound(); // Or handle the error appropriately
                }

                if (user.PictureURL is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.PictureURL, "UsersImages");
                }

                if (model.Image is not null)
                {
                    model.PictureURL = DocumentSettings.UploadFile(model.Image, "UsersImages");
                    user.PictureURL = model.PictureURL;
                }


              

                // Map the updated information from the model to the user entity
                user.UserName = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email; // Be careful allowing email changes, it might need re-verification
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.CreditCardNumber = model.CreditCardNumber; // Re-read security warnings about this
                user.DateOfBirth = model.DateOfBirth;
                user.Gender = model.Gender;

                // Save the changes to the database
                await _userManager.UpdateAsync(user);

                // Add a success message to show after redirecting
                TempData["StatusMessage"] = " Your profile has been updated successfully!";
        
        // **THE MAIN FIX: Redirect to the GET action**
        return RedirectToAction("Profile");
            }

            // If ModelState is not valid, return the view with the current data and validation errors
            return View("Profile", model);
        }


        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
        #endregion
    }
}
