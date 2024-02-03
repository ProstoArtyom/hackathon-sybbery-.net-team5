using BankAPI.Models;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    public class AlfaBankController : Controller
    {
        [HttpGet("rates")]
        public async Task<IActionResult> GetRates()
        {
            try
            {
                var rates = await GetInfo.GetRanks();

                if (!rates.Any())
                {
                    return NotFound();
                }

                return Ok(rates);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }
    }
}
