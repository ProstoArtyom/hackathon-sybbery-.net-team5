using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private static readonly IEnumerable<Bank> Banks = new List<Bank>()
        {
            new Bank { Code = "", Name = "Bank A" },
            new Bank { Code = "", Name = "Bank B" },
            new Bank { Code = "", Name = "Bank C" },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Bank>> GetBanks()
        {
            try
            {
                return Ok(Banks);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }

        [HttpGet("{bankName}/currencies")]
        public ActionResult<IEnumerable<string>> GetBankCurrencies(string bankName)
        {
            try
            {
                var bank = Banks.FirstOrDefault(b => b.Name == bankName);
                if (bank == null)
                {
                    return NotFound("Bank with the specified name not found.");
                }

                var currencies = new List<string> { "USD", "EUR", "GBP" };

                return Ok(currencies);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }


        [HttpGet("rate")]
        public ActionResult<IEnumerable<Rate>> GetRate(string bankName = "Default Bank", string currencyCode = null, DateTime? date = null)
        {
            try
            {
                if (date == null)
                {
                    date = DateTime.Now.Date;
                }

                // TODO: get list of rates
                IEnumerable<Rate> rates = new List<Rate>
                {
                    new() { Cur_ID = 1, Date = DateTime.Now.AddDays(-1),  },
                };

                rates = rates.Where(x => x.Date.Date == date).ToList();
                if (!string.IsNullOrEmpty(currencyCode))
                {
                    var rate = rates.FirstOrDefault(x => x.Cur_ID.ToString() == currencyCode);
                    if (rate == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        Ok(rate);
                    }
                }

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
