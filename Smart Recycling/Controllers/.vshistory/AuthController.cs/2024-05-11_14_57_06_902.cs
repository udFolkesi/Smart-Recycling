using BLL.Services;
using CORE.Helpers;
using CORE.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private SmartRecyclingDbContext dbContext;
        private readonly AuthService authService;
        public AuthController(SmartRecyclingDbContext dbContext, AuthService authService)
        {
            this.dbContext = dbContext;
            this.authService = authService;
        }

        [HttpPost("/token")]
        public IActionResult Token(string email, string password)
        {
            //EmailSenderHelper.SendConfirmation("alexeyfromov@gmail.com", /*dbContext.User.First().Password,*/ "oleksiy.salamatov@nure.ua");

            var identity = authService.GetIdentity(email, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            if (!AuthService.VerifyHashedPassword(dbContext.User.FirstOrDefault(u => u.Email == email).Password, password))
            {
                return BadRequest(new { errorText = "Invalid password." });
            }

            if (!dbContext.User.FirstOrDefault(u => u.Email == email).IsEmailConfirmed)
            {
                return BadRequest("Email is not confirmed");
            }

            return Json(authService.GetResponse(email, password, identity));
        }

        [HttpPost("{id}{code}")]
        public async Task<IActionResult> ConfirmCode(int id, string code)
        {
            var user = dbContext.User.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // User not found
            }

            if (user.ConfirmationCode == code)
            {
                user.IsEmailConfirmed = true;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Invalid confirmation code");
            }
        }








        /*// Action method for user registration
        [HttpPost]
        public ActionResult Register(string email)
        {
            // Generate confirmation token
            string token = Guid.NewGuid().ToString();

            // Save the token and email in your database with IsActive set to false

            // Send confirmation email
            SendConfirmationEmail(email, token);

            return View("RegistrationSuccessful");
        }

        // Action method to handle confirmation link
        public ActionResult ConfirmEmail(string email, string token)
        {
            // Retrieve the token from the database based on the email
            // Check if the token matches the one stored in the database
            // If they match, set IsActive to true in the database

            return View("EmailConfirmed");
        }

        // Method to send confirmation email
        private void SendConfirmationEmail(string email, string token)
        {
            string confirmationLink = Url.Action("ConfirmEmail", "Account", new { email = email, token = token }, Request.Url.Scheme);

            // You can customize the email message as per your requirement
            string body = $"Please click the following link to activate your account: {confirmationLink}";

            MailMessage message = new MailMessage();
            message.To.Add(email);
            message.Subject = "Confirm your email address";
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient("your_smtp_server_address");
            // Configure SMTP client settings

            // Send email
            smtpClient.Send(message);
        }*/
    }
}
