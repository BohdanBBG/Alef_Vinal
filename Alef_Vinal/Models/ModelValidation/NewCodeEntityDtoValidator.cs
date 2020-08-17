using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Models.ModelValidation
{
    public class NewCodeEntityDtoValidator: AbstractValidator<NewCodeEntityDto>
    {
        public NewCodeEntityDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Value).NotNull().NotEmpty().Length(1, 3);
        }
    }
}
