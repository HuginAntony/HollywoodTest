using FluentValidation;
using Tournament.Core.Models;

namespace Tournament.Core.Validators
{
    public class EventValidator : AbstractValidator<EventRequest>
    {
        public EventValidator()
        {
            RuleFor(x => x.TournamentId)
                .NotEmpty()
                .WithMessage("tournamentId is mandatory.")
                .OverridePropertyName("tournamentId");

            RuleFor(x => x.EventName)
                .MaximumLength(100)
                .NotEmpty()
                .WithMessage("eventName is mandatory.")
                .OverridePropertyName("eventName");

            RuleFor(x => x.EventNumber)
                .NotEmpty()
                .WithMessage("eventNumber is mandatory.")
                .OverridePropertyName("eventNumber");

            RuleFor(x => x.EventDateTime)
                .NotEmpty()
                .WithMessage("eventDateTime is mandatory.")
                .OverridePropertyName("eventDateTime");

            RuleFor(x => x.EventEndDateTime)
                .NotEmpty()
                .WithMessage("eventEndDateTime is mandatory.")
                .OverridePropertyName("eventEndDateTime");

            RuleFor(x => x.AutoClose)
                .NotEmpty()
                .WithMessage("autoClose is mandatory.")
                .OverridePropertyName("autoClose");
        }
    }
}