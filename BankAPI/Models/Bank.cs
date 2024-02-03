using System.Reflection.Metadata;

namespace BankAPI.Models
{
    public class Bank
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<Rate> Rates { get; set; }
    }
}
