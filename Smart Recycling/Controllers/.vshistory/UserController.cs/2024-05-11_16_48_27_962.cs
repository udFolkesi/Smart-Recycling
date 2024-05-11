using AutoMapper;
using BLL.Services;
using CORE.Helpers;
using CORE.Models;
using DAL.Contexts;
using IronQr;
using IronSoftware.Drawing;
using iTextSharp.text.pdf.qrcode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartRecycling.Dto;
using System.Drawing;
using System.Formats.Tar;

namespace SmartRecycling.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly SmartRecyclingDbContext dbContext;
        private readonly IMapper _mapper;

        public UserController(SmartRecyclingDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                IQueryable<User> query = dbContext.User
                    .Include(u => u.UserStatistics)
                    .Include(u => u.Operations);

                var user = await query.FirstOrDefaultAsync(c => c.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            var existingUser = await dbContext.User.Where(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                ModelState.AddModelError("", "User with such email already exists");
                return StatusCode(422, ModelState);
            }

            var userMap = _mapper.Map<User>(user);
            userMap.Password = AuthService.HashPassword(userMap.Password);
            //userMap.ConfirmationCode = EmailSenderHelper.SendConfirmation("alexeyfromov@gmail.com", user.Password, "oleksiy.salamatov@nure.ua");

            await dbContext.User.AddAsync(userMap);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDto updatedUserDto)
        {
            if (updatedUserDto == null || id != updatedUserDto.Id)
                return BadRequest(ModelState);

            var existingUser = await dbContext.User.FindAsync(id);

            if (existingUser == null)
                return NotFound();

            _mapper.Map(updatedUserDto, existingUser);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dbContext.User.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userToDelete = await dbContext.User.FindAsync(id);
            if (userToDelete == null) 
                return NotFound();

            dbContext.User.Remove(userToDelete);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{userId}")]
        public ActionResult GenerateQRCode(int userId)
        {
            QrCode myQr = QrWriter.Write($"User Id: {2}");
            AnyBitmap qrImage = myQr.Save();
            qrImage.SaveAs("qrCode.png");
            return File(System.IO.File.ReadAllBytes("qrCode.png"), "image/png");
        }
    }
}
