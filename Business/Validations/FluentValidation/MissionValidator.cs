using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations.FluentValidation
{
    public class MissionValidator : AbstractValidator<Mission>
    {
        public MissionValidator()
        {
            RuleFor(x=>x.Name).NotNull().WithMessage("Name can not be empty !").NotEmpty().WithMessage("Name is required !");
            RuleFor(x => x.IsCompleted).NotNull();
        }
    }
}
