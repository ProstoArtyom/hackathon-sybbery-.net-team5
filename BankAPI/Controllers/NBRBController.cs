using BankAPI.Models;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    public class NBRBController : Controller
    {
        [HttpGet("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            try
            {
                var rates = await GetInfo.GetCurrencies();

                if (!rates.Any())
                {
                    return NotFound();
                }

                return Ok(rates.Select(x => new { Id = x.Cur_ID, Name = x.Cur_Abbreviation }));
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }

        [HttpGet("currency/{currencyId}")]
        public async Task<IActionResult> GetCurrency(int currencyId)
        {
            try
            {
                var currencies = await GetInfo.GetBBRates(currencyId);

                if (currencies == null)
                {
                    return NotFound();
                }

                return Ok(currencies);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }

        [HttpGet("currenciesByTime/{currencyId}")]
        public async Task<IActionResult> GetCurrencies(int currencyId, DateTime? from = null, DateTime? to = null)
        {
            try
            {
                if (from == null)
                {
                    from = DateTime.Now.Date.AddDays(-1);
                }
                if (to == null)
                {
                    to = DateTime.Now.Date.AddDays(-1);
                }

                var currencies = await GetInfo.GetBBRatesByDates(currencyId, from.Value, to.Value);

                if (!currencies.Any())
                {
                    return NotFound();
                }

                return Ok(currencies);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }
    }
}
