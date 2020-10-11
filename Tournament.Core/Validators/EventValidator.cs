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
                .WithMessage("tournamentId is required.")
                .OverridePropertyName("tournamentId");

            RuleFor(x => x.EventName)
                .MaximumLength(100)
                .NotEmpty()
                .WithMessage("eventName is required.")
                .OverridePropertyName("eventName");

            RuleFor(x => x.EventNumber)
                .NotEmpty()
                .WithMessage("eventNumber is required.")
                .OverridePropertyName("eventNumber");

            RuleFor(x => x.EventDateTime)
                .NotEmpty()
                .WithMessage("eventDateTime is required.")
                .OverridePropertyName("eventDateTime");

            RuleFor(x => x.EventEndDateTime)
                .NotEmpty()
                .WithMessage("eventEndDateTime is required.")
                .OverridePropertyName("eventEndDateTime");

            RuleFor(x => x.AutoClose)
                .NotEmpty()
                .WithMessage("autoClose is required.")
                .OverridePropertyName("autoClose");
        }
    }
}