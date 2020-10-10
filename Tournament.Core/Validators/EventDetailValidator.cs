using FluentValidation;
using Tournament.Core.Models;

namespace Tournament.Core.Validators
{
    public class EventDetailValidator : AbstractValidator<EventDetailRequest>
    {
        public EventDetailValidator()
        {
            RuleFor(x => x.EventId)
                .NotEmpty()
                .WithMessage("eventId is mandatory.")
                .OverridePropertyName("eventId");

            RuleFor(x => x.EventDetailStatusId)
                .NotEmpty()
                .WithMessage("eventDetailStatusId is mandatory.")
                .OverridePropertyName("eventDetailStatusId");

            RuleFor(x => x.EventDetailName)
                .MaximumLength(50)
                .NotEmpty()
                .WithMessage("eventDetailName is mandatory.")
                .OverridePropertyName("eventDetailName");

            RuleFor(x => x.EventDetailNumber)
                .NotEmpty()
                .WithMessage("eventDetailNumber is mandatory.")
                .OverridePropertyName("eventDetailNumber");

            RuleFor(x => x.EventDetailOdd)
                .NotEmpty()
                .WithMessage("eventDetailOdd is mandatory.")
                .OverridePropertyName("eventDetailOdd");

            RuleFor(x => x.FinishingPosition)
                .NotEmpty()
                .WithMessage("finishingPosition is mandatory.")
                .OverridePropertyName("finishingPosition");

            RuleFor(x => x.FirstTimer)
                .NotEmpty()
                .WithMessage("firstTimer is mandatory.")
                .OverridePropertyName("firstTimer");
        }
    }
}