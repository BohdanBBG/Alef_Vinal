using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Models
{
    public class CodeEntity: BaseEntity
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

}
