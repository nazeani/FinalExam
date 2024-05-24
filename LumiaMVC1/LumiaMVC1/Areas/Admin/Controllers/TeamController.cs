using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FileNotFoundException = Business.Exceptions.FileNotFoundException;

namespace LumiaMVC1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            var teams= _teamService.GetAllTeams();
            return View(teams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team team)
        {
            if(!ModelState.IsValid) return View();
            try
            {
                _teamService.Add(team);
            }
            catch(ImageContentException ex)
            {
                ModelState.AddModelError("imageFile",ex.Message);
                return View();
            }
            catch (ImageLengthExceptions ex)
            {
                ModelState.AddModelError("imageFile", ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("imageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
           var team = _teamService.GetTeam(x => x.Id == id);
            return View(team);
        }
        [HttpPost]
        public IActionResult Update(Team team)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _teamService.Update(team.Id, team);
            }
            catch (ImageContentException ex)
            {
                ModelState.AddModelError("imageFile", ex.Message);
                return View();
            }
            catch (ImageLengthExceptions ex)
            {
                ModelState.AddModelError("imageFile", ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var team = _teamService.GetTeam(x => x.Id == id);
            return View(team);
        }
        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _teamService.Delete(id);
            }
            catch (EntityNotFoundException ex)
            {
               return NotFound(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
