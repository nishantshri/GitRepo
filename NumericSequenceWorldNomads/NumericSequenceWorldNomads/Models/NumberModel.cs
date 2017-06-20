using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NumericSequenceWorldNomads.Models
{
    public class NumberModel
    {
        [Required]
        public int Number { get; set; }
        public string AllNumbers { get; set; }
        public string OddNumbers { get; set; }
        public string EvenNumbers { get; set; }
        public string SubstitutedNumbers { get; set; }
        public string FibonacciNumbers { get; set; }
    }
}