using System;
using System.Collections.Generic;
using DaySlayer.Models;
using DaySlayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaySlayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize]
    public class RitualController : ControllerBase
    {
        private readonly IRitualRepository _ritualRepository;

        public RitualController(IRitualRepository ritualRepository)
        {
            _ritualRepository = ritualRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserRituals(int id)
        {
            try
            {
            List<Ritual> rituals = _ritualRepository.GetRituals(id);
            return Ok(rituals);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult AddRitual(Ritual ritual)
        {
            try
            {
                _ritualRepository.AddRitual(ritual);
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }   
        }
        [HttpPut]
        public IActionResult UpdateRitual(Ritual ritual)
        {
            try
            {
                _ritualRepository.UpdateRitual(ritual);
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
            
        }
        [HttpGet("activate/{id}")]
        public IActionResult Activate(int id)
        {
            try
            {
            _ritualRepository.ReactivateRitual(id);
            return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
            
        }
        [HttpGet("deactivate/{id}")]
        public IActionResult DeActivate(int id)
        {
            try
            {
            _ritualRepository.DeactivateRitual(id);
            return Ok();
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return NotFound();
            }

        }
    }
}
