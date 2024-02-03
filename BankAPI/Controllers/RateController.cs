using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    public class RateController : Controller
    {
        private static readonly IEnumerable<Bank> Banks = new List<Bank>()
        {
            new Bank { Code = "1", Name = "Bank A", Rates = new List<Rate>
            {
                new() { Date = DateTime.Now.AddDays(-1),  },
                new() { Date = DateTime.Now.AddDays(-2),  },
                new() { Date = DateTime.Now.AddDays(-3),  },
                new() { Date = DateTime.Now.AddDays(-1),  },
            }},
            new Bank { Code = "", Name = "Bank B" },
            new Bank { Code = "", Name = "Bank C" },
        };

        [HttpGet("Rate")]
        public ActionResult<IEnumerable<Rate>> GetRate(string bankName = "Default Bank", string currencyCode = null, DateTime? date = null)
        {
            try
            {
                if (date == null)
                {
                    date = DateTime.Now.Date.AddDays(-1);
                }

                // TODO: get list of rates

                var newRates = Banks.Where(x => x.RatesDate.Date == date).ToList();
                if (!string.IsNullOrEmpty(currencyCode))
                {
                    var rate = newRates.FirstOrDefault(x => x.Cur_ID.ToString() == currencyCode);
                    if (rate == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        Ok(rate);
                    }
                }

                if (!newRates.Any())
                {
                    return NotFound();
                }

                return Ok(newRates);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }

        [HttpGet("Rate/rates")]
        public ActionResult<IEnumerable<Rate>> GetRates(string currencyCode = null,
            string bankName = "Default Bank",
            DateTime? from = null, DateTime? to = null)
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

                // TODO: get list of rates

                var newRates = rates.Where(x => x.Date.Date >= from
                    && x.Date.Date <= to).ToList();
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

        [HttpGet("Rate/statistics")]
        public ActionResult<IEnumerable<Rate>> GetStatistics(string currencyCode = null,
            string bankName = "Default Bank",
            DateTime? from = null, DateTime? to = null)
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

                // TODO: get list of rates

                var newRates = rates.Where(x => x.Date.Date >= from
                    && x.Date.Date <= to).ToList();
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
