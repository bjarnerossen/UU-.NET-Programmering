using System;
using System.ComponentModel.DataAnnotations;

namespace Miljoboven.Models
{
    public class ErrandStatus
    {
        [Key]
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
