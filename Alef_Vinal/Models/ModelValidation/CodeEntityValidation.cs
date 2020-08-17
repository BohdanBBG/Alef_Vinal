using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Models.ModelValidation
{
    public class CodeEntityValidator: AbstractValidator<CodeEntity>
    {
        public CodeEntityValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Value).NotNull().NotEmpty().Length(1, 3);
        }
    }
}
