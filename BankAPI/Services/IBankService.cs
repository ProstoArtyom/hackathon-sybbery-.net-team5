using BankAPI.Models;

namespace BankAPI.Services
{
    public interface IBankService
    {
        public List<Bank> Banks { get; set; }
    }
}
