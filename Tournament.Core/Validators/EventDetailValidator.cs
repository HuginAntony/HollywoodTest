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
                .WithMessage("eventId is required.")
                .OverridePropertyName("eventId");

            RuleFor(x => x.EventDetailStatusId)
                .NotEmpty()
                .WithMessage("eventDetailStatusId is required.")
                .OverridePropertyName("eventDetailStatusId");

            RuleFor(x => x.EventDetailName)
                .MaximumLength(50)
                .NotEmpty()
                .WithMessage("eventDetailName is required.")
                .OverridePropertyName("eventDetailName");

            RuleFor(x => x.EventDetailNumber)
                .NotEmpty()
                .WithMessage("eventDetailNumber is required.")
                .OverridePropertyName("eventDetailNumber");

            RuleFor(x => x.EventDetailOdd)
                .NotEmpty()
                .WithMessage("eventDetailOdd is required.")
                .OverridePropertyName("eventDetailOdd");

            RuleFor(x => x.FinishingPosition)
                .NotEmpty()
                .WithMessage("finishingPosition is required.")
                .OverridePropertyName("finishingPosition");

            RuleFor(x => x.FirstTimer)
                .NotEmpty()
                .WithMessage("firstTimer is required.")
                .OverridePropertyName("firstTimer");
        }
    }
}