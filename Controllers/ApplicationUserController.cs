using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserRegWithAngularInNetCoreWebApi.Model;

namespace UserRegWithAngularInNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        //ILogger<ApplicationUserController> logger;
        public UserManager<ApplicationUser> userManager;
        public SignInManager<ApplicationUser> signInManager;
        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost]
        [Route("registration")]
        //Post
        public async Task<IActionResult> Register(RegisterModel model)
        {
            //var userExists = await userManager.FindByNameAsync(model.Username);
            var user = new ApplicationUser()
            {
                FullName = model.Fullname,
                UserName = model.Username,
                Email = model.Email
            };
            var reault = await userManager.CreateAsync(user, model.Password);
            return Ok(reault);
        }
        [HttpPost]
        [Route("login")]
        //Post
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                return RedirectToAction("Register");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
