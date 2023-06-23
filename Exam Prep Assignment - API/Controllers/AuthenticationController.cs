using Exam_Prep_Assignment___API.Models;
using Exam_Prep_Assignment___API.View_Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Exam_Prep_Assignment___API.EmailServices;
using Exam_Prep_Assignment___API.Repository;

namespace Exam_Prep_Assignment___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        //Endpoints for Authentication;
        private readonly ITopSalesRepository _TopSalesRepository;
        private readonly IEmailService _EmailService;

        Random rnd = new Random();
        public AuthenticationController(ITopSalesRepository topSalesRepository, IEmailService emailService)
        {
            _TopSalesRepository = topSalesRepository;
            _EmailService = emailService;
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin(UserViewModel user)
        {
            try
            {
                string hashedPassword = hashPassword(user.Password);
                var validLogin = await _TopSalesRepository.CheckUserAsync(user.username, hashedPassword);

                if (validLogin == null)
                {
                    user.valid = false;
                    return Ok(user);
                }
                user.valid = true;
                validLogin.otp = rnd.Next(1000, 9999);

                if (await _TopSalesRepository.SaveChangesAsync())
                {
                    Email email = new Email();
                    email.EmailToId = user.username;
                    email.EmailSubject = "OTP - Assignment 3";
                    email.EmailBody = "Your one time pin is: " + validLogin.otp;
                    _EmailService.SendEmail(email);
                    return Ok(user);
                }
            }
            catch (Exception)
            {
                return BadRequest("Could not log in user");
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("CheckOtp")]
        public async Task<IActionResult> CheckOtp(UserViewModel user)
        {
            var validLogin = await _TopSalesRepository.CheckUserOTPAsync(user.username, user.otp);
            if (validLogin == null)
            {
                user.valid = false;
                return Ok(user);
            }
            else
            {
                user.valid = true;
                return Ok(user);
            }
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> AddNewUser(UserViewModel newUser)
        {
            var user = new User { username = newUser.username, Password = newUser.Password };
            try
            {
                var isNewUser = await _TopSalesRepository.CheckUserAsync(user.username, user.Password);

                if (isNewUser == null)
                {
                    if ((user.username.Contains('@')) && (user.Password.Length > 6) && (user.Password.Length < 16))
                    {
                        string hashedPassword = hashPassword(user.Password);
                        user.Password = hashedPassword;
                        _TopSalesRepository.Add(user);
                        await _TopSalesRepository.SaveChangesAsync();
                        user.valid = true;
                        user.usernameValid = true;
                        user.passwordValid = true;
                        return Ok(user);
                    }
                    else if (!(user.username.Contains('@')) && (user.Password.Length > 6) && (user.Password.Length < 16))
                    {
                        user.valid = false;
                        user.usernameValid = false;
                        user.passwordValid = true;
                        return Ok(user);
                    }
                    else if ((user.username.Contains('@')) && ((user.Password.Length < 6) || (user.Password.Length > 16)))
                    {
                        user.valid = false;
                        user.usernameValid = true;
                        user.passwordValid = false;
                        return Ok(user);
                    }
                    else if (!(user.username.Contains('@')) && ((user.Password.Length < 6) || (user.Password.Length > 16)))
                    {
                        user.valid = false;
                        user.usernameValid = false;
                        user.passwordValid = false;
                        return Ok(user);
                    }
                    return Ok(user);
                }
                else
                {
                    user.valid = false;
                    return Ok(user);
                }
            }
            catch (Exception)
            {
                return Ok(user);
            }
        }

        private string hashPassword(string password)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
