using System;
using System.Security.Claims;
using DaySlayer.Models;
using DaySlayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaySlayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public HistoryController(IHistoryRepository historyRepository, IUserProfileRepository userProfileRepository)
        {
            _historyRepository = historyRepository;
            _userProfileRepository = userProfileRepository;
        }
        [HttpGet("limit/{id}")]
        public IActionResult GetOwners(int id, [FromQuery] Pagination pagination )
        {
            try
            {
                var owners = _historyRepository.GetHistoryPagination(pagination, id);
                return Ok(owners);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
            
        }
        //User words not acronyms
        [HttpGet("uh/{id}")]
        public IActionResult GetHistory(int id)
        {
            try
            {
                var history = _historyRepository.GetHistory(id);
                return Ok(history);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }

            
        }
        [HttpGet("sh/{id}")]
        public IActionResult GetHistoryById(int id)
        {
            try
            {
                var history = _historyRepository.GetHistoryById(id);
                return Ok(history);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
           
        }
        [HttpPost]
        public IActionResult AddHistory(History history)
        {
            try
            {
                int id = _historyRepository.AddHistory(history);
                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
            
        }
        [HttpPut("{id}")]
        public IActionResult UpdateHistory(int id, History history)
        {
            try
            {
            _historyRepository.UpdateHistory(id, history);
            return Ok();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NoContent();
            }
           
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHistory(int id)
        {
            try
            {
                _historyRepository.DeleteHistory(id);
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
            
        }
    }
}
