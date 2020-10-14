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
                .WithMessage("Event is required.")
                .OverridePropertyName("eventId");

            RuleFor(x => x.EventDetailStatusId)
                .NotEmpty()
                .WithMessage("Event detail status is required.")
                .OverridePropertyName("eventDetailStatusId");

            RuleFor(x => x.EventDetailName)
                .MaximumLength(50)
                .NotEmpty()
                .WithMessage("Event detail name is required.")
                .OverridePropertyName("eventDetailName");

            RuleFor(x => x.EventDetailNumber)
                .NotEmpty()
                .WithMessage("Event detail number is required.")
                .OverridePropertyName("eventDetailNumber");

            RuleFor(x => x.EventDetailOdd)
                .NotEmpty()
                .WithMessage("Event detail odd is required.")
                .OverridePropertyName("eventDetailOdd");

            RuleFor(x => x.FinishingPosition)
                .NotEmpty()
                .WithMessage("Finishing position is required.")
                .OverridePropertyName("finishingPosition");

            RuleFor(x => x.FirstTimer)
                .NotEmpty()
                .WithMessage("First timer is required.")
                .OverridePropertyName("firstTimer");
        }
    }
}