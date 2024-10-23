using System;

namespace Miljoboven.Models;

public class ErrandTableViewModel
{
    public string Message { get; set; }
    public IEnumerable<MyErrand> ErrandList { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
}