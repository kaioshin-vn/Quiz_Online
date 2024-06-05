using ASM_PH35423.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASM_PH35423.Controllers
{
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutController(SignInManager<ApplicationUser> context)
        {
            _signInManager = context;

        }

        [HttpGet("/Logout")]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        } 
    }
}
