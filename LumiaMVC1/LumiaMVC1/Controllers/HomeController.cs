using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LumiaMVC1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamService _teamService;
        public HomeController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public IActionResult Index()
        {
            var teams= _teamService.GetAllTeams();
            return View(teams);
        }
    }
}
