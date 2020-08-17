using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Models
{
    public class NewCodeEntityDto
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 1)]
        public string Value { get; set; }
    }
}
