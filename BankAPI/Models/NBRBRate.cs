using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class NbrbRate
    {
        public int Cur_ID { get; set; }
        public DateTime Date { get; set; }
        public string Cur_Abbreviation { get; set; }
        public int Cur_Scale { get; set; }
        public string Cur_Name { get; set; }
        public float Cur_OfficialRate { get; set; }
    }
}
