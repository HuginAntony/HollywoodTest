using System;
using FluentValidation;
using FluentValidation.Results;
using Tournament.Core.Models;

namespace Tournament.Core.Validators
{
    public class EventValidator : AbstractValidator<EventRequest>
    {
        public EventValidator()
        {
            DateTime eventDateTime = DateTime.Now;
            
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

            RuleFor(x => x.EventDateTime)
                .Custom((d, context) =>
                {
                    eventDateTime = d;
                    if (d.Date <= DateTime.Now.Date.AddDays(-1))
                        context.AddFailure(new ValidationFailure("eventDateTime", $"eventDateTime cannot be before today"));
                });

            RuleFor(x => x.EventEndDateTime)
                .NotEmpty()
                .WithMessage("eventEndDateTime is required.")
                .OverridePropertyName("eventEndDateTime");

            RuleFor(x => x.EventDateTime)
                .Custom((d, context) =>
                {
                    
                    if (d.Date <= eventDateTime.Date)
                        context.AddFailure(new ValidationFailure("eventEndDateTime", $"eventEndDateTime cannot be before the eventDateTime"));
                });

            RuleFor(x => x.AutoClose)
                .NotEmpty()
                .WithMessage("autoClose is required.")
                .OverridePropertyName("autoClose");
        }
    }
}