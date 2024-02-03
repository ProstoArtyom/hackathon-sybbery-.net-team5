using BankAPI.Models;

namespace BankAPI.Services
{
    public class BankService : IBankService
    {
        public List<Bank> Banks { get; set; }
        public BankService()
        {
            
        }       

    }
    
}
