using DistributedTracing;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedTracing
{
    public class CustomValidator : AbstractValidator<WeatherForecast>
    {
        public CustomValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Summary).NotEmpty().WithMessage("Summary Cannot be empty");
            RuleFor(x => x.TemperatureC).NotEmpty().NotEqual(0);

        }
    }
}
