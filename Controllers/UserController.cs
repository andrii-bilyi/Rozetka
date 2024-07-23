using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rozetka.Data;
using Rozetka.Data.Entity;
using Rozetka.Models.Signup;
using Rozetka.Services;
using Rozetka.Services.Hash;
using System.Security.Claims;
using System.Text.Json;

namespace Rozetka.Controllers
{
    public class UserController : Controller
    {
        //private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IHashService _hashService;

        //public UserController(DataContext context, IHashService hashService, IUserService userService)
        //{
        //    _context = context;
        //    _hashService = hashService;
        //    _userService = userService;
        //}

        public UserController(IHashService hashService, IUserService userService)
        {           
            _hashService = hashService;
            _userService = userService;
        }

        public async Task<ActionResult> PersonalDataAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            //Guid.TryParse(userId, out Guid userGuid);
            if (userId == null)
            {
                return RedirectToAction("Index", "Home"); // Перенаправление на домашнюю страницу
            }
            // Получаем пользователя
            User user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Home"); // Перенаправление на домашнюю страницу
            }
            return View(user);
        }

        public ViewResult SignUp()
        {
            SignupViewModel viewModel = new();

            // перевіряємо, чи є дані від форми
            if (HttpContext.Session.Keys.Contains("formStatus"))
            {
                // декодуємо статус
                viewModel.FormStatus = Convert.ToBoolean(
                    HttpContext.Session.GetString("formStatus"));
                HttpContext.Session.Remove("formStatus");

                // перевіряємо - якщо помилковий, то у сесії дані валідації і моделі
                if (viewModel.FormStatus ?? false)
                {
                    viewModel.FormModel = null;
                    viewModel.FormValidation = null;
                    return View(viewModel);
                }
                else
                {
                    viewModel.FormModel = JsonSerializer
                        .Deserialize<SignupFormModel>(
                            HttpContext.Session.GetString("formModel")!);
                    HttpContext.Session.Remove("formModel");

                    viewModel.FormValidation = JsonSerializer
                        .Deserialize<SignupFormValidation>(
                            HttpContext.Session.GetString("formValidation")!);
                    HttpContext.Session.Remove("formValidation");
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<RedirectToActionResult> SignupForm(SignupFormModel formModel)
        {
            SignupFormValidation results = new();
            bool isFormValid = true;
            //var users = new SignupViewModel();
            //users.Signups = await _cosmosDbService.LoadSignupsAsync();

            if (String.IsNullOrEmpty(formModel.FirstName))
            {
                results.FirstNameErrorMessage = "Поле 'Ім'я' обов'язкове для заповнення";
                isFormValid = false;
            }

            if (String.IsNullOrEmpty(formModel.LastName))
            {
                results.LastNameErrorMessage = "Поле 'Прізвище' обов'язкове для заповнення";
                isFormValid = false;
            }

            if (String.IsNullOrEmpty(formModel.Phone))
            {
                results.PhoneErrorMessage = "Поле 'Телефон' обов'язкове для заповнення";
                isFormValid = false;
            }

            if (String.IsNullOrEmpty(formModel.Email))
            {
                results.EmailErrorMessage = "Email не може бути порожнім";
                isFormValid = false;
            }
            //else
            //{
            //    foreach (var user in users.Signups)
            //    {
            //        if (user.UserEmail == formModel.Email)
            //        {
            //            results.EmailErrorMessage = "Помилка! Аккаунт з цією поштою вже існує!";
            //            isFormValid = false;
            //        }
            //    }
            //}
            if (String.IsNullOrEmpty(formModel.Password))
            {
                results.PasswordErrorMessage = "Пароль не може бути порожним";
                isFormValid = false;
            }

            if (formModel.Password != formModel.Repeat)
            {
                results.RepeatErrorMessage = "Повтор не збігається з паролем";
                isFormValid = false;
            }

            // до цього місця доходимо у разі відсутності помилок валідації
            // додаємо нового користувача до БД
            String salt = _hashService.HexString(Guid.NewGuid().ToString());
            String dk = _hashService.HexString(salt + formModel.Password);


            if (!isFormValid)
            {
                HttpContext.Session.SetString("formModel",
                    JsonSerializer.Serialize(formModel));

                HttpContext.Session.SetString("formValidation",
                    JsonSerializer.Serialize(results));

            }
            else
            {
                User user = new()
                {
                    FirstName = formModel.FirstName,
                    LastName = formModel.LastName,
                    PhoneNumber = formModel.Phone,
                    Email = formModel.Email,
                    PasswordHash = formModel.Password,
                    //PasswordSalt = salt,
                    //PasswordDk = dk,
                    RegisterDt = DateTime.Now
                };
                await _userService.AddUserAsync(user);               
            }
            HttpContext.Session.SetString("formStatus",
                isFormValid.ToString());

            return RedirectToAction(nameof(SignUp));
        }
    }
}
