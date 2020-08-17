using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Models
{
    public class CodeEntity: BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class CodeEntityValidator : AbstractValidator<CodeEntity>
    {
        public CodeEntityValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 20);
            RuleFor(x => x.Value).NotNull().NotEmpty().Length(1, 3);
        }
    }
}
