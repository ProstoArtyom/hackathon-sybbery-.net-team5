using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankAPI.Models;

public class Rate
{
    public decimal SellRate { get; set; }
    public string SellIso { get; set; }
    public int SellCode { get; set; }

    public decimal BuyRate { get; set; }
    public string BuyIso { get; set; }
    public int BuyCode { get; set; }

    public int Quantity { get; set; }
    public string Name { get; set; }

    public DateTime Date { get; set; }
}